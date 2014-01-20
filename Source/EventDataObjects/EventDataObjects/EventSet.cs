using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public class EventSet : InitialEventSet
    {

        public InitialEventSet EventSet { get; set; }
        public Operator Operator { get; set; }

        public EventSet(InitialEventSet EventSet, Operator Operator)
        {
            this.EventSet = EventSet;
            this.Operator = Operator;
        }


    }
}
