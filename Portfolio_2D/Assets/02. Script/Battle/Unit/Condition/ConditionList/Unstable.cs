using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �����̻��� �Ҿ����� �����̻� Ŭ����
 */

namespace Portfolio.condition
{
    public class Unstable : ContinuationCondition
    {
        public Unstable(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // ȿ�� ������ �����Ѵ�.
            unit.EffectResistance -= 0.3f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // ȿ�� ������ �����Ѵ�.
            unit.EffectResistance += 0.3f;
        }
    }

}