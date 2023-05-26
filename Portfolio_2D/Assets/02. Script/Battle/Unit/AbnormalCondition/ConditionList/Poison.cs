using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Condition
{
    public class Poison : TickCondition
    {
        public override void ApplyCondition(BattleUnit unit)
        {
            Debug.Log("�� �׽�Ʈ" + unit.MaxHP * 0.1f);
            unit.TakeDamage(unit.MaxHP * 0.1f);
        }
    }

}