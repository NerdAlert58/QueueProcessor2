using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor2.Objects
{
    public class Proc
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public int Priority { get; set; }
        public int Burst { get; set; }
        public int InitialBurst { get; set; }
        public int Arrival { get; set; }
        public bool Finished { get; set; }
        public int FinishedAtIndex { get; set; }
        public int LastStartProcessing { get; set; }
        public int TurnAroundTime { get; set; }
        public int WaitTime { get; set; }


        public bool Validate()
        {
            if (this.Priority >= 0 && this.Burst >= 1 && this.Arrival >= 0)
            {
                return true;
            }

            return false;
        }

        public Proc Clone()
        {
            return new Proc()
            {
                Name = this.Name,
                Color = this.Color,
                Burst = this.Burst,
                InitialBurst = this.InitialBurst,
                Arrival = this.Arrival,
                Finished = this.Finished,
                FinishedAtIndex = this.FinishedAtIndex,
                LastStartProcessing = this.LastStartProcessing,
                TurnAroundTime = this.TurnAroundTime,
                WaitTime = this.WaitTime
            };
        }

        public void FinalizeValues()
        {
            this.TurnAroundTime = this.CalculateTurnAroundTime();
            this.WaitTime = this.CalculateWaitTime();
        }

        private int CalculateTurnAroundTime()
        {
            return this.FinishedAtIndex - this.Arrival;
        }

        private int CalculateWaitTime()
        {
            return this.FinishedAtIndex - this.InitialBurst - this.Arrival;
        }
    }

    public enum Color
    {
        white,
        blue,
        red,
        green,
        purple,
        orange,
        gray
    };
}
