//-----------------------------------------------------------------------
// <copyright file="Convert2SafeResult.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="21/07/2015" Login="JLAlamo">Created 1.0</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Convert2
{
    using System;    
    using EPSFramework.DAL;

    public class SafeResultConvert2 : IConvert2
    {
        public dynamic Import(object sourceData, Type typeToImport = null)
        {
            return ConvertTo(sourceData, typeToImport);
        }

        public dynamic Export(object elementValue, Type typeToExport = null)
        {
            return ConvertTo(elementValue, typeToExport);
        }

        private static dynamic ConvertTo(object elementValue, Type typeToExport)
        {
            dynamic returnElement = null; // Just in case there's no conversionType defined.

            if (typeToExport == null)
            {
                throw new ArgumentNullException("Conversion type not defined. It Couldn't extract the value of the passed object.");
            }

            if (elementValue == DBNull.Value || elementValue == null)
            {
                return null;
            }
            else if (typeToExport.IsNullable())
            {
                Type valueTypeOfNullable = typeToExport.UnboxNullableArgument();
                returnElement = Convert.ChangeType(elementValue, valueTypeOfNullable);
            }
            else
            {
                returnElement = Convert.ChangeType(elementValue, typeToExport);
            }

            return returnElement;
        }
    }
}