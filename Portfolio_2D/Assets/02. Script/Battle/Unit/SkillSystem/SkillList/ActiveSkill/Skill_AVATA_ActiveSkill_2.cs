using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 아바타 유닛의 액티브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_2 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        // 스킬 레벨에 따라 쿨타임이 감소하기에 부모 클래스의 스킬 쿨타임을 리턴하는 함수를 재정의 해준다.
        public override int GetActiveSkillCooltime(int skillLevel)
        {
            return base.GetActiveSkillCooltime(skillLevel) - skillLevel;
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 체력이 가장 낮은 적순으로 5명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 자신한테 이펙트 출력
            SkillEffect effect = BattleManager.ObjectPool.SpawnSkillEffect();
            effect.PlayEffect("Anim_Skill_Effect_AVATA_ActiveSkill2");
            effect.transform.position = e.actionUnit.transform.position;

            int manaValue = 0;

            foreach (var targetUnit in e.targetUnits)
            {
                if (targetUnit.IsEffectHit(e.actionUnit.EffectHit))
                    // 대상에게 효과 적중 판단을 하고 성공했으면
                {
                    // 마나 회복 수치 증가
                    manaValue++;
                }
            }

            // 마나 회복
            if (manaValue > 0)
                e.actionUnit.AddMana(manaValue);

            yield return new WaitForSeconds(0.5f);

            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }

}