using System;

namespace LunchingPhilosophers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 0;
            string RightFork = args[0];
            string LeftFork = args[1];

            if (args.Length != 0)
            {
                int.TryParse(args[2], out number);
            }

            Console.WriteLine(number);
            Console.WriteLine(RightFork);
            Console.WriteLine(LeftFork);

            Philosopher philosopher = new Philosopher(RightFork, LeftFork, number.ToString());
            philosopher.Run();

            Console.ReadKey();
        }
    }
}
