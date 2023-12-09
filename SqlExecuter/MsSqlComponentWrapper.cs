using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SqlExecuter
{
    class MsSqlComponentWrapper
    {
        private string _connectionString;

        public MsSqlComponentWrapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteNonQuery(IEnumerable<SqlScriptFile> sqlScripts)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                var tran = con.BeginTransaction();

                foreach(var sqlScript in sqlScripts)
                {
                    try
                    {
                        using (var cmd = new SqlCommand(sqlScript.Content, con, tran))
                        {
                            var ret = cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        var msg = string.Format("{0} - {1}", sqlScript.ScriptInfo, exc.Message);
                        throw new Exception(msg, exc);
                    }
                }

                tran.Commit();
            }
        }
    }
}
