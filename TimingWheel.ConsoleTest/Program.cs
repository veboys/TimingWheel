using System;
using System.Threading;

namespace TimingWheel.ConsoleTest
{
    class Program
    {
        static TimingWheel timingWheel;
        static void Main(string[] args)
        {
            Test1();

            Console.ReadKey();
        }

        private static void Test1()
        {
            var startTime = new DateTime(2023, 12, 31, 23, 59, 58);
            timingWheel = new TimingWheel(startTime);

            timingWheel.RegisterJob(new Job(2024, 1, 1, 0, 0, 1, JobExecuteHandler));

            timingWheel.Start();
        }

        private static void Test2()
        {
            timingWheel = new TimingWheel(DateTime.Now);
            timingWheel.Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback(AddTestJobs));
        }

        private static void JobExecuteHandler()
        {
            Console.WriteLine("JobExecuteHandler");
        }

        private static void AddTestJobs(object state)
        {
            var startTime = DateTime.Now;

            for (int i = 0; i < 10000000; i++)
            {
                timingWheel.RegisterJob(new Job(startTime.AddSeconds(i), JobExecuteHandler));
                Thread.Sleep(1);
            }
        }
    }
}
