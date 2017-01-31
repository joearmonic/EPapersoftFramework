namespace EPSFramework.DAL.Mappings.Sample.Convert2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EPSFramework.DAL;

    public class YNCConvert2 : IConvert2
    {
        public dynamic Export(object sourceValue, Type typeToExport = null)
        {
            if (typeToExport == null)
            {
                throw new ArgumentNullException("Conversion type not defined. It Couldn't extract the value of the passed object.");
            }

            if (typeToExport != typeof(bool?) || sourceValue == null)
            {
                return null;
            }

            return sourceValue == null || sourceValue.ToString() == "N" ? null : sourceValue.Equals("Y") ? (bool?)true : (bool?)false;
        }
        
        // TODO: HAY QUE CAMBIARLO PARA QUE VAYA CON YNC
        public dynamic Import(object sourceData, Type typeToImport = null)
        {
            if (sourceData.GetType() == typeof(bool))
            {
                return (bool)sourceData ? "P" : "N";
            }
            else
            {
                return null;
            }
        }
    }

}
