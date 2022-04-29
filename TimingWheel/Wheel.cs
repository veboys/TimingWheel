using System;
using System.Collections.Generic;

namespace TimingWheel
{
    internal abstract class Wheel : IWheel
    {
        protected int maxIndex;
        protected int currentIndex;
        private readonly List<IJob>[] _jobs;

        public TimingWheel TimingWheel { get; }

        protected virtual IWheel ParentWheel { get; }
        protected virtual IWheel ChildWheel { get; }

        public Wheel(TimingWheel timingWheel, int capacity, int maxIndex, int initIndex)
        {
            TimingWheel = timingWheel;
            _jobs = new List<IJob>[capacity];
            this.maxIndex = maxIndex;
            currentIndex = initIndex;
        }
        public abstract void RegisterJob(IJob job);

        public void RegisterJobs(IEnumerable<IJob> jobs)
        {
            if (jobs == null)
                return;
            foreach (var job in jobs)
            {
                RegisterJob(job);
            }
        }

        protected void RegisterJob(int index, IJob job)
        {
            var jobList = _jobs[index];
            if (jobList == null)
            {
                jobList = new List<IJob>();
                _jobs[index] = jobList;
            }
            lock (jobList)
            {
                jobList.Add(job);
            }
        }

        public void MoveNext()
        {
            currentIndex += 1;

            if (currentIndex > maxIndex)
            {
                currentIndex = 0;
                ParentWheel?.MoveNext();
            }

            OnMoveNext(currentIndex);
        }

        protected virtual void OnMoveNext(int index)
        {
            var jobList = _jobs[index];
            if (jobList == null || jobList.Count == 0)
                return;

            lock (jobList)
            {
                if (ChildWheel == null)
                {
                    ExecuteJobs(jobList);
                }
                else
                {
                    Console.WriteLine($"{GetType().Name} move jobs to {ChildWheel.GetType().Name}");
                    ChildWheel.RegisterJobs(jobList);
                }

                jobList.Clear();
            }
        }

        private void ExecuteJobs(IEnumerable<IJob> jobList)
        {
            foreach (var job in jobList)
            {
                job.Execute();
            }
        }
    }
}
