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

        public int ExecuteNonQuery(IEnumerable<SqlScript> sqlScripts)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                var allScript = string.Join("\r\n ", sqlScripts.Select(a => a.Lines).SelectMany(a => a));

                var tran = con.BeginTransaction();

                using (var cmd = new SqlCommand(allScript, con, tran))
                {
                    var ret = cmd.ExecuteNonQuery();
                    tran.Commit();
                    return ret;
                }
            }
        }
    }
}
