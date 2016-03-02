//-----------------------------------------------------------------------
// <copyright file="BasicDwellingEntity.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="16/10/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Mappings.Sample.SA
{
    using EntityAnnotations;

    [TableName("Products", ToModel = "Product")]
    public class SAProductEntity : BaseEntityTable
    {
        [Identity]
        public Column<int> Id;

        public Column<string> Description { get; set; }
        public Column<decimal> Price { get; set; }

        public SAProductEntity()
        {
            this.Id = new Column<int>("Id", 0);
            this.Description = new Column<string>("Description", string.Empty);
            this.Price = new Column<decimal>("Price", 0);
        }

        public override object[] GetPK()
        {
            return new[] { BaseTableMapping.GetName(() => this.Id) };
        }
    }
}