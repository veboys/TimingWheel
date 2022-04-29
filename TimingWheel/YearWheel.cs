namespace TimingWheel
{
    internal class YearWheel : Wheel
    {
        public int Month => currentIndex + 1;

        protected override IWheel ParentWheel => TimingWheel;
        protected override IWheel ChildWheel => TimingWheel.MonthWheel;

        public YearWheel(TimingWheel timingWheel, int month) : base(timingWheel, 12, 11, month - 1)
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
