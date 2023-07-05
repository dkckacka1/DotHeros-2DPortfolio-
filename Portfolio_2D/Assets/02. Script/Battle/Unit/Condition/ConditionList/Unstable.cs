using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지속형 상태이상인 불안정의 상태이상 클래스
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
            // 효과 저항이 감소한다.
            unit.EffectResistance -= 0.3f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // 효과 저항을 복구한다.
            unit.EffectResistance += 0.3f;
        }
    }

}