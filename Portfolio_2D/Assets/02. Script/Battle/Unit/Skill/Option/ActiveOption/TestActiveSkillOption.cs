using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class TestActiveSkillOption : OptionSkill
    {
        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            Debug.Log($"{GetType().Name} ��ų�� ��ų������ {skillLevel} �Դϴ�. Ÿ���� {targetUnit.name}�Դϴ�.");
        }
    }

}