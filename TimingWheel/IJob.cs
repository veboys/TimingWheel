using System;

namespace TimingWheel
{
    public interface IJob
    {
        int Year { get; }
        int Month { get; }
        int Day { get; }
        int Hour { get; }
        int Minute { get; }
        int Second { get; }
        void Execute();
    }
}
