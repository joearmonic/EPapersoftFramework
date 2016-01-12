//-----------------------------------------------------------------------
// <copyright file="TableMappingBase.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="30/10/2015" Login="JLAlamo">Base class for every
//          mapping context class that creates the entityTable associated
//          with it.
//      </Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using EntityAnnotations;

    public class BaseTableMapping
    {
        /// <summary>
        /// The tables that comprises the TableMappings assembly specific implementation
        /// </summary>
        protected IEnumerable<Type> _tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTableMapping"/> class.
        /// </summary>
        public BaseTableMapping()
        {
            LoadTables(Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Create the specified entity indicated by its class name.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual BaseEntityTable Creator(String entity)
        {
            BaseEntityTable table = null;

            foreach (Type tableType in _tables)
            {
                string compareEntity = GetCompareTerm(tableType, typeof(TableNameAttribute), "ToModel");
                if (compareEntity == null)
                {
                    compareEntity = tableType.Name.Replace("Entity", "");// by default when no attr is used, the name is the classname without suffix.
                }

                if (compareEntity == entity)
                {
                    table = Activator.CreateInstance(tableType, null) as BaseEntityTable;
                    break;
                }
            }

            return table;
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <param name="theTable">The table.</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetColumns(BaseEntityTable theTable)
        {
            Dictionary<string, object> fieldsValues = new Dictionary<string, object>();
            var props = theTable.GetType().GetProperties();
            foreach (var item in props)
            {
                var columnInstance = item.GetValue(theTable, null);// An instance of the property that represents a column in DB.
                fieldsValues.Add(item.Name, columnInstance.GetType().GetProperty("Value").GetValue(columnInstance, null));
            }

            return fieldsValues;
        }

        /// <summary>
        /// Gets the name of a property indicated in a lambda expression.
        /// </summary>
        /// <param name="theProperty">The property.</param>
        /// <returns></returns>
        public static string GetName(Expression<Func<object>> theProperty)
        {
            var memberExpr = theProperty.Body as MemberExpression;
            if (memberExpr != null)
            {
                return memberExpr.Member.Name;
            }

            return null;
        }

        /// <summary>
        /// Gets the compare term. TODO: Poner en zona común de código.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="attrField">The attribute field.</param>
        /// <returns></returns>
        internal static string GetCompareTerm(Type type, Type attribute, String attrField)
        {
            string compareName = type.Name;
            var attrs = type.GetCustomAttributes(attribute, false);
            if (attrs.Count() > 0)
            {
                PropertyInfo prop = attribute.GetProperty(attrField);
                compareName = prop.GetValue(attrs[0], null) as String;
            }

            return compareName;
        }

        /// <summary>
        /// Loads the tables contained in the assembly that define the mappings.
        /// </summary>
        /// <param name="tableMapping">The table mapping.</param>
        protected internal virtual void LoadTables(Assembly tableMapping)
        {
            this._tables = tableMapping
                .GetExportedTypes()
               .Where(t => t.BaseType == typeof(BaseEntityTable))
               .ToList();
        }
    }
}