using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class BasicAttack : OptionSkill
    {
        public BasicAttack(int skillID) : base(skillID)
        {
        }

        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            targetUnit.TakeDamage(currentTurnUnit.AttackPoint);
        }
    }
}