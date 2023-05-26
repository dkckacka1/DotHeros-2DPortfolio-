using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class Poison : TickCondition
    {
        public Poison(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            Debug.Log("독 테스트" + unit.MaxHP * 0.1f);
            unit.TakeDamage(unit.MaxHP * 0.1f);
        }
    }

}