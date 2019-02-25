using System;

namespace Mmosoft.Oops.Animation
{
    public class Step
    {
        public int TotalStep { get; set; }
        public int Interval { get; set; }
        public Action<int> AnimAction { get; set; }
        public Step()
        {
            TotalStep = 1;
            Interval = 1;
        }

        public Step(int step, int interval, Action<int> animAction)
        {
            TotalStep = step;
            Interval = interval;
            AnimAction = animAction;
        }
    }
}
