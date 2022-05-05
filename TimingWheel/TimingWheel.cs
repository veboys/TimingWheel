using System;
using System.Collections.Generic;
using System.Threading;

namespace TimingWheel
{
    public class TimingWheel
    {
        public int Year => YearWheel.Year;
        public int Month => MonthWheel.Month;
        public int Day => DayWheel.Day;
        public int Hour => HourWheel.Hour;
        public int Minute => MinuteWheel.Minute;
        public int Second => SecondWheel.Second;


        internal YearWheel YearWheel { get; }
        internal MonthWheel MonthWheel { get; }
        internal DayWheel DayWheel { get; }
        internal HourWheel HourWheel { get; }
        internal MinuteWheel MinuteWheel { get; }
        internal SecondWheel SecondWheel { get; }

        private Thread _workerThread;

        public TimingWheel() : this(DateTime.Now)
        {

        }

        public TimingWheel(DateTime time, bool autoStart = false)
        {
            YearWheel = new YearWheel(this, time.Year);
            MonthWheel = new MonthWheel(this, time.Month);
            DayWheel = new DayWheel(this, time.Day, DateTime.DaysInMonth(Year, Month));
            HourWheel = new HourWheel(this, time.Hour);
            MinuteWheel = new MinuteWheel(this, time.Minute);
            SecondWheel = new SecondWheel(this, time.Second);

            if (autoStart)
            {
                Start(); 
            }
        }

        public void RegisterJobs(IEnumerable<IJob> jobs)
        {
            if (jobs == null)
                return;

            foreach (var job in jobs)
            {
                RegisterJob(job);
            }
        }

        public void RegisterJob(IJob job)
        {
            if (job.Year > Year)
            {
                YearWheel.RegisterJob(job);
            }
            else if (job.Month > Month)
            {
                MonthWheel.RegisterJob(job);
            }
            else if (job.Day > Day)
            {
                DayWheel.RegisterJob(job);
            }
            else if (job.Hour > Hour)
            {
                HourWheel.RegisterJob(job);
            }
            else if (job.Minute > Minute)
            {
                MinuteWheel.RegisterJob(job);
            }
            else if (job.Second > Second)
            {
                SecondWheel.RegisterJob(job);
            }
            else
            {
                job.Execute();
            }
        }

        public void Start()
        {
            _workerThread = new Thread(new ThreadStart(Tick));
            _workerThread.Start();
        }

        private void Tick()
        {
            while (true)
            {
                Thread.Sleep(1000);
                SecondWheel.MoveNext();
                Console.WriteLine($"{Year}/{Month:00}/{Day:00} {Hour:00}:{Minute:00}:{Second:00}");
            }
        }
    }
}
