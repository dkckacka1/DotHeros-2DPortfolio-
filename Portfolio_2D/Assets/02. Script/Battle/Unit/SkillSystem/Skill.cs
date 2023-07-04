using Portfolio.Battle;
using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.skill
{
    public abstract class Skill
    {
        protected SkillData skillData;

        public SkillData GetData => skillData;
        public Sprite skillSprite;
        public List<Condition> conditionList = new List<Condition>();

        public Skill(SkillData skillData)
        {
            this.skillData = skillData;
            this.skillSprite = GameManager.Instance.GetSprite(skillData.skillIconSpriteName);
            if(skillData.conditinID_1 != -1)
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_1, out Condition condition_1))
                {
                    //Debug.Log("conditionO");
                    conditionList.Add(condition_1);
                }
                else
                {
                    //Debug.Log("conditionX");
                }
            }
            if (skillData.conditinID_2 != -1)
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_2, out Condition condition_2))
                {
                    conditionList.Add(condition_2);
                }
            }
            if (skillData.conditinID_3 != -1)
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_3, out Condition condition_3))
                {
                    conditionList.Add(condition_3);
                }
            }
            //Debug.Log(skillData.skillName + " " + skillData.conditinID_1 + " " + skillData.conditinID_2 + " " + skillData.conditinID_3 + " : " + conditionList.Count);
        }

        public virtual void Action(object sender, SkillActionEventArgs e)
        {
            BattleManager.BattleUIManager.AddLog(GetLogString(e));
        }

        protected bool TryGetSkillActionArgs(EventArgs args, out SkillActionEventArgs skillargs)
        {

            if (args != null && args is SkillActionEventArgs)
            {
                skillargs = args as SkillActionEventArgs;
                return true;
            }

            Debug.LogWarning("Action Eventargs error");
            skillargs = null;
            return false;
        }

        protected abstract string GetLogString(SkillActionEventArgs e);

        public virtual string GetDesc(int skillLevel)
        {
            string desc = string.Empty;
            object[] values = GetLevelValue();

            if (values.Length == 0)
            {
                desc = GetData.skillDesc;
            }
            else
            {
                var SetSkillLevelValues = values.Select(value => (float)value * skillLevel).ToArray();
                desc = string.Format(GetData.skillDesc, SetSkillLevelValues.Cast<object>().ToArray());
            }

            return desc;
        }

        public object[] GetLevelValue()
        {
            List<object> levelValues = new List<object>();
            if (GetData.skillLevelValue_1 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_1);
            }

            if (GetData.skillLevelValue_2 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_2);
            }

            if (GetData.skillLevelValue_3 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_3);
            }

            return levelValues.ToArray();
        }


    } 
}
