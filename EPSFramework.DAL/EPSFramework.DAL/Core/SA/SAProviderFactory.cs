//-----------------------------------------------------------------------
// <copyright file="ASAProviderFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="31/10/2015" Login="JLAlamo">Factory to provide
//          every element of the Sybase 11 implementation to execute queries</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core.SA
{
    using System;
    using System.Data.Common;
    using iAnywhere.Data.SQLAnywhere;
    using Providers.SA;
    using Providers;

    public class SAProviderFactory : DataBaseProviderFactory, IDataProviderFactory
    {
        public SAProviderFactory(string connString)
            : base(connString)
        {
        }

        public override BaseQueryBuilder CreateQueryBuilder(BaseEntityTable entity)
        {
            return new SAQueryBuilder(entity);
        }

        public ITransactionService CreateTransactionService(TableMappingFactory mappingFactory)
        {            
            TransactionService tranService = new TransactionService(this,  mappingFactory);
            return tranService as ITransactionService;
        }

        internal override DbConnection CreateConnection(string conn = null)
        {
            return new SAConnection(conn);
        }

        internal override DbCommand CreateCommand(string sqlQuery, DbConnection conn)
        {
            return new SACommand(sqlQuery, (SAConnection)conn);
        }

        internal override DbParameter CreateParameter(string name, object value)
        {
            DbParameter p = new SAParameter(name, BuildType(value));
            p.Value = value;
            return p;
        }

        private SADbType BuildType(object value)
        {
            if (value != null)
            {
                switch (value.GetType().Name)
                {
                    case "String":
                        return SADbType.VarChar;

                    case "DateTime":
                        return SADbType.DateTime;

                    case "Int32":
                        return SADbType.Integer;

                    case "Boolean":
                        return SADbType.VarChar;

                    case "Double":
                        return SADbType.Double;
                }
            }

            return SADbType.VarChar;
        }

        public IDatabaseProcedurable CreateCommandProcedure()
        {
            throw new NotImplementedException();
        }
    }
}