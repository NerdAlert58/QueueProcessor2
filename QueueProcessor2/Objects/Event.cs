using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor2.Objects
{
    public class Event
    {
        public int Index { get; set; } // This is the unit of time for which this Event exists.
        public IList<Proc> Processes { get; set; } // This is the step of processes that are requesting run time.
        public IList<Proc> Waiting_1 { get; set; } // This will be the queue that holds and drops Processes as they are available for run time again.
        public IList<Proc> Waiting_2 { get; set; } // This will be the queue that holds and drops Processes as they are available for run time again.
        public IList<Proc> Finished { get; set; } // Once a Proc finishes its Burst, we will add it here.
        public Proc CurrentProc { get; set; }
        public int TimeQuantum { get; set; }
        public string Gantt { get; set; }
    }
}
