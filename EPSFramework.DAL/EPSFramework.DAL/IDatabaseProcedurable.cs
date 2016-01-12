//-----------------------------------------------------------------------
// <copyright file="IDatabaseProcedurable.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="15/01/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;

    public interface IDatabaseProcedurable
    {
        string ConnectionString { set; }

        /// <summary>
        /// Executes the specified stored procedure. This overload is the simplest one. It's recommended for Stored procedures that don't need to
        /// retrieve results or do it by returning and specified output parameters. It executes 'non query' action.
        /// </summary>
        /// <param name="spName">Name of the stored procedure.</param>
        /// <param name="parms">The parameters for the stored procedure.</param>
        /// <returns>Whether the execution were completed succesfully.</returns>
        bool Execute(String spName, Dictionary<String, Object> parms);

        /// <summary>
        /// Executes the specified stored procedure. THis overload allows to use a DataReader for retrieving the data, as well as fields' names in
        /// a dictionary to automatically get the selected columns without hardcoding anything.
        /// </summary>
        /// <param name="spName">Name of the stored procedure.</param>
        /// <param name="parms">The parameters for the stored procedure.</param>
        /// <param name="fieldsValues">The fields values that the columns' names of the table have.</param>
        /// <param name="result">The result.</param>
        /// <returns>Whether the execution were completed succesfully.</returns>
        bool Execute(String spName, Dictionary<String, Object> parms, Dictionary<string, object> fieldsValues, out List<Dictionary<String, Object>> result);
    }
}