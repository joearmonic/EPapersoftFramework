//-----------------------------------------------------------------------
// <copyright file="IdentityAttribute.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="12/06/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.EntityAnnotations
{
    using System;

    public class IdentityAttribute : Attribute
    {
        public IdentityAttribute()
        {
        }

        public bool IsIdentity
        {
            get;
            set;
        }
    }
}