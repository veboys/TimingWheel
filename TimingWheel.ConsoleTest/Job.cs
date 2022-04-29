using System;

namespace TimingWheel.ConsoleTest
{
    public class Job : IJob
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        public Action Callback { get; set; }

        public Job(int year, int month, int day, int hour, int minute, int second, Action callback)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
            Second = second;
            Callback = callback;
        }

        public Job(DateTime time, Action callback)
            : this(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, callback)
        {
        }

        public void Execute()
        {
            Console.WriteLine($"job execute, expectTime={ToString()}");
            Callback?.Invoke();
        }

        public override string ToString()
        {
            return $"[{Year}/{Month:00}/{Day:00} {Hour:00}:{Minute:00}:{Second:00}]";
        }
    }
}
