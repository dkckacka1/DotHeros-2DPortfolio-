using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class TestPassiveSkill : OptionSkill
    {
        private float healValue = 10;

        public TestPassiveSkill(int skillID) : base(skillID)
        {
        }

        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            Debug.Log(GetType().Name + " is TakeAction");
            targetUnit.OnStartCurrentTurnEvent += ((sender ,e) => { (sender as BattleUnit).CurrentHP += (healValue * skillLevel); });
        }
    }
}