using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.skill.Option;
using System;


namespace Portfolio
{
    public class Skill
    {
        SkillData skillData;

        OptionSkill optionSkill_1;
        OptionSkill optionSkill_2;
        OptionSkill optionSkill_3;

        public Skill(SkillData skillData)
        {
            this.skillData = skillData;

            if (skillData.optionName1 != "NULL")
            {
                SetOptionSkill(skillData.optionName1, out optionSkill_1);
            }

            if (skillData.optionName2 != "NULL")
            {
                SetOptionSkill(skillData.optionName1, out optionSkill_2);
            }

            if (skillData.optionName3 != "NULL")
            {
                SetOptionSkill(skillData.optionName1, out optionSkill_3);
            }
        }

        private void SetOptionSkill(string optionName, out OptionSkill optionSkill)
        {
            string className = $"Portfolio.skill.Option.{optionName}";

            Type type = Type.GetType(className);

            optionSkill = Activator.CreateInstance(type) as OptionSkill;
        }

        public void TakeAction(BattleUnit targetUnit)
        {
            optionSkill_1?.TakeAction(targetUnit);
            optionSkill_2?.TakeAction(targetUnit);
            optionSkill_3?.TakeAction(targetUnit);
        }

        public override string ToString()
        {
            return @$"{skillData.skillName}";
        }
    }
}