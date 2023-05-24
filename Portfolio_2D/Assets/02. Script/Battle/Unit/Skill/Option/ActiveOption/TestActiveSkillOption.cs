using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill.Option
{
    public class TestActiveSkillOption : OptionSkill
    {
        private int stackCount = 2;
        private float posionDamage = 10;

        public TestActiveSkillOption(int skillID) : base(skillID)
        {
        }

        public override void TakeAction(BattleUnit targetUnit, int skillLevel = 1)
        {
            Debug.Log($"{GetType().Name} ��ų�� ��ų������ {skillLevel} �Դϴ�. Ÿ���� {targetUnit.name}�Դϴ�.");
            targetUnit.TakeStackSkill(skillID, stackCount + skillLevel, Posion);
        }

        private void Posion(object sender, EventArgs e)
        {
            (sender as BattleUnit).CurrentHP -= 10;
        }
    }

}