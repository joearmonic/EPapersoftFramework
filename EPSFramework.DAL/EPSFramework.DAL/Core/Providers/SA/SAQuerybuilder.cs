//-----------------------------------------------------------------------
// <copyright file="SAQuerybuilder.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="09/08/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core.Providers.SA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SAQueryBuilder : BaseQueryBuilder
    {
        public SAQueryBuilder(BaseEntityTable mapping)
            : base(mapping)
        {
        }

        protected override string[] Parameters(Dictionary<string, object> fieldValues)
        {
            return fieldValues.Keys.Select(v => " ? ").ToArray();
        }
    }
}