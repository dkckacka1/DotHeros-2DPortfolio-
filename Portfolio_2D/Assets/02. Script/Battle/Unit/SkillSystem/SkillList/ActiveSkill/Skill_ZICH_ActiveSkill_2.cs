using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 지크의 액티브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_ActiveSkill_2 : ActiveSkill
    {
        public Skill_ZICH_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 적 최대 5명까지 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint + (e.actionUnit.AttackPoint * (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_ZICH_ActiveSkill_2");
            foreach (var targetUnit in e.targetUnits)
            {
                // 대상을 타격한다.
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // 대상 위치에 이펙트 출력
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}