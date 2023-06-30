using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class IncreaseDefencePoint : ContinuationCondition
    {
        public IncreaseDefencePoint(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            unit.DefencePoint *= 1.3f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            unit.DefencePoint /= 1.3f;
        }
    }
}