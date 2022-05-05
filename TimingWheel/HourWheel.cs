namespace TimingWheel
{
    internal class HourWheel : Wheel
    {
        public int Hour => currentIndex;

        protected override IWheel ParentWheel => TimingWheel.DayWheel;
        protected override IWheel ChildWheel => TimingWheel.MinuteWheel;

        public HourWheel(TimingWheel timingWheel, int hour) : base(timingWheel, 24, 23, hour)
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
