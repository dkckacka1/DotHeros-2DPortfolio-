using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Condition
{
    public abstract class ContinuationCondition : AbnormalCondition
    {
        public abstract void UnApplyCondition(BattleUnit unit);
    }

}