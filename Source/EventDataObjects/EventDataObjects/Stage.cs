using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public class Stage : InitialStage
    {

        public Time Time { get; set; }

        public Stage(InitialEventSet set, Time Time, Stage NextStage = null, List<ConditionQuery> ConditionQuerys = null)
            : base(set, NextStage, ConditionQuerys)
        {
            this.Time = Time;

        }

        public Stage(InitialEventSet set, List<Action> Actions = null, List<ConditionQuery> ConditionQuerys = null)
            : base(set, Actions, ConditionQuerys)
        {
            this.Time = Time;
        }

    }
}
