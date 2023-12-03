using System;
using System.Diagnostics;
using System.IO;

namespace StartProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            const string path = "LunchingPhilosophers.exe";
#else
           const string path = "LunchingPhilosophers.exe";
#endif

            string[] forks = { "fork0", "fork1", "fork2", "fork3", "fork4" };

            Process.Start(path, string.Format("{0} {1} {2}", forks[0], forks[1], "1"));
            Process.Start(path, string.Format("{0} {1} {2}", forks[1], forks[2], "2"));
            Process.Start(path, string.Format("{0} {1} {2}", forks[2], forks[3], "3"));
            Process.Start(path, string.Format("{0} {1} {2}", forks[3], forks[4], "4"));
            Process.Start(path, string.Format("{0} {1} {2}", forks[4], forks[0], "5"));

            Console.WriteLine("StartProject");
            
            var watcher = new FileSystemWatcher(@".");
 
            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Changed += OnChanged;

            watcher.Filter = "fork*.txt";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            
            Console.ReadKey();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            string time = string.Format("[{0:HH:mm:ss.fff}]", DateTime.Now);

            using (FilesChangeLogger loger = new FilesChangeLogger())
            {
                loger.WriteLog(string.Format("{0}  - Changed: {1}", time, e.Name));
            }
        }
    }
}
