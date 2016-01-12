//-----------------------------------------------------------------------
// <copyright file="IDataProviderFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="29/10/2015" Login="JLAlamo">Interface for whatever data provider</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using Core;

    public interface IDataProviderFactory
    {
        IDataBaseProvider CreateAccesor();

        ITransactionService CreateTransactionService(TableMappingFactory mappingFactory);

        BaseQueryBuilder CreateQueryBuilder(BaseEntityTable entity);
    }
}