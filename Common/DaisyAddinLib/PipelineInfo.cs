using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Daisy.DaisyConverter.DaisyConverterLib
{
    public class Pipeline
    {
        private Dictionary<string, FileInfo> m_ScriptsInfo = new Dictionary<string, FileInfo>();
        public Pipeline(string ScriptsDirectory)
        {

            PopulateScriptsDictionary(ScriptsDirectory);
        }

        private void PopulateScriptsDictionary(string DirPath)
        {
            string[] ScriptsFilePaths = Directory.GetFiles(DirPath, "*.taskScript", SearchOption.TopDirectoryOnly);

            string ScriptName = "";
            FileInfo ScriptFileInfo = null;
            foreach (string s in ScriptsFilePaths)
            {
                ScriptFileInfo = new FileInfo(s);
                ScriptName = ScriptFileInfo.Name.Replace(ScriptFileInfo.Extension, "");
                m_ScriptsInfo.Add(ScriptName, ScriptFileInfo);

            }

        }
        public Dictionary<string, FileInfo> ScriptsInfo
        {
            get { return m_ScriptsInfo; }
        }

    }
}
