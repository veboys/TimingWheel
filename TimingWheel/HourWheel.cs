namespace TimingWheel
{
    internal class HourWheel : Wheel
    {
        public int Minute => currentIndex;

        protected override IWheel ParentWheel => TimingWheel.DayWheel;
        protected override IWheel ChildWheel => TimingWheel.MinuteWheel;

        public HourWheel(TimingWheel timingWheel, int minute) : base(timingWheel, 60, 59, minute)
        {

        }

        public override void RegisterJob(IJob job)
        {
            if (job == null)
                return;
            RegisterJob(job.Minute, job);
        }
    }
}
