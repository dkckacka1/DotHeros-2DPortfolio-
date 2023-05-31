using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class IncreaseAttackDamage : ContinuationCondition
    {
        public IncreaseAttackDamage(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            unit.AttackPoint *= 1.4f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            unit.AttackPoint /= 1.4f;
        }
    }
}