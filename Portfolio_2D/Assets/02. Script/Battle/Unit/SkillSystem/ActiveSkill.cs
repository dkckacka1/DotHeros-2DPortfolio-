using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.skill
{
    public abstract class ActiveSkill : Skill
    {
        public new ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }

        public virtual int GetActiveSkillCooltime(int skillLevel) => GetData.skillCoolTime;

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            e.actionUnit.isSkillUsing = true;
            e.actionUnit.StartCoroutine(PlaySkill(e));
        }
        public override string GetDesc(int skillLevel)
        {
            return base.GetDesc(skillLevel);
            //Debug.Log("나는 액티브 스킬입니다.");
        }

        protected override string GetLogString(SkillActionEventArgs e)
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

        public abstract IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits);
        protected abstract IEnumerator PlaySkill(SkillActionEventArgs e);
    }
}