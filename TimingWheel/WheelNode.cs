using System;
using System.Collections.Generic;
using System.Text;

namespace TimingWheel
{
    internal class WheelNode
    {
        public int Id { get; }
        public List<IJob> Jobs { get; }

        public WheelNode(int id)
        {
            Id = id;
            Jobs = new List<IJob>();
        }
    }
}
