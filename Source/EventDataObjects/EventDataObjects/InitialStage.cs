using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public class InitialStage
    {

        public int StageID { get; private set; }
        public Stage NextStage { get; set; }
        public InitialEventSet InitialEventSet { get; set; }
        public List<Action> Actions { get; set; }
        public List<ConditionQuery> ConditionQuerys { get; set; }

        public InitialStage(InitialEventSet set, Stage NextStage, List<ConditionQuery> ConditionQuerys = null)
        {
            this.NextStage = NextStage;
            this.InitialEventSet = set;
            this.ConditionQuerys = ConditionQuerys;
            this.StageID = getStageId();

        }

        public InitialStage(InitialEventSet set, List<Action> Actions = null, List<ConditionQuery> ConditionQuerys = null)
        {
            this.InitialEventSet = set;
            this.Actions = Actions;
            this.ConditionQuerys = ConditionQuerys;
            this.StageID = getStageId();
        }


        private int getStageId()
        {
            return 1;
        }

    }
}
