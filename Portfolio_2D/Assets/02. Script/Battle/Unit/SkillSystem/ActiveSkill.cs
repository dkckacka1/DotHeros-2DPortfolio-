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
        protected bool _isSkillAnimationPlay;
        protected bool _isSkillEffectPlay;
        public bool IsSkillPlay => _isSkillAnimationPlay || _isSkillEffectPlay;

        public new ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            e.actionUnit.isSkillUsing = true;
        }
        public override string GetDesc(int skillLevel)
        {
            return base.GetDesc(skillLevel);
            //Debug.Log("나는 액티브 스킬입니다.");
        }

        public virtual void AddAnimationEvent(AnimationClip animClip)
        {
            return;
        }

        protected virtual IEnumerator PlaySkillAnimation(BattleUnit actionUnit, UnitSkillType skillType)
        {
            actionUnit.Animator.ResetTrigger("Idle");
            string animatorTriggerName = string.Empty;
            switch (skillType)
            {
                case UnitSkillType.BaseAttack:
                    animatorTriggerName = "BaseAttack";
                    actionUnit.Animator.SetTrigger(animatorTriggerName);
                    break;
                case UnitSkillType.ActiveSkill_1:
                    animatorTriggerName = "ActiveSkill1";
                    actionUnit.Animator.SetTrigger(animatorTriggerName);
                    break;
                case UnitSkillType.ActiveSkill_2:
                    animatorTriggerName = "ActiveSkill2";
                    actionUnit.Animator.SetTrigger(animatorTriggerName);
                    break;
            }

            var clip = actionUnit.GetCurrentClip;
            float length = clip.length;

            _isSkillAnimationPlay = true;
            yield return new WaitForSeconds(length);

            actionUnit.Animator.ResetTrigger(animatorTriggerName);
            actionUnit.Animator.SetTrigger("Idle");
            _isSkillAnimationPlay = false;
        }

        protected virtual IEnumerator SkillPlayWait(UnityAction SkillPlayEndAction = null)
        {
            while (true)
            {
                if (!IsSkillPlay)
                {
                    break;
                }
                yield return null;
            }
            SkillPlayEndAction?.Invoke();
        }
    }
}