//-----------------------------------------------------------------------
// <copyright file="DALException.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="17/01/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.Core
{
    using System;

    public class DALException : Exception
    {
        public DALException(string msg, Exception origEx)
            : base(msg, origEx)
        {
        }
    }
}