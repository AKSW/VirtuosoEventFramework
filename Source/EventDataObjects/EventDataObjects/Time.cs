using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public class Time
    {

        public TimeSpan Duration { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool WaitTillEnd { get; private set; }

        public Time(DateTime start, int days, int hours, int minutes, int seconds, bool wait)
        {
            this.StartTime = start;
            this.Duration = new TimeSpan(days, hours, minutes, seconds);
            this.WaitTillEnd = wait;
        }

    }
}
