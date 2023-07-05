using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 틱형 상태이상인 독 상태이상 클래스
 */

namespace Portfolio.condition
{
    public class Poison : TickCondition
    {
        public Poison(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // 상태이상 효과가 발동하면 최대체력의 10%가 감소한다.
            unit.TakeDamage(unit.MaxHP * 0.1f, unit, false);
        }
    }

}