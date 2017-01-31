namespace EPSFramework.DAL.Mappings.Sample.Convert2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EPSFramework.DAL.Core;

    /// <summary>
    /// Defines a class that convert to/from a specific char value from/to database.
    /// <example>'Y' in database represents true boolean value. 'N', false.</example>
    /// </summary>
    public class YNConvert2 : IConvert2
    {
        /// <summary>
        /// Exports the specified source data to the type passed as parameter. This class only validates conversions to Boolean type. If the type
        /// to export indicated were different from a boolean Type it can't convert the value an returns <remarks>null</remarks>.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="typeToExport">The type to export.</param>
        /// <returns></returns>
        public dynamic Export(object sourceValue, Type typeToExport = null)
        {
            if (typeToExport == null)
            {
                throw new ArgumentNullException("Conversion type not defined. It Couldn't extract the value of the passed object.");
            }

            if (typeToExport != typeof(Boolean) || sourceValue == null)
            {
                return null;
            }

            return sourceValue != null && sourceValue.Equals("N") ? true : false;
        }

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
