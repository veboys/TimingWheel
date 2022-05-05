using System;

namespace TimingWheel
{
    internal class DayWheel : Wheel
    {
        public int Day => currentIndex + 1;

        protected override IWheel ParentWheel => TimingWheel.MonthWheel;
        protected override IWheel ChildWheel => TimingWheel.HourWheel;

        public DayWheel(TimingWheel timingWheel, int day, int daysInMonth) : base(timingWheel, 31, daysInMonth - 1, day - 1)
        {

        }

        public override void RegisterJob(IJob job)
        {
            if (job == null)
                return;
            RegisterJob(job.Day - 1, job);
        }

        protected override void OnMoveNext(int index)
        {
            maxIndex = DateTime.DaysInMonth(TimingWheel.Year, TimingWheel.Month);
            base.OnMoveNext(index);
        }
    }
}
