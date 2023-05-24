using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class BasicAttack : OptionSkill
    {
        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            float attackDamage = currentTurnUnit.AttackPoint;

            targetUnit.TakeDamage(attackDamage);
        }
    }
}