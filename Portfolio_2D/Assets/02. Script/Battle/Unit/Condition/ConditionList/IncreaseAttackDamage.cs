using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �����̻��� ���ݷ� ���� �����̻� Ŭ����
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
            // ���ݷ� 40% ����
            unit.AttackPoint *= 1.4f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // ���ݷ� ����
            unit.AttackPoint /= 1.4f;
        }
    }
}