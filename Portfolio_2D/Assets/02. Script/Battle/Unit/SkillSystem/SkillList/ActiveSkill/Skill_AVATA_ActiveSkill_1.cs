using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아바타 유닛의 액티브 스킬 1 클래스
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_1 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 체력이 가장 낮은 적순으로 5명 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지
            float skillDamage = e.actionUnit.AttackPoint * (0.5f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            foreach (var targetUnit in e.targetUnits)
            {
                // 타겟에게 피해를 입힌다.
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // 타겟에게 불안정 상태이상을 부여한다.
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], 3);
                // 스킬 이펙트를 타겟의 발밑에서 출력한다.
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_ActiveSkill1");
                effect.transform.position = targetUnit.footPos.position;
            }

            yield return new WaitForSeconds(1f);

            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}