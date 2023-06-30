using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class Brand : ContinuationCondition
    {
        float unitDefencePoint;

        public Brand(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            unitDefencePoint = unit.DefencePoint * 0.2f;
            unit.DefencePoint -= unitDefencePoint;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            unit.DefencePoint += unitDefencePoint;
        }
    }

}