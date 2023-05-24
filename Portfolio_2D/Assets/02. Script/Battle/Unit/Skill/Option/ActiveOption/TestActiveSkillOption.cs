using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class TestActiveSkillOption : OptionSkill
    {
        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            Debug.Log($"{GetType().Name} 스킬은 스킬레벨이 {skillLevel} 입니다. 타겟은 {targetUnit.name}입니다.");
        }
    }

}