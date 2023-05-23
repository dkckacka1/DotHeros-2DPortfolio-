using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class BasicAttack : OptionSkill
    {
        public override void TakeAction(BattleUnit targetUnit)
        {
            Debug.Log(ActionText());
            Debug.Log($"{currentTurnUnit} is attack target {targetUnit.name}");

            float attackDamage = currentTurnUnit.AttackPoint;

            targetUnit.TakeDamage(attackDamage);
        }
    }
}