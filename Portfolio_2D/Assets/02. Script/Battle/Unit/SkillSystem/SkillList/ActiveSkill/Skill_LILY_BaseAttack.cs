using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ �⺻ ���� ��ų Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LILY_BaseAttack : ActiveSkill, ISingleTarget
    {
        public Skill_LILY_BaseAttack(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ���� ü���� ���� ������ 1�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų ������
            float skillDamage = e.actionUnit.AttackPoint * 0.75f;
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LILY_BaseAttack");

            foreach (var targetUnit in e.targetUnits)
            {
                // ������ ���ظ� ������ ����Ʈ ���
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_LILY_BaseAttack");
                effect.transform.position = targetUnit.transform.position;
            }
            yield return new WaitForSeconds(1f);
            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}