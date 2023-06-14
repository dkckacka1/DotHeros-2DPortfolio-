using Portfolio.Battle;
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

        public Skill(SkillData skillData)
        {
            this.skillData = skillData;
            this.skillSprite = GameManager.Instance.GetSprite(skillData.skillIconSpriteName);
        }

        public virtual void Action(object sender, SkillActionEventArgs e)
        {
            BattleManager.BattleUIManager.AddLog(GetLogString(e));
        }

        public virtual string GetDesc(int skillLevel)
        {
            //Debug.Log($"{this.skillData.skillDesc} + {skillLevel}");
            return "";
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

        private string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
            {
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
            {
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            string targetUnit = "";
            
            if (e.targetUnits.Count() > 1)
            {
                if (e.targetUnits.Any(unit => !unit.IsEnemy))
                {
                    targetUnit = $"<color=green>[아군들]</color>";
                }
                else
                {
                    targetUnit = $"<color=red>[적군들]</color>";
                }
            }
            else
            {
                if (!e.targetUnits.First().IsEnemy)
                {
                    targetUnit = $"<color=green>[{e.targetUnits.First().name}]</color>";
                }
                else
                {
                    targetUnit = $"<color=red>[{e.targetUnits.First().name}]</color>";
                }
            }

            string log = $"{playerUnit}이(가) {targetUnit}에게 [{skillData.skillName}]을(를) 사용!";

            return log;
        }
    } 
}
