//-----------------------------------------------------------------------
// <copyright file="TransactionService.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="06/07/2015" Login="JLALAMO">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections;
    using System.Data.Common;
    using Core;

    public sealed class TransactionService : ITransactionService
    {
        private DataBaseProviderFactory _providerFactory;

        private TableMappingFactory _mappingFactory;

        private IDataBaseProvider accesor;

        private BaseTableMapping mapping;

        private TransactionKeeper tranKeeper;

        private System.Collections.Hashtable registeredRepositories;

        public TransactionService(DataBaseProviderFactory providerFactory, TableMappingFactory mappingFactory)
        {
            this._providerFactory = providerFactory;
            this._mappingFactory = mappingFactory;
            this.accesor = _providerFactory.CreateAccesor();
            this.mapping = _mappingFactory.Create();
            registeredRepositories = new System.Collections.Hashtable();
            this.tranKeeper = new TransactionKeeper(this.accesor);
        }

        /// <summary>
        /// Registers the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="theRepository">The repository.</param>
        /// <returns></returns>
        public IRepository<TEntity> RegisterRepository<TEntity>(IRepository<TEntity> theRepository) where TEntity : class
        {
            ((BaseRepository<TEntity>)theRepository).EnableTransaction(this.accesor);

            registeredRepositories = new System.Collections.Hashtable();
            registeredRepositories.Add(theRepository.GetHashCode(), theRepository);

            return theRepository;
        }

        /// <summary>
        /// Registers the repository's type to create a new instance that will be managed by the system.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="theType">The type.</param>
        /// <returns></returns>
        public IRepository<TEntity> RegisterRepository<TEntity>(Type theType) where TEntity : class
        {
            var theRepository = Activator.CreateInstance(theType, new object[] { this._providerFactory, this._mappingFactory });
            ((BaseRepository<TEntity>)theRepository).EnableTransaction(this.accesor);
            registeredRepositories = new System.Collections.Hashtable();
            registeredRepositories.Add(theRepository.GetHashCode(), theRepository);

            return theRepository as IRepository<TEntity>;
        }

        public void UnRegisterRepositories()
        {
            foreach (DictionaryEntry entry in registeredRepositories)
            {
                dynamic Repo = entry.Value;
                Repo.DisableTransaction();
            }

            registeredRepositories.Clear();
        }

        public IDatabaseProcedurable InitializeService(IInitializable initializable)
        {
            var svc = Activator.CreateInstance(initializable.GetType(), new object[] { this.accesor });
            return svc as IDatabaseProcedurable;
        }

        public TransactionKeeper BeginConnection()
        {
            DbConnection conn = this._providerFactory.CreateConnection();// This connection enables internally the conn for the Keeper.
            return this.tranKeeper.BeginTransactionalMode(conn);
        }

        public void EndConnection()
        {
            if (this.tranKeeper == null)
            {
                throw new InvalidOperationException("The transaction Keeper is not initialized");
            }

            this.tranKeeper.Dispose();
        }
    }
}