using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.skill.Option;
using System;


namespace Portfolio
{
    public class Skill
    {
        private SkillData data;

        private OptionSkill optionSkill_1;
        private OptionSkill optionSkill_2;
        private OptionSkill optionSkill_3;

        public SkillData Data { get => data; }

        public Skill(SkillData skillData)
        {
            this.data = skillData;

            if (skillData.optionName1 != "NULL")
            {
                SetOptionSkill(skillData.optionName1, out optionSkill_1);
            }

            if (skillData.optionName2 != "NULL")
            {
                SetOptionSkill(skillData.optionName2, out optionSkill_2);
            }

            if (skillData.optionName3 != "NULL")
            {
                SetOptionSkill(skillData.optionName3, out optionSkill_3);
            }
        }

        private void SetOptionSkill(string optionName, out OptionSkill optionSkill)
        {
            string className = $"Portfolio.skill.Option.{optionName}";

            Type type = Type.GetType(className);

            optionSkill = Activator.CreateInstance(type) as OptionSkill;
        }

        public void SetCurrentTurnUnit(BattleUnit battleUnit)
        {
            optionSkill_1?.SetCurrentTurnUnit(battleUnit);
            optionSkill_2?.SetCurrentTurnUnit(battleUnit);
            optionSkill_3?.SetCurrentTurnUnit(battleUnit);
        }

        public void TakeAction(object sender, EventArgs e)
        {
            SkillActionEventArgs args = (SkillActionEventArgs)e;
            if (optionSkill_1 != null)
            {
                Debug.Log(optionSkill_1);
            }
            if (optionSkill_2 != null)
            {
                Debug.Log(optionSkill_2);
            }
            if (optionSkill_3 != null)
            {
                Debug.Log(optionSkill_3);
            }
            optionSkill_1?.TakeAction(args.targetUnit, args.skillLevel);
            optionSkill_2?.TakeAction(args.targetUnit, args.skillLevel);
            optionSkill_3?.TakeAction(args.targetUnit, args.skillLevel);
        }

        public override string ToString()
        {
            return @$"{Data.skillName}";
        }
    }
}