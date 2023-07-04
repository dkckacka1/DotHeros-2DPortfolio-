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
            // ���鿡�� �������� �Ŀ������� ü���̰��� ���� �����߿� ���� �� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderFrontLineNLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų �������� ��ų������ 10%
            float skillDamage = e.actionUnit.AttackPoint * (e.skillLevel * GetData.skillLevelValue_1 * 0.01f);
            foreach (var targetUnit in e.targetUnits)
            {
                for (int i = 0; i < 7; i++)
                    // 7ȸ Ÿ��
                {
                    yield return new WaitForSeconds(0.15f);
                    // ����Ʈ ����
                    var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                    effect.PlayEffect("Anim_Skill_Effect_GWEN_ActiveSkill_1");
                    if(targetUnit.IsEnemy)
                        // �´� ���� �÷��̾�Ŀ� ���� ���� ����
                    {
                        effect.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        effect.transform.localScale = new Vector3(-1, 1, 0);
                    }
                    // ����Ʈ�� ���� ������ ��� ���� �ʵ��� ������ ���� �Է�
                    Vector3 effectPos = new Vector3(targetUnit.transform.position.x, targetUnit.transform.position.y + Random.Range(-1f,1f));
                    effect.transform.position = effectPos;
                    e.actionUnit.HitTarget(targetUnit, skillDamage);
                }
            }
            yield return new WaitForSeconds(1f);
            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}