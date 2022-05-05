namespace TimingWheel
{
    internal class MinuteWheel : Wheel
    {
        public int Minute => currentIndex;

        protected override IWheel ParentWheel => TimingWheel.HourWheel;
        protected override IWheel ChildWheel => TimingWheel.SecondWheel;

        public MinuteWheel(TimingWheel timingWheel, int minute) : base(timingWheel, 60, 59, minute)
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
