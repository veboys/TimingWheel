namespace TimingWheel
{
    internal class DayWheel : Wheel
    {
        public int Hour => currentIndex;

        protected override IWheel ParentWheel => TimingWheel.MonthWheel;
        protected override IWheel ChildWheel => TimingWheel.HourWheel;

        public DayWheel(TimingWheel timingWheel, int hour) : base(timingWheel, 24, 23, hour)
        {

        }

        public override void RegisterJob(IJob job)
        {
            if (job == null)
                return;
            RegisterJob(job.Hour, job);
        }
    }
}
