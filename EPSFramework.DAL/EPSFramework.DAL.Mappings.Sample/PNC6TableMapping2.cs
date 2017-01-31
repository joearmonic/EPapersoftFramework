//-----------------------------------------------------------------------
// <copyright file="TableMappingBase.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="19/12/2014" Login="JLAlamo">Created</Entry>
//      <Entry Date="22/12/2014" Login="JLAlamo">Added Resident, Usuario</Entry>
//      <Entry Date="26/12/2014" Login="JLAlamo">Passed shared methods to static class "TableMApping"</Entry>
//      <Entry Date="09/06/2015" Login="JLAlamo">v2.0.0.0 PNC7 Migration</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace TIBFramework.DAL.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TIBFramework.DAL.Core.EntityAnnotations;

    public class PNC6TableMapping2 : BaseTableMapping
    {
        public PNC6TableMapping2()
            : base()
        {
        }

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

        [TableName("RESIDENT", ToModel = "Resident")]
        public class BasicResidentTable : BaseEntityTable
        {
            private Column<int> rESIDENT_DEF;
            private Column<int> lOCATION_REF;
            private Column<string> tITLE;
            private Column<string> fIRST_NAME;
            private Column<string> lAST_NAME;
            private Column<DateTime?> dATE_OF_BIRTH;
            private Column<DateTime?> dATE_TIME_CREATED;
            private Column<string> pRIMARY_YN;
            private Column<int> pRIORITY;
            private Column<string> mIDDLE_NAME;
            private Column<string> nI_NUMBER;
            private Column<string> pHONE_1;
            private Column<string> pHONE_2;
            private Column<string> aGENDA_USER_YNC;
            private Column<int> aCCESS_REF; //" default="0;
            private Column<int> aUTHORITY_REF; //" default="4;
            private Column<string> bANK_GIRO_NUMBER;
            private Column<string> ePRN;
            private Column<string> sS_REF_NO;
            private Column<string> nOTES_YN; //" default="N;

            public Column<int> RESIDENT_DEF
            {
                get
                {
                    return rESIDENT_DEF;
                }

                set
                {
                    rESIDENT_DEF = value;
                }
            }

            public Column<int> LOCATION_REF
            {
                get
                {
                    return lOCATION_REF;
                }

                set
                {
                    lOCATION_REF = value;
                }
            }

            public Column<string> TITLE
            {
                get
                {
                    return tITLE;
                }

                set
                {
                    tITLE = value;
                }
            }

            public Column<string> FIRST_NAME
            {
                get
                {
                    return fIRST_NAME;
                }

                set
                {
                    fIRST_NAME = value;
                }
            }

            public Column<string> LAST_NAME
            {
                get
                {
                    return lAST_NAME;
                }

                set
                {
                    lAST_NAME = value;
                }
            }

            public Column<DateTime?> DATE_OF_BIRTH
            {
                get
                {
                    return dATE_OF_BIRTH;
                }

                set
                {
                    dATE_OF_BIRTH = value;
                }
            }

            public Column<DateTime?> DATE_TIME_CREATED
            {
                get
                {
                    return dATE_TIME_CREATED;
                }

                set
                {
                    dATE_TIME_CREATED = value;
                }
            }

            public Column<string> PRIMARY_YN
            {
                get
                {
                    return pRIMARY_YN;
                }

                set
                {
                    pRIMARY_YN = value;
                }
            }

            public Column<int> PRIORITY
            {
                get
                {
                    return pRIORITY;
                }

                set
                {
                    pRIORITY = value;
                }
            }

            public Column<string> MIDDLE_NAME
            {
                get
                {
                    return mIDDLE_NAME;
                }

                set
                {
                    mIDDLE_NAME = value;
                }
            }

            public Column<string> NI_NUMBER
            {
                get
                {
                    return nI_NUMBER;
                }

                set
                {
                    nI_NUMBER = value;
                }
            }

            public Column<string> PHONE_1
            {
                get
                {
                    return pHONE_1;
                }

                set
                {
                    pHONE_1 = value;
                }
            }

            public Column<string> PHONE_2
            {
                get
                {
                    return pHONE_2;
                }

                set
                {
                    pHONE_2 = value;
                }
            }

            public Column<string> AGENDA_USER_YNC
            {
                get
                {
                    return aGENDA_USER_YNC;
                }

                set
                {
                    aGENDA_USER_YNC = value;
                }
            }

            public Column<int> ACCESS_REF
            {
                get
                {
                    return aCCESS_REF;
                }

                set
                {
                    aCCESS_REF = value;
                }
            }

            public Column<int> AUTHORITY_REF
            {
                get
                {
                    return aUTHORITY_REF;
                }

                set
                {
                    aUTHORITY_REF = value;
                }
            }

            public Column<string> BANK_GIRO_NUMBER
            {
                get
                {
                    return bANK_GIRO_NUMBER;
                }

                set
                {
                    bANK_GIRO_NUMBER = value;
                }
            }

            public Column<string> EPRN
            {
                get
                {
                    return ePRN;
                }

                set
                {
                    ePRN = value;
                }
            }

            public Column<string> SS_REF_NO
            {
                get
                {
                    return sS_REF_NO;
                }

                set
                {
                    sS_REF_NO = value;
                }
            }

            public Column<string> NOTES_YN
            {
                get
                {
                    return nOTES_YN;
                }

                set
                {
                    nOTES_YN = value;
                }
            }

            //  public Column<object> EMAIL_ADDRESS;

            public BasicResidentTable()
            {
                YNConvert2 convertToYN = new YNConvert2();
                YNCConvert2 convertToYNC = new YNCConvert2();

                RESIDENT_DEF = new Column<int>("ResidentDef", 0);
                LOCATION_REF = new Column<int>("LocationRef", 0);
                TITLE = new Column<string>("Title", null);
                FIRST_NAME = new Column<string>("FirstName", null);
                LAST_NAME = new Column<string>("LastName", null);
                DATE_OF_BIRTH = new Column<DateTime?>("DateOfBirth", null);
                DATE_TIME_CREATED = new Column<DateTime?>("DateTimeCreated", null);
                PRIMARY_YN = new Column<string>("IsPrimary", "N", convertToYN);
                PRIORITY = new Column<int>("Priority", 0);
                MIDDLE_NAME = new Column<string>("MiddleName", null);
                NI_NUMBER = new Column<string>("NiNumber", null);
                PHONE_1 = new Column<string>("MobilePhone1", null);
                PHONE_2 = new Column<string>("MobilePhone2", null);
                AGENDA_USER_YNC = new Column<string>("IsAgendaUser", "N", convertToYNC);
                ACCESS_REF = new Column<int>("AccessRef", 0);
                AUTHORITY_REF = new Column<int>("AuthorityRef", 0);
                BANK_GIRO_NUMBER = new Column<string>("BankGiroNumber", null);
                EPRN = new Column<string>("Eprn", null);
                SS_REF_NO = new Column<string>("SsRefNo", null);
                NOTES_YN = new Column<string>("HasNotes", "N", convertToYN);
                // EMAIL_ADDRESS = new Column<object>("EmailAddress;
            }

            public override object[] GetPK()
            {
                return new[] { BaseTableMapping.GetName(() => this.RESIDENT_DEF) };
            }
        }

        public class ContactRelationTable : BaseEntityTable
        {
            public object RELATION_DEF;
            public object RESIDENT_REF;
            public object LOCATION_REF;
            public object PRIORITY;
            public object ENTITY_TYPE;
            public object CONTACT_REF;

            public ContactRelationTable()
            {
            }

            public override object[] GetPK()
            {
                return new[] { BaseTableMapping.GetName(() => this.RELATION_DEF) };
            }

            public ContactRelationTable(ContactRelation model)
            {
                RELATION_DEF = model.RelationDef;
                RESIDENT_REF = model.ResidentDef;
                LOCATION_REF = model.LocationDef;
                PRIORITY = model.Priority;
                ENTITY_TYPE = 2;
                CONTACT_REF = model.ContactDef;
            }

            public override T MapModel<T>(object dto)
            {
                Dictionary<String, object> result = (Dictionary<String, object>)dto;

                var cr = new ContactRelation();

                cr.RelationDef = Convert.ToInt32(result.SafeSelect(BaseTableMapping.GetName(() => this.RELATION_DEF)));
                cr.ContactDef = Convert.ToInt32(result.SafeSelect(BaseTableMapping.GetName(() => this.CONTACT_REF)));
                cr.LocationDef = Convert.ToInt32(result.SafeSelect(BaseTableMapping.GetName(() => this.LOCATION_REF)));
                cr.ResidentDef = Convert.ToInt32(result.SafeSelect(BaseTableMapping.GetName(() => this.RESIDENT_REF)));
                cr.Priority = Convert.ToInt32(result.SafeSelect(BaseTableMapping.GetName(() => this.PRIORITY)));

                return (T)Convert.ChangeType(cr, typeof(T));
            }

            public override void SetModel<T>(T theModel)
            {
                ContactRelation model = (ContactRelation)Convert.ChangeType(theModel, typeof(ContactRelation));
                RELATION_DEF = model.RelationDef;
                RESIDENT_REF = model.ResidentDef;
                LOCATION_REF = model.LocationDef;
                PRIORITY = model.Priority;
                ENTITY_TYPE = 2;
                CONTACT_REF = model.ContactDef;
            }
        }

        public class BasicActionOperatorTable : BaseEntityTable
        {
            public object PK;
            public object Accion_def;
            public object Fecha;
            public object Accion_tipo_ref;
            public object Usuario_ref;
            public object Operador;

            public BasicActionOperatorTable()
            {
            }
        }

        public class BasicKeywordTable : BaseEntityTable
        {
            public object KEYWORD_NO;
            public object KEYWORD_REF;
            public object KEYWORD_TEXT;
            public object RESIDENT_REF;

            public BasicKeywordTable()
            {
            }

            public BasicKeywordTable(Key model)
            {
                KEYWORD_NO = model.KeywordNo;
                KEYWORD_REF = model.KeywordRef;
                KEYWORD_TEXT = model.KeywordText;
                RESIDENT_REF = model.ResidentRef;
            }

            public override object[] GetPK()
            {
                return new[] { GetName(() => this.KEYWORD_REF) };
            }

            public override T MapModel<T>(object dto)
            {
                Dictionary<String, object> result = (Dictionary<String, object>)dto;

                Key model = new Key();

                model.KeywordNo = (int)result.SafeSelect(BaseTableMapping.GetName(() => this.KEYWORD_NO), typeof(Int32));
                model.KeywordRef = (int)result.SafeSelect(BaseTableMapping.GetName(() => this.KEYWORD_REF), typeof(Int32));
                model.ResidentRef = (int)result.SafeSelect(BaseTableMapping.GetName(() => this.RESIDENT_REF));
                model.KeywordText = (string)result.SafeSelect(BaseTableMapping.GetName(() => this.KEYWORD_TEXT));

                return (T)Convert.ChangeType(model, typeof(T));
            }

            public override void SetModel<T>(T theModel)
            {
                Key model = (Key)Convert.ChangeType(theModel, typeof(Key));
                KEYWORD_NO = model.KeywordNo;
                KEYWORD_REF = model.KeywordRef;
                KEYWORD_TEXT = model.KeywordText;
                RESIDENT_REF = model.ResidentRef;
            }
        }

        public class BasicAttrTable : BaseEntityTable
        {
            public object ATTR_CATEGORY_REF;
            public object ATTR_CHOICE_REF;
            public object AUTHORITY_REF;
            public object ENTITY_TYPE;
            public object ENTITY_REF;

            public BasicAttrTable()
            {
            }

            public override object[] GetPK()
            {
                return String.Join(
                    "|",
                    GetName(() => this.ENTITY_REF),
                    GetName(() => this.ENTITY_TYPE),
                    GetName(() => this.ATTR_CATEGORY_REF)).Split('|');
            }
        }

        public class BasicNoteTable : BaseEntityTable
        {
            public Column<int> NOTES_DEF;
            public Column<int> ACCESS_REF;
            public Column<int> ENTITY_TYPE;
            public Column<int> ENTITY_REF;
            public Column<string> TITLE;
            public Column<string> TEXT;

            public BasicNoteTable()
            {
                NOTES_DEF = new Column<int>("NoteDef", 0);
                //CREATE_TIME = new Column<string>("CreateTime", datetime.Today;
                ACCESS_REF = new Column<int>("AccessRef", 0);
                ENTITY_TYPE = new Column<int>("Entity_type", 0);
                ENTITY_REF = new Column<int>("EntityRef", 0);
                TITLE = new Column<string>("Title", null);
                TEXT = new Column<string>("Text", null);
                //DELETE_TIME = new Column<string>("DeleteTime;
                //RELATED_YN = new Column<string>("IsRelated;
            }

            public override object[] GetPK()
            {
                return new[] { GetName(() => this.NOTES_DEF) };
            }
        }

        public class TemporarySuspensionTable : BaseEntityTable
        {
            public object PK;
            public object NUMERO;
            public object USUARIO_REF;
            public object FECHA_INICIO;
            public object FECHA_FIN;
            public object MOTIVO;
            public object FIN_PREVISTO;
            public object OBSERVACIONES;

            public TemporarySuspensionTable()
            {
            }

            public TemporarySuspensionTable(TemporarySuspension model)
            {
                PK = "NOTES_DEF";
                NUMERO = model.Number;
                USUARIO_REF = model.UsuarioRef;
                FECHA_INICIO = model.StartDate;
                FECHA_FIN = model.EndDate;
                MOTIVO = model.Reason;
                FIN_PREVISTO = model.EstimatedEndDate;
                OBSERVACIONES = model.Observations;
            }

            public override T MapModel<T>(object dto)
            {
                throw new NotImplementedException();
            }

            public override void SetModel<T>(T theModel)
            {
                throw new NotImplementedException();
            }
        }

        public class BasicUsuarioTable : BaseEntityTable
        {
            public object PK;
            public object USUARIO_REF;
            public object DNI;
            public object EXPEDIENTE;
            public object FECHA_PETICION;
            public object MOTIVO_PETICION;
            public object FECHA_ALTA;
            public object MOTIVO_ALTA;
            public object FECHA_BAJA;
            public object MOTIVO_BAJA;
            public object BAJA_PREVISTA;
            public object TIPO;
            public object PRIMARY_YN;
            public object SITUACION;
            public object MOTIVO;
            public object ENTIDAD_REF;

            public BasicUsuarioTable()
            {
            }

            public BasicUsuarioTable(User model)
            {
                PK = "USUARIO_REF";
                USUARIO_REF = model.AsResident.ResidentDef; // Must be completed with its resident_def counterpart.
                DNI = model.Dni;
                EXPEDIENTE = model.Expediente;
                FECHA_PETICION = model.DateRequest;
                MOTIVO_PETICION = model.ReasonRequest;
                FECHA_ALTA = model.SubscribeDate;
                MOTIVO_ALTA = model.Subscribereason;
                FECHA_BAJA = model.UnsubscribeDate;
                MOTIVO_BAJA = model.Unsubscribereason;
                BAJA_PREVISTA = model.UnsubscribeExpected;
                TIPO = model.Type;
                PRIMARY_YN = Convert2.ToYNChar(model.AsResident.IsPrimary); // must be completed with its resident_primary_YN counterpart.
                SITUACION = model.Situation != null ? ((int)model.Situation).ToString("00") : null;
                MOTIVO = model.Reason != null ? ((int)model.Reason).ToString("00") : null;
                ENTIDAD_REF = model.EntityRef.ToString("00");
            }

            public override T MapModel<T>(object dto)
            {
                throw new NotImplementedException();
            }

            public override void SetModel<T>(T theModel)
            {
                throw new NotImplementedException();
            }
        }

        public class ContributionsTable : BaseEntityTable
        {
            public object Usuario_ref;
            public object Aportacion;
            public object RENTA_MENSUAL_CALCULADA;
            public object TRAMO_APORTACION_REF;

            public ContributionsTable()
            {
            }

            public ContributionsTable(Contribution model)
            {
                Usuario_ref = model.UsuarioRef;
                Aportacion = model.Aportacion;
                RENTA_MENSUAL_CALCULADA = model.Renta;
                TRAMO_APORTACION_REF = model.TramoAportacion;
            }

            public override T MapModel<T>(object dto)
            {
                throw new NotImplementedException();
            }

            public override void SetModel<T>(T theModel)
            {
                throw new NotImplementedException();
            }
        }

        public class UserKeyTable : BaseEntityTable
        {
            public object Usuario_ref;
            public object Keyword_ref;
            public object Dispositivo_ref;
            public object Creacion;
            public object Modificacion;
            public object Observaciones;

            public UserKeyTable()
            {
            }

            public UserKeyTable(UserKey model)
            {
                Usuario_ref = model.UsuarioRef;
                Keyword_ref = model.KeywordDef;
                Dispositivo_ref = model.Device_ref;
                Creacion = model.CreationDate;
                Modificacion = model.EditDate;
                Observaciones = model.Comments;
            }

            public override T MapModel<T>(object dto)
            {
                throw new NotImplementedException();
            }

            public override void SetModel<T>(T theModel)
            {
                throw new NotImplementedException();
            }
        }
    }
}