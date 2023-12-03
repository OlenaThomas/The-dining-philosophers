using System;
using System.IO;
using System.Threading;

namespace LunchingPhilosophers
{
    public class Philosopher
    {
        private static Semaphore semaphore = new Semaphore(3, 3, "Philosophers");
        int count = 10;
        private Thread _thread;
        Mutex _mutexLeft;
        Mutex _mutexRight;

        private string _leftFork;
        private string _rightFork;
        private string _number;

        public Philosopher(string forkRight, string forkLeft, string number)
        {
            _rightFork = forkRight;
            _leftFork = forkLeft;
            _number = number;

            _mutexRight = new Mutex(false, forkRight);
            _mutexLeft = new Mutex(false, forkLeft);
        }

        public void Run()
        {
            _thread = new Thread(Eat);
            _thread.Start();
        }

        private void Eat()
        {
            while (count > 0)
            {
                semaphore.WaitOne();

                _mutexRight.WaitOne();
                _mutexLeft.WaitOne();

                File.AppendAllText(string.Concat(_rightFork, ".txt"), string.Format("[{0:HH:mm:ss.fff}] - {1}{2} {3}\n", DateTime.Now, "Philosopher", _number, "took the right fork."));
                File.AppendAllText(string.Concat(_leftFork, ".txt"), string.Format("[{0:HH:mm:ss.fff}] - {1}{2} {3}\n", DateTime.Now, "Philosopher", _number, "took the left fork."));

                Thread.Sleep(100);

                File.AppendAllText(string.Concat(_rightFork, ".txt"), string.Format("[{0:HH:mm:ss.fff}] - {1}{2} {3}\n", DateTime.Now, "Philosopher", _number, "put the right fork."));
                File.AppendAllText(string.Concat(_leftFork, ".txt"), string.Format("[{0:HH:mm:ss.fff}] - {1}{2} {3}\n", DateTime.Now, "Philosopher", _number, "put the left fork."));

                _mutexLeft.ReleaseMutex();
                _mutexRight.ReleaseMutex();

                count--;

                semaphore.Release();
            }

            FileInfo fileInfo = new FileInfo(string.Concat(_rightFork, ".txt"));
            Console.WriteLine(fileInfo.DirectoryName);
            Console.WriteLine("Done");
        }
    }
}
