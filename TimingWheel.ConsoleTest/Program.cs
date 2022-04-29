using System;
using System.Threading;

namespace TimingWheel.ConsoleTest
{
    class Program
    {
        static TimingWheel timingWheel;
        static void Main(string[] args)
        {
            //var startTime = new DateTime(2023, 12, 31, 23, 59, 58);
            timingWheel = new TimingWheel(DateTime.Now);
            timingWheel.Start();

            ThreadPool.QueueUserWorkItem(new WaitCallback(AddTestJobs));

            Console.ReadKey();
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
