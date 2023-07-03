using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class BladeShield : ContinuationCondition
    {
        public BladeShield(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            unit.OnTakeDamagedEvent += Unit_OnTakeDamagedEvent;
        }


        public override void UnApplyCondition(BattleUnit unit)
        {
            unit.OnTakeDamagedEvent -= Unit_OnTakeDamagedEvent;
        }

        private void Unit_OnTakeDamagedEvent(object sender, TakeDamageEventArgs e)
        {
            e.hitUnit.TakeDamage(e.damage / 2, e.hitUnit ,false);
        }
    }

}