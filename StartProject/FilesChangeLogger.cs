using System;
using System.IO;

namespace StartProject
{
    public class FilesChangeLogger : IDisposable
    {
        private object _lockFile = new object();
        private string _fileName;

        private Stream _stream;
        private TextWriter _writer;

        public FilesChangeLogger(string logFileName = "log.txt")
        {
            _fileName = logFileName;

            _stream = new FileStream(_fileName, FileMode.OpenOrCreate | FileMode.Append);
            _writer = new StreamWriter(_stream);
        }

        public void Dispose()
        {
            _writer.Flush();
            _writer.Dispose();
        }

        public void WriteLog(string message)
        {
            lock (_lockFile)
            {
                _writer.WriteLine("{0} - {1}", DateTime.Now.ToLongTimeString(), message);
            }
        }
    }
}
