using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace SqlExecuter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sqlScripts = Directory
                    .GetFiles(ConfigurationManager.AppSettings["sqlScriptDir"].ToString(), "*.sql", SearchOption.AllDirectories)
                    .Select(a => SqlScript.FromFile(a));

                var sqlComWrapper = new MsSqlComponentWrapper(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

                sqlComWrapper.ExecuteNonQuery(sqlScripts);
            }
            catch(Exception exc)
            {
                using (var fs = File.Create(string.Format(".\\{0}.log", DateTime.Now.ToString("yyyyMMddHHmmssfff"))))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(exc.Message);
                        sw.WriteLine(exc.StackTrace);
                    }
                }
            }
        }
    }
}
