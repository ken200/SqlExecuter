using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SqlExecuter
{
    class SqlScript
    {
        public IEnumerable<string> Lines { get; private set; }

        private SqlScript(){}

        public static SqlScript FromFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    var lines = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        lines.Add(line.LastOrDefault() != ';' ? line + "; " : line);
                    }
                    return new SqlScript() { Lines = lines };
                }
            }
        }
    }
}
