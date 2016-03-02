//-----------------------------------------------------------------------
// <copyright file="SAMappingFactory.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="03/12/2015" Login="08/07/2015">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Mappings.Sample
{
    public class SAMappingFactory : TableMappingFactory
    {
        public SAMappingFactory()
        { }

        public override BaseTableMapping Create()
        {
            return new SATableMapping();
        }
    }
}