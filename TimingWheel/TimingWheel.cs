using System;
using System.Collections.Generic;
using System.Threading;

namespace TimingWheel
{
    public class TimingWheel : IWheel
    {
        public int Year { get; set; }
        public int Month => YearWheel.Month;
        public int Day => MonthWheel.Day;
        public int Hour => DayWheel.Hour;
        public int Minute => HourWheel.Minute;
        public int Second => MinuteWheel.Second;

        internal YearWheel YearWheel { get; }
        internal MonthWheel MonthWheel { get; }
        internal DayWheel DayWheel { get; }
        internal HourWheel HourWheel { get; }
        internal MinuteWheel MinuteWheel { get; }

        private Thread _workerThread;
        private List<IJob> _yearJobList = new List<IJob>();

        public TimingWheel() : this(DateTime.Now)
        {

        }

        public TimingWheel(DateTime time, bool autoStart = false)
        {
            Year = time.Year;
            YearWheel = new YearWheel(this, time.Month);
            MonthWheel = new MonthWheel(this, time.Day, DateTime.DaysInMonth(Year, Month));
            DayWheel = new DayWheel(this, time.Hour);
            HourWheel = new HourWheel(this, time.Minute);
            MinuteWheel = new MinuteWheel(this, time.Second);

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
                lock (_yearJobList)
                {
                    _yearJobList.Add(job);
                }
            }
            else if (job.Month > Month)
            {
                YearWheel.RegisterJob(job);
            }
            else if (job.Day > Day)
            {
                MonthWheel.RegisterJob(job);
            }
            else if (job.Hour > Hour)
            {
                DayWheel.RegisterJob(job);
            }
            else if (job.Minute > Minute)
            {
                HourWheel.RegisterJob(job);
            }
            else if (job.Second > Second)
            {
                MinuteWheel.RegisterJob(job);
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
                MinuteWheel.MoveNext();
                Console.WriteLine($"{Year}/{Month:00}/{Day:00} {Hour:00}:{Minute:00}:{Second:00}");
            }
        }

        public void MoveNext()
        {
            Year += 1;
            OnMoveNext(Year);
        }

        private void OnMoveNext(int year)
        {
            if (_yearJobList == null || _yearJobList.Count == 0)
            {
                return;
            }

            lock (_yearJobList)
            {
                for (int i = _yearJobList.Count - 1; i >= 0; i--)
                {
                    var job = _yearJobList[i];
                    if (job.Year == year)
                    {
                        _yearJobList.RemoveAt(i);
                        YearWheel.RegisterJob(job);
                    }
                    else if (job.Year < year)
                    {
                        _yearJobList.RemoveAt(i);
                        job.Execute();
                    }
                }
            }
        }
    }
}
