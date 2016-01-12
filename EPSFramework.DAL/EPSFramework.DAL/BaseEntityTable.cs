namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class BaseEntityTable
    {
        protected PropertyInfo[] _currProperties;

        public BaseEntityTable()
        {
            _currProperties = this.GetType().GetProperties();
        }

        public virtual object[] GetPK()
        {
            return null; // Needs specific implementation, if used, that's because is not abstract.
        }

        public virtual T MapModel<T>(object dto)
        {
            Dictionary<String, object> result = (Dictionary<String, object>)dto;
            T theModel = Activator.CreateInstance<T>();

            PropertyInfo[] theModelProps = theModel.GetType().GetProperties();
            foreach (PropertyInfo targetProperty in theModelProps)
            {
                foreach (PropertyInfo columnProp in _currProperties)
                {
                    var columnInstance = columnProp.GetValue(this, null);
                    String key = columnInstance.GetType().GetProperty("Key").GetValue(columnInstance, null) as String;

                    if (key.Equals(targetProperty.Name))
                    {
                        var transformAction = columnInstance.GetType().GetMethod("Transform");
                        object valueToMap = transformAction.Invoke(columnInstance, new[] { result[columnProp.Name], targetProperty.PropertyType });
                        targetProperty.SetValue(theModel, valueToMap, null);
                        break;
                    }
                }
            }

            return theModel;
        }

        public virtual void SetModel<T>(T theModel)
        {
            PropertyInfo[] theModelProps = theModel.GetType().GetProperties();

            foreach (var sourceProperty in theModelProps)
            {
                foreach (var theTableProp in _currProperties)
                {
                    var columnInstance = theTableProp.GetValue(this, null);
                    string key = columnInstance.GetType().GetProperty("Key").GetValue(columnInstance, null) as string;

                    if (key.Equals(sourceProperty.Name))
                    {
                        var transformAction = columnInstance.GetType().GetMethod("GetIn");
                        object valueToSet = transformAction.Invoke(columnInstance, new[] { sourceProperty.GetValue(theModel, null) });
                        columnInstance.GetType().GetProperty("Value").SetValue(columnInstance, valueToSet, null);
                        break;
                    }
                }
            }
        }

        public string ModelColumnName(string columnName)
        {
            string retName = null;
            if (this._currProperties == null)
            {
                _currProperties = this.GetType().GetProperties();// Late binding in case of the base ctor was not used.
            }

            foreach (var item in _currProperties)
            {
                var columnInstance = item.GetValue(this, null);// An instance of the property that represents a column in DB.
                retName = columnInstance.GetType().GetProperty("Key").GetValue(columnInstance, null) as String;
                if (columnName == retName)
                {
                    retName = item.Name;
                    break;
                }
            }

            if (retName == null)
            {
                throw new KeyNotFoundException(string.Format("A Key in the EntityTable for the columnName {0} was not found", columnName));
            }

            return retName;
        }
    }
}