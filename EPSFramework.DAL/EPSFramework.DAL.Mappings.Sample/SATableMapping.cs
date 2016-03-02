//-----------------------------------------------------------------------
// <copyright file="SATableMapping.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="03/12/2015" Login="JLAlamo">Created and adapted
// to use more than one MODEL in the same assembly</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Mappings.Sample
{
    using System.Linq;
    using System.Reflection;

    public class SATableMapping : BaseTableMapping
    {
        public SATableMapping()
        {
        }

        /// <summary>
        /// Loads the tables contained in the assembly that define the mappings.
        /// </summary>
        /// <param name="tableMapping">The table mapping.</param>
        protected override void LoadTables(Assembly tableMapping)
        {
            this._tables = tableMapping
                .GetExportedTypes()
               .Where(t => 
                t.BaseType == typeof(BaseEntityTable) 
                && t.Name.Contains("SA"))
               .ToList();
        }
    }
}