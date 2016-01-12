//-----------------------------------------------------------------------
// <copyright file="SqlProviderFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="08/07/2015" Login="JLAlamo">Created</Entry>
//      <Entry Date="05/12/2015" Login="JLAlamo">Adapted to the standard adopted</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core.SqlServer
{
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using Providers.SqlServer;
    using Providers;

    public class SqlProviderFactory : DataBaseProviderFactory, IDataProviderFactory
    {
        public SqlProviderFactory(string connString)
            : base(connString)
        {
        }

        public override BaseQueryBuilder CreateQueryBuilder(BaseEntityTable entity)
        {
            return new SqlQueryBuilder(entity);
        }

        public ITransactionService CreateTransactionService(TableMappingFactory mappingFactory)
        {
            TransactionService tranService = new TransactionService(this, mappingFactory);
            return tranService as ITransactionService;
        }

        internal override DbConnection CreateConnection(string conn = null)
        {
            return new SqlConnection(conn);
        }

        internal override DbCommand CreateCommand(string sqlQuery, DbConnection conn)
        {
            return new SqlCommand(sqlQuery, (SqlConnection)conn);
        }

        internal override DbParameter CreateParameter(string name, object value)
        {
            DbParameter p = new SqlParameter(name, BuildType(value));
            p.Value = value;
            return p;
        }

        private SqlDbType BuildType(object value)
        {
            if (value != null)
            {
                switch (value.GetType().Name)
                {
                    case "String":
                        // Comprobar xml para retornar return SqlDbType.Xml;
                        return SqlDbType.VarChar;

                    case "DateTime":
                        return SqlDbType.DateTime2;

                    case "Int32":
                        return SqlDbType.Int;
                    case "Int64":
                        return SqlDbType.BigInt;
                    case "Boolean":
                        return SqlDbType.VarChar;

                    case "Double":
                        return SqlDbType.Decimal;
                }
            }

            return SqlDbType.VarChar;
        }
    }
}