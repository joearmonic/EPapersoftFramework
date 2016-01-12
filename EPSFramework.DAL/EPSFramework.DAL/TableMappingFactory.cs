//-----------------------------------------------------------------------
// <copyright file="TableMappingFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="29/10/2015" Login="JLAlamo">Abstract Factory for the family of TableMappings</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    public abstract class TableMappingFactory
    {
        public abstract BaseTableMapping Create();
    }
}