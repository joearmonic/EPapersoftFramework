//-----------------------------------------------------------------------
// <copyright file="DataBaseProvicerFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="11/06/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core
{
    using System.Data.Common;
    using Providers;

    public abstract class DataBaseProviderFactory
    {
        public DataBaseProviderFactory(string connString)
        {
            this.ConnectionString = connString;
        }

        public virtual string ConnectionString
        {
            get;
            set;
        }

        public virtual IDataBaseProvider CreateAccesor()
        {
            DataProvider provider = new DataProvider(this);
            return provider;
        }

        public abstract BaseQueryBuilder CreateQueryBuilder(BaseEntityTable entity);

        internal abstract DbConnection CreateConnection(string conn = null);

        internal abstract DbCommand CreateCommand(string sqlQuery, DbConnection conn);

        internal abstract DbParameter CreateParameter(string name, object value);
    }
}