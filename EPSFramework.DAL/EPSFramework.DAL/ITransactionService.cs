//-----------------------------------------------------------------------
// <copyright file="ITransactionService.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="06/07/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using Core;

    public interface ITransactionService
    {
        IRepository<TIn> RegisterRepository<TIn>(IRepository<TIn> theService) where TIn : class;

        IRepository<TIn> RegisterRepository<TIn>(Type theType) where TIn : class;

        void UnRegisterRepositories();

        TransactionKeeper BeginConnection();

        void EndConnection();

        IDatabaseProcedurable InitializeService(IInitializable initializable);
    }
}