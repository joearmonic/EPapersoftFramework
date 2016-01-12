//-----------------------------------------------------------------------
// <copyright file="IDataBaseProvider.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="06/07/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using Core;

    public interface IDataBaseProvider
    {
        String ConnectionString { set; get; }

        DbCommand Cmd { set; get; }

        DbConnection Conn { set; get; }

        DbTransaction Tran { set; get; }

        Boolean InATransaction { get; set; }

        DALException CurrentException { get; }

        // List<Dictionary<String, Object>> ExecQueryInTran(string sql, Dictionary<string, object> parameters = null, System.Data.CommandType cmdType = System.Data.CommandType.Text);

        // int ExecNonQueryInTran(String sql, Dictionary<string, object> parameters, System.Data.CommandType cmdType = System.Data.CommandType.Text);

        // Object ExecScalarInTran(string sql, Dictionary<string, object> parameters = null, System.Data.CommandType cmdType = System.Data.CommandType.Text);

        List<Dictionary<String, Object>> ExecuteQuery(String sql, Dictionary<string, object> parameters = null, System.Data.CommandType cmdType = System.Data.CommandType.Text);

        int ExecuteNonQuery(String sql, Dictionary<string, object> parameters, System.Data.CommandType cmdType = System.Data.CommandType.Text);

        Object ExecuteScalar(String sql, Dictionary<string, object> parameters = null, System.Data.CommandType cmdType = System.Data.CommandType.Text);
    }
}