namespace EPSFramework.DAL.Core.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    internal class DataProvider : IDataBaseProvider
    {
        private enum QueryType
        {
            ExecuteQuery,
            ExecuteScalar,
            ExecuteNonQuery
        }

        private DataBaseProviderFactory _providerFactory;

        public DataProvider(DataBaseProviderFactory providerFactory)
        {
            this._providerFactory = providerFactory;
            this.ConnectionString = this._providerFactory.ConnectionString;
        }

        #region Properties
        public DbCommand Cmd { get; set; }

        public DbConnection Conn { get; set; }

        public DbTransaction Tran { get; set; }

        public string ConnectionString { get; set; }

        public DALException CurrentException { get; private set; }

        public bool InATransaction { get; set; }
        #endregion

        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters, CommandType cmdType = CommandType.Text)
        {            
            var ret = WrapExecute(QueryType.ExecuteNonQuery, sql, parameters, cmdType);

            if (this.CurrentException != null)
            {
                throw this.CurrentException;
            }

            if (ret == null)
            {
                return -1;
            }

            return ret;
        }

        public List<Dictionary<string, object>> ExecuteQuery(string sql, Dictionary<string, object> parameters = null, CommandType cmdType = CommandType.Text)
        {            
            List<Dictionary<string, object>> rows = WrapExecute(QueryType.ExecuteQuery, sql, parameters, cmdType);

            if (this.CurrentException != null)
            {
                throw this.CurrentException;
            }

            // It's needed to safe this outcome to be consistent with return an object -> null, return list -> list.Count = 0
            if (rows == null)
            {
                rows = new List<Dictionary<string, object>>();
            }

            return rows;
        }

        public object ExecuteScalar(string sql, Dictionary<string, object> parameters = null, CommandType cmdType = CommandType.Text)
        {
            var ret = WrapExecute(QueryType.ExecuteScalar, sql, parameters, cmdType);

            if (this.CurrentException != null)
            {
                throw this.CurrentException;
            }

            return ret;
        }

        #region Private methods

        private dynamic ExecuteDo(QueryType queryType)
        {
            switch (queryType)
            {
                case QueryType.ExecuteQuery:
                    dynamic rows = new List<Dictionary<String, Object>>();
                    using (DbDataReader read = Cmd.ExecuteReader())
                    {
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                Dictionary<string, object> fieldsResult = new Dictionary<string, object>();
                                for (int i = 0; i < read.FieldCount; i++)
                                {
                                    fieldsResult.Add(read.GetName(i), read[i]);
                                }

                                rows.Add(fieldsResult);
                            }

                            read.Close();
                        }
                    }

                    return rows;
                case QueryType.ExecuteScalar:
                    return Cmd.ExecuteScalar();
                case QueryType.ExecuteNonQuery:
                    return Cmd.ExecuteNonQuery();
                default:
                    throw new InvalidOperationException("Query type not defined or does not exist");
            }
        }

        private dynamic Execute(QueryType queryType, IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
        {
            dynamic result = null;

            this.CurrentException = null; // Clear whatever exception has been produced before to leave room for a new one just in case.

            try
            {
                if (Conn.State != ConnectionState.Open)
                {
                    Conn.Open();
                }

                if (this.InATransaction && this.Tran == null)
                {
                    this.Tran = Conn.BeginTransaction(isolationLevel);
                    this.Cmd.Transaction = this.Tran;
                }

                result = ExecuteDo(queryType);
            }
            catch (DbException ex)
            {
                CurrentException = new DALException("Unable to perfom queries on database.", ex);
            }

            return result;
        }        

        private dynamic WrapExecute(QueryType queryType, string sql, Dictionary<string, object> parameters = null, CommandType cmdType = CommandType.Text, IsolationLevel isolationLevel = IsolationLevel.RepeatableRead)
        {
            dynamic ret = null;
            PrepareConnection(this.InATransaction);
            PrepareCommand(sql, cmdType, this.InATransaction);
            SetDbParameters(parameters);

            if (!this.InATransaction)
            {
                using (Conn)
                {
                    ret = Execute(queryType);
                    Cmd.Dispose();
                    Cmd = null;
                }
            }
            else
            {
                ret = Execute(queryType);
            }

            return ret;
        }

        private void SetDbParameters(Dictionary<string, object> parameters)
        {
            // Prepares the parameters and clears previous ones just in case of reusing cmd object.
            if (Cmd.Parameters.Count > 0)
            {
                Cmd.Parameters.Clear();
            }

            if (parameters != null && parameters.Count > 0)
            {
                DbParameter[] saParams = PrepareParams(parameters);
                if (saParams != null)
                {
                    Cmd.Parameters.AddRange(saParams);
                }
            }
        }

        private DbParameter[] BuidParams(Dictionary<string, object> parameters)
        {
            List<DbParameter> saparameters = new List<DbParameter>();
            foreach (var parameter in parameters)
            {
                DbParameter saParam = _providerFactory.CreateParameter(parameter.Key, parameter.Value == null ? DBNull.Value : parameter.Value);
                saparameters.Add(saParam);
            }

            return saparameters.ToArray();
        }        

        private DbParameter[] PrepareParams(Dictionary<string, object> parameters)
        {
            DbParameter[] dbparams = null;
            if (parameters != null && parameters.Count > 0)
            {
                dbparams = BuidParams(parameters);
            }
            return dbparams;
        }

        private void PrepareCommand(string sql, CommandType cmdType, bool inATransaction)
        {
            if (String.IsNullOrEmpty(this.ConnectionString))
            {
                throw new DALException("Unable to execute queries. The connection could'nt be prepared", new InvalidOperationException("Connection string must be provided"));
            }

            if (String.IsNullOrEmpty(sql))
            {
                throw new DALException("Unable to execute queries. The command could'nt be prepared", new InvalidOperationException("sql command text string must be provided"));
            }

            if (Cmd == null || !inATransaction)
            {
                Cmd = _providerFactory.CreateCommand(sql, Conn);
                Cmd.CommandType = cmdType;
            }
        }

        private void PrepareConnection(bool inATransaction)
        {
            if (String.IsNullOrEmpty(this.ConnectionString))
            {
                throw new DALException("Unable to execute queries. The connection could'nt be prepared", new InvalidOperationException("Connection string must be provided"));
            }

            if (inATransaction)
            {
                if (this.Conn == null)
                {
                    throw new DALException("Unable to execute explicit transaction queries", new InvalidOperationException("Connection for explicit transacted operations must be initialized"));
                }                

                if (String.IsNullOrEmpty(this.Conn.ConnectionString))
                {
                    this.Conn.ConnectionString = this.ConnectionString;
                }
            }
            else
            {
                this.Conn = _providerFactory.CreateConnection(this.ConnectionString);
            }
        }
        #endregion
    }
}