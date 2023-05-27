using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public abstract class ContinuationCondition : Condition
    {
        protected ContinuationCondition(ConditionData conditionData) : base(conditionData)
        {
        }

        public abstract void UnApplyCondition(BattleUnit unit);
    }

}