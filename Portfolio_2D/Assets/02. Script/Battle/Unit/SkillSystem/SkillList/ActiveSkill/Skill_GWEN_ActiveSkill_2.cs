using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 그웬 유닛의 액티브 스킬 2 클래스
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_ActiveSkill_2 : ActiveSkill
    {
        public Skill_GWEN_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> grids)
        {
            // 아군만 타겟으로 잡음
            return grids.GetAllyTarget(actionUnit).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_GWEN_ActiveSkill_2");
            // 스킬 레벨에 따라 버프 턴 증가
            int conditionTime = 1 + (e.skillLevel);
            foreach (var targetUnit in e.targetUnits)
            {
                // 타겟에게 칼날 방패 상태이상 부여
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], conditionTime);
                // 각 타겟의 발밑에서 이펙트 발동
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_GWEN_ActiveSkill_2");
                effect.transform.position = targetUnit.footPos.position;
            }
            yield return new WaitForSeconds(1f);
            e.actionUnit.isSkillUsing = false;
        }
    }

}