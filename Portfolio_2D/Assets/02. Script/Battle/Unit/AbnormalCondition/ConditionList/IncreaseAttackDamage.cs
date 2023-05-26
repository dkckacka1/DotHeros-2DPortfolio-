using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Condition
{
    public class IncreaseAttackDamage : ContinuationCondition
    {
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