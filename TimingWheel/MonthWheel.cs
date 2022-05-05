namespace TimingWheel
{
    internal class MonthWheel : Wheel
    {
        public int Month => currentIndex + 1;

        protected override IWheel ParentWheel => TimingWheel.YearWheel;
        protected override IWheel ChildWheel => TimingWheel.DayWheel;

        public MonthWheel(TimingWheel timingWheel, int month) : base(timingWheel, 12, 11, month - 1)
        {

        }

        public override void RegisterJob(IJob job)
        {
            if (job == null)
                return;
            RegisterJob(job.Month - 1, job);
        }
    }
}
