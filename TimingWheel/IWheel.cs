using System.Collections.Generic;

namespace TimingWheel
{
    internal interface IWheel
    {
        void RegisterJob(IJob job);
        void RegisterJobs(IEnumerable<IJob> jobs);
        void MoveNext();
    }
}
