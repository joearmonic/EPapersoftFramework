namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using Core;

    public class ADONetCommandProcedure : IDatabaseProcedurable
    {
        protected IDataBaseProvider _accesor = null;

        public ADONetCommandProcedure(DataBaseProviderFactory factory)
        {
            this._accesor = factory.CreateAccesor();
            this.ConnectionString = factory.ConnectionString;
        }

        public ADONetCommandProcedure(IDataBaseProvider accesor)
        {
            _accesor = accesor;
            this.ConnectionString = _accesor.ConnectionString;
        }

        public string ConnectionString
        {
            set; private get;
        }

        public bool Execute(string spName, Dictionary<string, object> parms)
        {
            int ret = ExecuteNonQueryAccesor(spName, parms, System.Data.CommandType.StoredProcedure);
            if (ret <= 0)
            {
                return false;
            }

            return true;
        }

        public bool Execute(string spName, Dictionary<string, object> parms, Dictionary<string, object> fieldsValues, out List<Dictionary<string, object>> result)
        {
            result = ExecuteQueryAccesor(spName, fieldsValues, parms, System.Data.CommandType.StoredProcedure);
            if (result == null || result.Count == 0)
            {
                return false;
            }

            return true;
        }

        protected List<Dictionary<String, Object>> ExecuteQueryAccesor(string spName, Dictionary<string, object> fieldsValues, Dictionary<string, object> parms, System.Data.CommandType cmdType)
        {
            List<Dictionary<String, Object>> result = null;
            result = _accesor.ExecuteQuery(spName, parms, cmdType);

            return result;
        }

        protected int ExecuteNonQueryAccesor(string spName, Dictionary<string, object> fieldsValues, System.Data.CommandType cmdType)
        {
            int result = 0;
            result = _accesor.ExecuteNonQuery(spName, fieldsValues, cmdType);

            return result;
        }
    }
}