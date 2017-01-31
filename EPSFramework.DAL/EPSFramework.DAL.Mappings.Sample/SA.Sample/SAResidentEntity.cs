namespace EPSFramework.DAL.PNC.Mappings.SA
{
    using System;
    using System.Collections;
    using DAL.Mappings.Sample.Convert2;
    using EPSFramework.DAL.Convert2;
    using EPSFramework.DAL.EntityAnnotations;

    [TableName("RESIDENT", ToModel = "Resident")]
    public class SAResidentEntity : BaseEntityTable
    {
        public SAResidentEntity()
        {
            RESIDENT_DEF = new Column<int>("ResidentDef", 0);

            LOCATION_REF = new Column<int>("LocationRef", 0);
            TITLE = new Column<string>("Title", null);
            FIRST_NAME = new Column<string>("FirstName", null);
            LAST_NAME = new Column<string>("LastName", null);
            DATE_OF_BIRTH = new Column<DateTime?>("DateOfBirth", null);
            NOTES_YN = new Column<string>("Notes", "N", new ComputedConvert2
                    (
                        n => ((IList)n).Count >= 1 ? "Y" : "N"
                    )
                );
            SS_REF_NO = new Column<string>("SsRefNo", null);
            DATE_TIME_CREATED = new Column<DateTime?>("FirstName", DateTime.Now);
            DATE_TIME_LAST_MOD = new Column<DateTime?>("FirstName", DateTime.Now);
            ACCESS_REF = new Column<int>("AccessRef", 0);
            SPLAT_NO = new Column<int?>("SplatNo", null);
            PRIORITY = new Column<int>("Priority", 10000);
            PRIMARY_YN = new Column<string>("IsPrimary", "N", new YNConvert2());
            AUTHORITY_REF = new Column<int>("AuthorityRef", 0);
            IMSI_ID = new Column<string>("ImsiId", null);
            MOBILE_PHONE_NUMBER = new Column<string>("MobilePhoneNumber", null);
            MIDDLE_NAME = new Column<string>("MiddleName", null);
            DATE_OF_DEATH = new Column<DateTime?>("DateOfDeath", null);
            BANK_GIRO_NUMBER = new Column<string>("BankGiroNumber", null);
            EPRN = new Column<string>("Eprn", null);
            EMAIL_ADDRESS = new Column<string>("EmailAddress", null);
            NI_NUMBER = new Column<string>("NINumber", null);
            RISK_COLOUR_REF = new Column<int>("RiskColourRef", 0);
            PHONE_1 = new Column<string>("MobilePhone1", null);
            PHONE_2 = new Column<string>("MobilePhone2", null);
            MOBILE_SOL_USER_YN = new Column<string>("IsMobileUser", null, new YNConvert2());
            AGENDA_USER_YNC = new Column<string>("AgendaUser", null, new YNCConvert2());
        }

        [Identity]
        public Column<int> RESIDENT_DEF { get; set; }

        public Column<int> LOCATION_REF { get; set; }

        public Column<string> TITLE { get; set; }

        public Column<string> FIRST_NAME { get; set; }

        public Column<string> LAST_NAME { get; set; }

        public Column<DateTime?> DATE_OF_BIRTH { get; set; }

        public Column<string> NOTES_YN { get; set; }

        public Column<string> SS_REF_NO { get; set; }

        public Column<DateTime?> DATE_TIME_CREATED { get; set; }

        public Column<DateTime?> DATE_TIME_LAST_MOD { get; set; }

        public Column<int> ACCESS_REF { get; set; }

        public Column<int?> SPLAT_NO { get; set; }

        public Column<int> PRIORITY { get; set; }

        public Column<string> PRIMARY_YN { get; set; }

        public Column<int> AUTHORITY_REF { get; set; }

        public Column<string> IMSI_ID { get; set; }

        public Column<string> MOBILE_PHONE_NUMBER { get; set; }

        public Column<string> MIDDLE_NAME { get; set; }

        public Column<DateTime?> DATE_OF_DEATH { get; set; }

        public Column<string> BANK_GIRO_NUMBER { get; set; }

        public Column<string> EPRN { get; set; }

        public Column<string> EMAIL_ADDRESS { get; set; }

        public Column<string> NI_NUMBER { get; set; }

        public Column<int> RISK_COLOUR_REF { get; set; }

        public Column<string> PHONE_1 { get; set; }

        public Column<string> PHONE_2 { get; set; }

        public Column<string> MOBILE_SOL_USER_YN { get; set; }

        public Column<string> AGENDA_USER_YNC { get; set; }

        public override object[] GetPK()
        {
            return new[] { BaseTableMapping.GetName(() => this.RESIDENT_DEF) };
        }
    }
}