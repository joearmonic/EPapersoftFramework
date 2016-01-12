//-----------------------------------------------------------------------
// <copyright file="Column{T1, T2}" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="10/07/2015" Login="JLAlamo">Created 1.0</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using Convert2;    

    public class Column<T2>
    {
        private IConvert2 _convert2Property;

        public Column(String key, T2 value)
            : this(key, value, (IConvert2)new SafeResultConvert2())
        {
        }

        public Column(String key, T2 value, IConvert2 convert2Property)
        {
            this.Key = key;
            this.Value = value;
            this._convert2Property = convert2Property;
        }

        public String Key { get; set; }

        public T2 Value { get; set; }

        public object Transform(object source, Type typeToConvert)
        {
           var converted = this._convert2Property.Export(source, typeToConvert);
            if (converted == null)
            {
                // Just in case the element is null, It must be initialized to default value depending on its type.
                return typeToConvert.IsValueType || typeToConvert.IsCollection() ? Activator.CreateInstance(typeToConvert) : null;
            }

            return converted;
        }

        public T2 GetIn(object setValue)
        {
            T2 convertedValue = this._convert2Property.Import(setValue, typeof(T2));
            if (this.Value != null && convertedValue == null)
            {
                return this.Value;
            }

            return convertedValue;
        }
    }
}