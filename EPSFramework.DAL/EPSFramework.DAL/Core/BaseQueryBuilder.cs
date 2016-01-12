//-----------------------------------------------------------------------
// <copyright file="PNC7QueryBuilder.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="15/06/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Core.QueryBuilders;
    using QueryBuilders;
    using EntityAnnotations;

    public abstract class BaseQueryBuilder
    {
        private BaseEntityTable _t;
        private Type _entityTableType;
        private string _tableName;
        private List<PropertyInfo> _piIdentity;
        private Dictionary<string, object> _fieldValues;

        protected StringBuilder _queryCompositor;
        protected bool _isMultiQuerySelected;

        public BaseQueryBuilder(BaseEntityTable t)
        {
            this.GetInformationMapping(t);
            this._queryCompositor = new StringBuilder();
            this._isMultiQuerySelected = false;
        }

        public Dictionary<string, object> CurrentProperties
        {
            get;
            set;
        }

        public BaseQueryBuilder Select(Dictionary<string, object> fieldsValues = null)
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            if (fieldsValues == null)
            {
                fieldsValues = _fieldValues;
            }

            var table = BaseTableMapping.GetCompareTerm(_entityTableType, typeof(TableNameAttribute), "Name");

            _queryCompositor.AppendFormat("SELECT {0} FROM {1} ", string.Join(",", fieldsValues.Keys.ToArray()), table);

            return this;
        }

        public virtual BaseQueryBuilder SelectIdentity()
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            _queryCompositor.Append("SELECT @@IDENTITY;");

            return this;
        }

        public BaseQueryBuilder Where<TEntity>(Expression<Func<TEntity, bool>> whereClausule)
        {
            var newExpr = Evaluator.PartialEval(whereClausule);            
            var whereTranslated = new QueryTranslator(this._t).Translate(newExpr);
            this._queryCompositor.Append(" WHERE ");
            this._queryCompositor.Append(whereTranslated);

            return this;
        }

        public BaseQueryBuilder Where(dynamic id)
        {
            String stringDelimiter = "";
            object[] idValue = null;

            if (id.GetType() == typeof(object[]))
            {
                idValue = (object[])id;
            }
            else
            {
                idValue = new object[] { id };
            }

            object[] pkNames = this._t.GetPK();
            this._queryCompositor.Append("WHERE ");

            for (int i = 0; i < pkNames.Length; i++)
            {
                if (idValue[i] is String || idValue[i] is DateTime)
                {
                    stringDelimiter = "'";
                }

                this._queryCompositor.AppendFormat("{0} = {2}{1}{2} AND ", pkNames[i], idValue[i], stringDelimiter);
            }

            this._queryCompositor.Remove(this._queryCompositor.Length - 4, 4);

            return this;
        }

        public BaseQueryBuilder Insert(IDictionary<String, object> columnValues = null)
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            if (columnValues == null)
            {
                columnValues = _fieldValues;
            }

            if (this._piIdentity.Count > 0)
            {
                columnValues = columnValues
                    .Where(fv => this._piIdentity.Any(pi => pi.Name != fv.Key))
                    .ToDictionary(k => k.Key, v => v.Value);
            }

            this._queryCompositor.AppendFormat
                (
                    "INSERT INTO {0} ({1}) VALUES ({2});",
                    _tableName,
                    string.Join(",", columnValues.Keys.ToArray()),
                    string.Join(",", Parameters((Dictionary<string, object>)columnValues))
                );

            this.CurrentProperties = columnValues as Dictionary<string, object>;

            return this;
        }

        public BaseQueryBuilder Update(IDictionary<String, object> columnValues = null)
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            if (columnValues == null)
            {
                columnValues = _fieldValues;
            }

            if (this._piIdentity.Count > 0)
            {
                // Exclude identity columns from updatable fields.
                columnValues = columnValues
                    .Where(fv => this._piIdentity.Any(pi => pi.Name != fv.Key))
                    .ToDictionary(k => k.Key, v => v.Value);
            }

            string columnChanges = " SET ";
            var parametersNames = Parameters(columnValues as Dictionary<string, object>);

            for (int i = 0; i < columnValues.Count; i++)
            {
                var item = columnValues.Skip(i).FirstOrDefault();
                columnChanges += String.Format("{0} = {1},", item.Key, parametersNames[i]);
            }

            columnChanges = columnChanges.Remove(columnChanges.Length - 1, 1); // removes the last comma.

            // update first clausule
            this._queryCompositor.AppendFormat
                (
                    "UPDATE {0} {1} ",
                    _tableName,
                    columnChanges
                );

            var PK = _t.GetPK();
            // where clausule. When updating we know previously the primary identifier of the entity.
            this.Where(this._fieldValues
                    .Where(fv => PK.Any(pk => pk.ToString() == fv.Key))
                    .Select(fv => fv.Value).ToArray()
                    );

            this.CurrentProperties = columnValues as Dictionary<string, object>;

            return this;
        }

        public BaseQueryBuilder Delete()
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            this._queryCompositor.AppendFormat
                (
                    "DELETE {0} ",
                    _tableName
                );

            var PK = _t.GetPK();
            // where clausule. When updating we know previously the primary identifier of the entity.
            this.Where(this._fieldValues
                .Where(fv => PK.Any(pk => pk.ToString() == fv.Key))
                .Select(fv => fv.Value).ToArray()
            );

            return this;
        }

        public void MultiSelect(bool isMulti)
        {
            this._isMultiQuerySelected = isMulti;
        }

        public void Clear()
        {
            _queryCompositor.Clear();
            this.CurrentProperties = new Dictionary<string, object>();
        }

        public override string ToString()
        {
            return this._queryCompositor.ToString();
        }

        private string FullTableName(TableNameAttribute entityTableAttribute)
        {
            string returnName = string.Empty;

            if (!string.IsNullOrEmpty(entityTableAttribute.DatabaseName))
            {
                returnName = entityTableAttribute.DatabaseName + ".";
            }

            if (!string.IsNullOrEmpty(entityTableAttribute.SchemaName))
            {
                returnName += entityTableAttribute.SchemaName + ".";
            }

            if (!string.IsNullOrEmpty(entityTableAttribute.Name))
            {
                returnName += entityTableAttribute.Name;
            }

            return returnName;
        }

        private void GetInformationMapping(BaseEntityTable t)
        {
            Type entityTableType = t.GetType();
            TableNameAttribute entityTableNameAttribute = entityTableType.GetCustomAttributes(typeof(TableNameAttribute), false)[0] as TableNameAttribute;            
            this._tableName = FullTableName(entityTableNameAttribute);
            this._piIdentity = GetIdentityColumns(t);
            this._fieldValues = BaseTableMapping.GetColumns(t).ToDictionary(k => k.Key, v => v.Value);
            this._entityTableType = t.GetType();
            this._t = t;
        }

        private List<PropertyInfo> GetIdentityColumns(BaseEntityTable t)
        {
            Type entityTableType = t.GetType();
            var identityColumnsInfo = entityTableType.GetProperties()
                .Where(pi => pi.GetCustomAttributes(typeof(IdentityAttribute), false).Count() > 0)
                .ToList();

            return identityColumnsInfo;
        }

        protected abstract string[] Parameters(Dictionary<string, object> fieldValues);
    }
}