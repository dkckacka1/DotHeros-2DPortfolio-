using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ���� ��ũ�� �⺻ ���� ��ų Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_ZICH_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ü���� ���� ���� �� ������ 1�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(GetData.targetNum).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint;
            foreach (var targetUnit in e.targetUnits)
            {
                // ��� Ÿ��
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // ��� ��ġ�� ����Ʈ ���
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_ZICH_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(0.5f);
            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}