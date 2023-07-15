using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 데스블링어 유닛의 액티브 스킬 1 클래스
 * 적군중 무작위 한명을 선택하여 자신의 공격력의 (80 +{0})% 만큼 데미지를 입힙니다. 그리고 연결된 유닛에게도  반절의 데미지를 입힙니다.
 */

namespace Portfolio.skill
{
    public class Skill_DeathBringer_ActiveSkill_1 : ActiveSkill
    {
        public Skill_DeathBringer_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 1. 적들만 타겟합니다.
            // 2. 개중에 한명만 지정합니다.
            var list = new List<GridPosition>(1) { targetUnits.GetEnemyTarget(actionUnit, this).GetRandomTarget() };
            return list.SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지는 80% + 스킬레벨 * 20%
            float skillDamage = e.actionUnit.AttackPoint * (0.8f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            // 연결된 유닛에게 줄 데미지는 스킬데미지의 반절
            float nextDamage = skillDamage / 2;

            // 스킬 이펙트 보여주기
            var effect = BattleManager.ObjectPool.SpawnSkillEffect();
            effect.PlayEffect("Anim_Skill_Effect_DeathBringer_ActiveSkill_1");
            effect.transform.position = e.targetUnits.ToList()[0].footPos.position;
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_DeathBringer_ActiveSkill_1");
            yield return new WaitForSeconds(0.8f);


            foreach (var targetUnit in e.targetUnits)
            {
                // 대상에게 피해를 줍니다.
                e.actionUnit.HitTarget(targetUnit, skillDamage, false);

                // 대상에게 연결된 유닛에게 피해를 줍니다.
                foreach (var linkedUnit in targetUnit.CurrentGrid.GetLinkedGrids(false).Select(grid => grid.CurrentBattleUnit))
                {
                    e.actionUnit.HitTarget(linkedUnit, nextDamage, false);
                }
            }

            yield return new WaitForSeconds(0.7f);
            e.actionUnit.isSkillUsing = false;
        }
    }
}