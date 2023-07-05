using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지속형 상태이상인 공격력 증가 상태이상 클래스
 */

namespace Portfolio.condition
{
    public class IncreaseAttackDamage : ContinuationCondition
    {
        public IncreaseAttackDamage(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // 공격력 40% 증가
            unit.AttackPoint *= 1.4f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // 공격력 복구
            unit.AttackPoint /= 1.4f;
        }
    }
}