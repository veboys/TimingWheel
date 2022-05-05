namespace TimingWheel
{
    internal class SecondWheel : Wheel
    {
        public int Second => currentIndex;

        protected override IWheel ParentWheel => TimingWheel.MinuteWheel;

        public SecondWheel(TimingWheel timingWheel, int second) : base(timingWheel, 60, 59, second)
        {

        }

        public override void RegisterJob(IJob job)
        {
            if (job == null)
                return;

            RegisterJob(job.Second, job);
        }
    }
}
