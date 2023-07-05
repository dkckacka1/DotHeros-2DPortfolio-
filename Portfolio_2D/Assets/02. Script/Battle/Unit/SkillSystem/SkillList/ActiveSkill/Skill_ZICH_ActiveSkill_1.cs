using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지크 유닛의 액티브 스킬 1 클래스
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_ActiveSkill_1 : ActiveSkill
    {
        public Skill_ZICH_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {

        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 아군 최대 5명 타겟
            return targetUnits.GetAllyTarget(actionUnit).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            foreach (var targetUnit in e.targetUnits)
            {
                // 대상에게 공격력 증가 상태이상 부여 스킬 레벨에 따라 지속시간이 다르다.
                targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 1 + e.skillLevel);
                // 대상의 발밑에서 이펙트 출력 
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_ZICH_ActiveSkill1");
                effect.transform.position = targetUnit.footPos.position;
            }
            yield return new WaitForSeconds(0.5f);

            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}