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
    using System;
    using System.Collections;
    using DAL.Convert2;
    using EntityAnnotations;

    [TableName("Orders", ToModel = "Order")]
    public class SAOrderEntity : BaseEntityTable
    {
        [Identity]
        public Column<int> Id;

        public Column<int> ClientId { get; set; }
        public Column<decimal> Total { get; set; }
        public Column<DateTime?> ShippedOn { get; set; }

        public SAOrderEntity()
        {
            this.Id = new Column<int>("LocationDef", 0);
            this.ClientId = new Column<int>("ClientId", 0);
            this.Total = new Column<decimal>("Total", 0);
            this.ShippedOn = new Column<DateTime?>("ShippedOn", null);
        }

        public override object[] GetPK()
        {
            return new[] { BaseTableMapping.GetName(() => this.Id) };
        }
    }
}