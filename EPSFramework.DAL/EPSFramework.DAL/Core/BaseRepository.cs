//-----------------------------------------------------------------------
// <copyright file="DBServiceBase.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="27/12/2014" Login="JLAlamo">Created</Entry>
//      <Entry Date="09/06/2015" Login="JLAlamo">v2.0.0.0 PNC7 Migration</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core
{
    using System;
    using System.Collections.Generic;

    public abstract class BaseRepository<T> where T : class
    {
        protected BaseEntityTable t;

        public BaseRepository()
        {
        }

        protected abstract List<T> InflateList(List<Dictionary<string, object>> result);

        protected abstract T Inflate(object dto);

        internal abstract void EnableTransaction(IDataBaseProvider dataBaseProvider);

        internal abstract void DisableTransaction();
    }
}