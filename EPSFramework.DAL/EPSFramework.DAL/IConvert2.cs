//-----------------------------------------------------------------------
// <copyright file="IConvert2.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="21/07/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;

    public interface IConvert2
    {
        dynamic Export(object sourceValue, Type typeToExport = null);

        dynamic Import(object sourceValue, Type typeToImport = null);
    }
}