using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SqlExecuter
{
    class SqlScriptFile
    {
        private string _filePath;

        public string ScriptInfo => string.Format("SqlScriptFilePath: {0}", _filePath);

        public string Content { get; }

        public SqlScriptFile(string filePath)
        {
            _filePath = filePath;

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    Content = sr.ReadToEnd();
                }
            }
        }
    }
}
