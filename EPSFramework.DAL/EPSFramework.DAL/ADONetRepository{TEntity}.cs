//-----------------------------------------------------------------------
// <copyright file="ADONetRepository{TEntity}.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="30/10/2015" Login="JLAlamo">Repository based on ADO.NET
//      as ORM-like engine.
//      </Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;

    public class ADONetRepository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        private String tEntityName;
        private BaseTableMapping tMapping;
        private IDataProviderFactory dataProviderFactory;
        private IDataBaseProvider _internalAccesor = null;
        private IDataBaseProvider _accesor = null;

        public ADONetRepository(IDataProviderFactory dataProviderFactory, TableMappingFactory mappingFactory)
            : base()
        {
            this.dataProviderFactory = dataProviderFactory;
            this._accesor = this.dataProviderFactory.CreateAccesor();
            tMapping = mappingFactory.Create();

            tEntityName = typeof(TEntity).Name;
            t = tMapping.Creator(tEntityName);
            if (t == null)
            {
                throw new Exception("Table mapping Factory class not found. The repository cannot be initialized");
            }
        }

        #region IRepository implementation

        public IList<TEntity> GetAll()
        {
            var qb = dataProviderFactory.CreateQueryBuilder(t);
            qb.Select();
            var result = this._accesor.ExecuteQuery(qb.ToString());
            if (result.Count > 0)
            {
                return InflateList(result);
            }

            return new List<TEntity>();
        }

        public TEntity Get(dynamic id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("There's no primary key provided as argument to make a get query");
            }

            var qb = dataProviderFactory.CreateQueryBuilder(t);
            qb.Select().Where(id);
            var result = this._accesor.ExecuteQuery(qb.ToString()).FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            return Inflate(result);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expr)
        {
            if (expr == null)
            {
                throw new ArgumentNullException("There's no expression defined to make a get query");
            }

            var qb = dataProviderFactory.CreateQueryBuilder(t);
            qb.Select().Where(expr);
            var result = this._accesor.ExecuteQuery(qb.ToString()).FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            return Inflate(result);
        }

        public dynamic Insert(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("The model to insert is not initializated");
            }

            BaseEntityTable localt = tMapping.Creator(tEntityName);
            localt.SetModel<TEntity>(model);
            var qb = dataProviderFactory.CreateQueryBuilder(localt);
            qb.MultiSelect(true);
            string theQuery = qb.Insert().SelectIdentity().ToString();
            var result = this._accesor.ExecuteScalar(theQuery, BaseTableMapping.GetColumns(localt));

            return result;
        }

        public dynamic Update(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("The model to update is not initializated");
            }

            BaseEntityTable localt = tMapping.Creator(tEntityName);
            localt.SetModel<TEntity>(model);
            var qb = dataProviderFactory.CreateQueryBuilder(localt);
            qb.Update();

            var result = this._accesor.ExecuteNonQuery(qb.ToString(), BaseTableMapping.GetColumns(localt));

            // When we have result is the number of acomplished changes. We need only that the update went correctly.
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public dynamic Delete(TEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("The model to delete is not initializated");
            }

            BaseEntityTable localt = tMapping.Creator(tEntityName);
            localt.SetModel<TEntity>(model);
            var qb = dataProviderFactory.CreateQueryBuilder(localt);
            qb.Delete();

            var result = this._accesor.ExecuteNonQuery(qb.ToString(), BaseTableMapping.GetColumns(localt));

            // When we have result is the number of acomplished changes. We need only that the update went correctly.
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public IList<TEntity> GetAllBy(params object[] keyValuePairObjects)
        {
            throw new NotImplementedException();
        }

        public dynamic GetValue(string fieldName, params object[] keyValuePairObjects)
        {
            throw new NotImplementedException();
        }

        public dynamic Max(string fieldName, params object[] keyValuePairObjects)
        {
            throw new NotImplementedException();
        }

        #endregion IRepository implementation       

        protected override List<TEntity> InflateList(List<Dictionary<string, object>> result)
        {
            List<TEntity> tInstance = null;
            foreach (var row in result)
            {
                if (tInstance == null)
                {
                    tInstance = new List<TEntity>();
                }

                tInstance.Add(Inflate(row));
            }

            return tInstance;
        }

        protected override TEntity Inflate(object dto)
        {
            if (dto == null)
            {
                return default(TEntity);
            }

            return t.MapModel<TEntity>(dto);
        }

        /// <summary>
        /// Enables the transactional functionality of the repository. So the connection needs and explicit commit or rollback from a ITransactionService instance.
        /// </summary>
        /// <param name="dataBaseProvider">The data base provider.</param>
        internal override void EnableTransaction(IDataBaseProvider dataBaseProvider)
        {
            _internalAccesor = this._accesor;
            this._accesor = dataBaseProvider;
        }

        internal override void DisableTransaction()
        {
            this._accesor = _internalAccesor;
            this._internalAccesor = null;
        }        
    }
}