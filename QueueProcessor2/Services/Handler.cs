using QueueProcessor2.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor2.Services
{
    public class Handler
    {
        private IList<Proc> _processes { get; set; }
        private int _timeQuantum_1 { get; set; }
        private int _timeQuantum_2 { get; set; }
        private Proc _currentProc { get; set; }
        private IDictionary<int, Event> _events { get; set; }
        private int _idleTime { get; set; }
        private ProcResults _results { get; set; }

        // a.Show the scheduling order of the processes using a Gantt chart.
        // b.What is the turnaround time for each process?
        // c.What is the waiting time for each process?
        // d.What is the CPU utilization rate


        public Handler(IList<Proc> processes)
        {
            _processes = processes ?? throw new ArgumentNullException(nameof(processes));
            _timeQuantum_1 = 3;
            _timeQuantum_2 = 4;
            _currentProc = null;
            _events = new Dictionary<int, Event>();
            _idleTime = 0;
        }

        private Proc GetIdleProc()
        {
            return new Proc()
            {
                Name = "IDLE",
                Priority = int.MaxValue,
                Burst = 0,
                Arrival = 0,
                Finished = false,
                FinishedAtIndex = 0
            };
        }

        public IDictionary<int, Event> GetEvents()
        {
            return _events;
        }

        public (IDictionary<int, Event>, ProcResults) DoWork()
        {
            var finished = false;
            var index = 0;
            var timeQuantum = 0;
            var waitQueue_1 = new List<Proc>();
            var waitQueue_2 = new List<Proc>();
            var sb = new StringBuilder();
            while (!finished)
            {
                // Check for new arrivals
                var arrivals = new List<Proc>();
                if (_processes.Any(x => x.Arrival == index))
                {
                    for (int i = 0; i < _processes.Count; i++)
                    {
                        var myProc = _processes[i];
                        if (myProc.Arrival == index)
                        {
                            arrivals.Add(myProc);
                        }
                    }

                    // Check if process is currently running
                    for (int i = 0; i < arrivals.Count; i++)
                    {
                        var arr = arrivals[i];

                        if (_currentProc != null)
                        {
                            if (_currentProc.Priority <= arr.Priority)
                            {
                                switch(arr.Priority)
                                {
                                    case 1:
                                        waitQueue_1.Add(arr);
                                        waitQueue_1.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                        break;
                                    case 2:
                                        waitQueue_2.Add(arr);
                                        waitQueue_2.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                        break;
                                    default:
                                        throw new ArgumentNullException("Process priority MUST be 1 or 2 for this algorithm.");
                                }                                
                            }
                            else
                            {
                                if (!string.Equals(_currentProc.Name, "IDLE"))
                                {
                                    switch (_currentProc.Priority)
                                    {
                                        case 1:
                                            waitQueue_1.Add(_currentProc);
                                            waitQueue_1.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                            break;
                                        case 2:
                                            waitQueue_2.Add(_currentProc);
                                            waitQueue_2.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                            break;
                                        default:
                                            throw new ArgumentNullException("Process priority MUST be 1 or 2 for this algorithm.");
                                    }
                                }
                                _currentProc = arr;
                                timeQuantum = _currentProc.Priority == 1 ? _timeQuantum_1 : _timeQuantum_2;
                            }
                        }
                        else
                        {
                            if (!arr.Finished)
                            {
                                if (waitQueue_1.Count > 0 || waitQueue_2.Count > 0)
                                {
                                    switch (arr.Priority)
                                    {
                                        case 1:
                                            waitQueue_1.Add(arr);
                                            waitQueue_1.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                            break;
                                        case 2:
                                            waitQueue_2.Add(arr);
                                            waitQueue_2.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                            break;
                                        default:
                                            throw new ArgumentNullException("Process priority MUST be 1 or 2 for this algorithm.");
                                    }
                                }
                                else
                                {
                                    _currentProc = arr;
                                    timeQuantum = _currentProc.Priority == 1 ? _timeQuantum_1 : _timeQuantum_2;
                                }                                
                            }
                        }
                    }
                }

                if (_currentProc == null)
                {
                    if (waitQueue_1.Count > 0)
                    {
                        _currentProc = waitQueue_1[0];
                        waitQueue_1.RemoveAt(0);
                        timeQuantum = _timeQuantum_1;
                    }
                    else if (waitQueue_2.Count > 0)
                    {
                        _currentProc = waitQueue_2[0];
                        waitQueue_2.RemoveAt(0);
                        timeQuantum = _timeQuantum_2;
                    }
                    else
                    {
                        // Nothing should happen here.
                    }
                }

                if (_currentProc == null || string.Equals(_currentProc.Name, "IDLE"))
                {
                    // IDLE PROCESS TIME
                    _currentProc = GetIdleProc();
                    _idleTime += 1;
                    timeQuantum -= 1;
                    if (timeQuantum == _timeQuantum_1 - 1)
                    {
                        AddToGantt(sb, index, _currentProc.Name);
                    }
                }
                else
                {
                    // use 1 unit from active proc
                    _currentProc.Burst -= 1;
                    timeQuantum -= 1;
                    var timeQuantumCheckpoint = _currentProc.Priority == 1 ? _timeQuantum_1 - 1 : _timeQuantum_2 - 1;
                    
                    if (timeQuantum == timeQuantumCheckpoint)
                    {
                        _currentProc.LastStartProcessing = index;
                        AddToGantt(sb, index, _currentProc.Name);
                    }

                    if (_currentProc.Burst == 0)
                    {
                        _currentProc.Finished = true;
                        _currentProc.FinishedAtIndex = index + 1;
                    }
                }

                // create Event object and add to events
                _events[index] = new Event()
                {
                    Index = index,
                    Processes = CloneProcs(_processes),
                    Waiting_1 = CloneProcs(waitQueue_1),
                    Waiting_2 = CloneProcs(waitQueue_2),
                    CurrentProc = _currentProc.Clone(),
                    TimeQuantum = timeQuantum,
                    Gantt = sb.ToString()
                };


                // If proc still has more units && timeQuantum is not used up, set currentProc to this guy
                if (timeQuantum == 0)
                {
                    if (!string.Equals(_currentProc.Name, "IDLE"))
                    {
                        if (!_currentProc.Finished)
                        {
                            switch (_currentProc.Priority)
                            {
                                case 1:
                                    waitQueue_1.Add(_currentProc);
                                    waitQueue_1.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                    break;
                                case 2:
                                    waitQueue_2.Add(_currentProc);
                                    waitQueue_2.Sort(delegate (Proc x, Proc y) { return y.Priority.CompareTo(x.Priority); });
                                    break;
                                default:
                                    throw new ArgumentNullException("Process priority MUST be 1 or 2 for this algorithm.");
                            }
                        }
                    }
                    _currentProc = null;
                }

                if (_currentProc != null && _currentProc.Finished)
                {
                    _currentProc = null;
                }
                
                if (_processes.All(x => x.Finished == true))
                {
                    finished = true;
                    AddToGantt(sb,index + 1,"Complete|");
                    var lastIndex = _events.Keys.Max();
                    var lastEvent = _events[lastIndex];
                    lastEvent.Gantt = sb.ToString();
                }


                index++;
            }

            if (_events == null || _events.Count <= 0)
            {
                return (null, null);
            }
            var last = _events.Keys.Max();

            var processes = _events[last].Processes;
            var waitTime = 0;
            var turnAroundTime = 0;

            for (int i = 0; i < processes.Count; i++)
            {
                var process = processes[i];
                process.FinalizeValues();
                waitTime += process.WaitTime;
                turnAroundTime += process.TurnAroundTime;
            }

            _results = new ProcResults()
            {
                AverageTurnAroundTime = (float)turnAroundTime / processes.Count,
                AverageWaitTime = (float)waitTime / processes.Count,
                CPUUtilization = (float)(last-_idleTime) / last
            };

            return (_events, _results);
        }

        private void AddToGantt(StringBuilder sb, int index, string name)
        {
            sb.Append($"|{index} - {name} ");
        }

        private IList<Proc> CloneProcs(IList<Proc> procs)
        {
            var clones = new List<Proc>();
            for (int i = 0; i < procs.Count; i++)
            {
                var proc = procs[i];
                clones.Add(new Proc()
                {
                    Name = proc.Name,
                    Color = proc.Color,
                    Burst = proc.Burst,
                    Arrival = proc.Arrival,
                    Finished = proc.Finished,
                    FinishedAtIndex = proc.FinishedAtIndex,
                    LastStartProcessing = proc.LastStartProcessing
                });
            }
            return clones;
        }
    }
}
