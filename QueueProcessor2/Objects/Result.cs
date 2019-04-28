using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor2.Objects
{
    public class ProcResults
    {
        public float AverageTurnAroundTime { get; set; }
        public float AverageWaitTime { get; set; }
        public float CPUUtilization { get; set; }
        public string WaitTimes { get; set; }
        public string TurnAroundTimes { get; set; }
    }
}
