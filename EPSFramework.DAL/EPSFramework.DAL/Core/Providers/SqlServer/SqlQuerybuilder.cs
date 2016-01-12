//-----------------------------------------------------------------------
// <copyright file="SqlQueryBuilder.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="05/12/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core.Providers.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SqlQueryBuilder : BaseQueryBuilder
    {
        public SqlQueryBuilder(BaseEntityTable mapping)
            : base(mapping)
        {
        }

        public override BaseQueryBuilder SelectIdentity()
        {
            if (!_isMultiQuerySelected)
            {
                this.Clear(); // Because denotes a new query.
            }

            _queryCompositor.Append("SELECT SCOPE_IDENTITY();");

            return this;
        }

        protected override string[] Parameters(Dictionary<string, object> fieldValues)
        {
            return fieldValues.Keys.Select(v => String.Format(" @{0}", v)).ToArray();
        }
    }
}