using System.Collections.Generic;

namespace TimingWheel
{
    internal class YearWheel : IWheel
    {
        private IWheel ChildWheel => TimingWheel.MonthWheel;

        private LinkedList<WheelNode> _jobs;

        public TimingWheel TimingWheel { get; }

        public int Year { get; private set; }

        public YearWheel(TimingWheel timingWheel, int year)
        {
            TimingWheel = timingWheel;
            Year = year;
            _jobs = new LinkedList<WheelNode>();
        }

        public void MoveNext()
        {
            Year++;

            var node = FindYearNode(Year);
            if (node != null && ChildWheel != null)
            {
                ChildWheel.RegisterJobs(node.Value.Jobs);
            }
        }

        public void RegisterJob(IJob job)
        {
            lock (_jobs)
            {
                var node = _jobs.First;
                while (node != null)
                {
                    if (node.Value.Id >= job.Year)
                    {
                        break;
                    }

                    node = node.Next;
                }

                if (node == null || node.Value.Id != job.Year)
                {
                    var newNode = new LinkedListNode<WheelNode>(new WheelNode(job.Year));
                    if (node == null)
                    {
                        _jobs.AddLast(newNode);
                    }
                    else
                    {
                        _jobs.AddBefore(node, newNode);
                    }
                    node = newNode;
                }

                node.Value.Jobs.Add(job);
            }
        }

        public void RegisterJobs(IEnumerable<IJob> jobs)
        {
            foreach (var job in jobs)
            {
                RegisterJob(job);
            }
        }

        private LinkedListNode<WheelNode> FindYearNode(int year)
        {
            var node = _jobs.First;
            while (node != null)
            {
                if (node.Value.Id == Year)
                {
                    return node;
                }
                else if (node.Value.Id > year)
                {
                    break;
                }

                node = node.Next;
            }

            return null;
        }
    }
}
