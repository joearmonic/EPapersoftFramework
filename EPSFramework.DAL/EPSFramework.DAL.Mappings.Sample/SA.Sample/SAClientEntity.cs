namespace EPSFramework.DAL.PNC.Mappings.SA
{
    using System;
    using System.Collections;
    using DAL.Mappings.Sample.Convert2;
    using Convert2;
    using EntityAnnotations;

    [TableName("Clients", ToModel = "Client")]
    public class SAClientEntity : BaseEntityTable
    {
        public SAClientEntity()
        {
            Id = new Column<int>("Id", 0);
            Name = new Column<string>("FirstName", null);
            LastName = new Column<string>("LastName", null);
            DateofBirth = new Column<DateTime?>("DateOfBirth", null);
            IsDeleted_YN = new Column<string>("IsDeleted", "N", new ComputedConvert2
                    (
                        n => ((IList)n).Count >= 1 ? "Y" : "N"
                    )
                );
            EmailAddress = new Column<string>("EmailAddress", null);
            Priority_YNC = new Column<string>("Priority", null, new YNCConvert2());
        }

        [Identity]
        public Column<int> Id { get; set; }

        public Column<string> Name { get; set; }

        public Column<string> LastName { get; set; }

        public Column<DateTime?> DateofBirth { get; set; }

        public Column<string> IsDeleted_YN { get; set; }

        public Column<string> EmailAddress { get; set; }

        public Column<string> Priority_YNC { get; set; }

        public override object[] GetPK()
        {
            return new[] { BaseTableMapping.GetName(() => this.Id) };
        }
    }
}