using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class Skill_GWEN_ActiveSkill_1 : ActiveSkill
    {
        public Skill_GWEN_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // 적들에서 전열에서 후열순으로 체력이가장 낮은 순서중에 가장 앞 타겟
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderFrontLineNLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // 스킬 데미지는 스킬레벨의 10%
            float skillDamage = e.actionUnit.AttackPoint * (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            foreach (var targetUnit in e.targetUnits)
            {
                for (int i = 0; i < 7; i++)
                    // 7회 타격
                {
                    yield return new WaitForSeconds(0.15f);
                    // 이펙트 생성
                    var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                    effect.PlayEffect("Anim_Skill_Effect_GWEN_ActiveSkill_1");
                    if(targetUnit.IsEnemy)
                        // 맞는 적이 플레이어냐에 따라 방향 조절
                    {
                        effect.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        effect.transform.localScale = new Vector3(-1, 1, 0);
                    }
                    // 이펙트가 같은 곳에만 출력 되지 않도록 랜덤한 값을 입력
                    Vector3 effectPos = new Vector3(targetUnit.transform.position.x, targetUnit.transform.position.y + Random.Range(-1f,1f));
                    effect.transform.position = effectPos;
                    e.actionUnit.HitTarget(targetUnit, skillDamage);
                }
            }
            yield return new WaitForSeconds(1f);
            // 스킬 종료
            e.actionUnit.isSkillUsing = false;
        }
    }
}