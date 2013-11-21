using System;
using System.IO;
using System.Text;

namespace Rose.Common
{
    public class LogFile : IDisposable
    {

        public static LogFile Default { get; set; }

        public LogFile(string filePath)
        {
            this.filePath = filePath;
            File.CreateText(filePath).Close();
        }

        private readonly string filePath;

        public void Write(string s)
        {
            File.AppendAllText(filePath, s, Encoding.Unicode);
        }

        public void WriteLine(string s)
        {
            Write(s + Environment.NewLine);
        }

        public void Dispose()
        {
            File.Delete(filePath);
        }
    }
}
