//-----------------------------------------------------------------------
// <copyright file="BasicDwellingEntity.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="16/10/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace TIBFramework.DAL.Mappings.PNC
{
    using System;
    using System.Collections;
    using TIBFramework.DAL.Convert2;
    using TIBFramework.DAL.EntityAnnotations;

    [TableName("EPEC_LOCATION", ToModel = "Dwelling")]
    public class BasicDwellingTable : BaseEntityTable
    {
        [Identity]
        public Column<int> LOCATION_DEF;

        public Column<long> EQUIP_ID;
        public Column<int> EQUIP_MODEL_REF;
        public Column<string> EQUIP_PHONE;
        public Column<string> ADDRESS_NAME;
        public Column<string> ADDRESS_NUMBER;
        public Column<string> ADDRESS_STREET;
        public Column<string> ADDRESS_AREA;
        public Column<string> ADDRESS_TOWN;
        public Column<string> ADDRESS_COUNTY;
        public Column<string> ADDRESS_POSTCODE;
        public Column<string> OTHER_PHONE;
        public Column<DateTime?> DATE_TIME_CREATED;
        public Column<int> ACCESS_REF;
        public Column<int> AUTHORITY_REF;
        public Column<int> SCHEME_REF;
        public Column<int> SCHEME_ID;
        public Column<string> NOTES_YN;

        public BasicDwellingTable()
        {
            this.LOCATION_DEF = new Column<int>("LocationDef", 0);
            this.EQUIP_ID = new Column<long>("EquipId", 0);
            this.EQUIP_MODEL_REF = new Column<int>("EquipModelRef", 0);
            this.EQUIP_PHONE = new Column<string>("EquipPhone", null);
            this.ADDRESS_NAME = new Column<string>("AddressName", null);
            this.ADDRESS_NUMBER = new Column<string>("AddressNumber", null);
            this.ADDRESS_STREET = new Column<string>("AddressStreet", null);
            this.ADDRESS_AREA = new Column<string>("AddressArea", null);
            this.ADDRESS_TOWN = new Column<string>("AddressTown", null);
            this.ADDRESS_COUNTY = new Column<string>("AddressCounty", null);
            this.ADDRESS_POSTCODE = new Column<string>("AddressPostCode", null);
            this.OTHER_PHONE = new Column<string>("OtherPhone", null);
            this.DATE_TIME_CREATED = new Column<DateTime?>("DateTimeCreated", null);
            this.ACCESS_REF = new Column<int>("AccessRef", 0);
            this.AUTHORITY_REF = new Column<int>("AuthorityRef", 0);
            this.SCHEME_REF = new Column<int>("SchemeRef", 0);
            this.SCHEME_ID = new Column<int>("SchemeId", 0);
            this.NOTES_YN = new Column<string>(
                "Notes", "N", new ComputedConvert2
                    (
                        n => ((IList)n).Count >= 1 ? "Y" : "N"
                    )
                );
        }

        public override object[] GetPK()
        {
            return new[] { BaseTableMapping.GetName(() => this.LOCATION_DEF) };
        }
    }
}