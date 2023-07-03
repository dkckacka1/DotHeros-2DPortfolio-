using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public abstract IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits);
        protected abstract IEnumerator PlaySkill(SkillActionEventArgs e);
    }
}