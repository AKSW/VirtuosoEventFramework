using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public class InitialEventSet
    {
        private bool isInitialSet = true;
        public int SetID { get; set; }
        public List<AtomicEvent> events;

        public InitialEventSet(List<AtomicEvent> events)
        {
            this.events = events;
        }
        public InitialEventSet()
        {
            isInitialSet = false;
        }

        public List<AtomicEvent> Events{
            get{
                if (isInitialSet)
                    return events;
                else
                    return null;
            }
        }
        
    }
}
