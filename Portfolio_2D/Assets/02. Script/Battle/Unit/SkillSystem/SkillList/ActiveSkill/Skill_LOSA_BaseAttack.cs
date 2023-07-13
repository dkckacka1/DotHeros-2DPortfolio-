using Portfolio.Battle;
using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� �λ��� �⺻ ���� ��ų Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LOSA_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_LOSA_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ü���� ���� ���� �� ������ 1�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų ������
            float skillDamage = e.actionUnit.AttackPoint * 0.8f;

            foreach (var targetUnit in e.targetUnits)
            {
                // ����� Ÿ��
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // ��󿡰� ���� �����̻� �ο�
                targetUnit.AddCondition(conditionList[0].conditionID, conditionList[0], 2);
                // ����Ʈ ���
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LOSA_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}

