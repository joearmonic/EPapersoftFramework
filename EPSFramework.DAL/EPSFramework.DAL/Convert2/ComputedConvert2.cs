namespace EPSFramework.DAL.Convert2
{
    using System;

    /// <summary>
    /// Defines a class that convert to/from a specific value from/to database value by executing a custom Function defined as parameter
    /// <example>ComputedConvert2 can accept FUNC{object[], string} in which the resulting string is converted operating over a field or a set of fields</example>
    /// </summary>
    public class ComputedConvert2 : IConvert2
    {
        private Func<object, dynamic> _computeIn;

        private Func<object, dynamic> _computeOut;

        public ComputedConvert2(Func<object, dynamic> computeIn, Func<object, dynamic> computeOut = null)
        {
            this._computeIn = computeIn;
            this._computeOut = computeOut;
        }

        /// <summary>
        /// Exports the specified source data to the type passed as parameter. This class only validates conversions to Boolean type. If the type
        /// to export indicated were different from a boolean Type it can't convert the value an returns <remarks>null</remarks>.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <param name="typeToExport">The type to export.</param>
        /// <returns></returns>
        public dynamic Export(object sourceValue, Type typeToExport = null)
        {
            if (_computeOut != null)
            {
                var result = _computeOut(sourceValue);

                if (typeToExport != null)
                {
                    return Convert.ChangeType(result, typeToExport);
                }

                return result;
            }

            return null;
        }

        public dynamic Import(object sourceData, Type typeToImport = null)
        {
            if (_computeIn != null)
            {
                var result = _computeIn(sourceData);

                if (typeToImport != null)
                {
                    return Convert.ChangeType(result, typeToImport);
                }

                return result;
            }

            return null;
        }
    }
}