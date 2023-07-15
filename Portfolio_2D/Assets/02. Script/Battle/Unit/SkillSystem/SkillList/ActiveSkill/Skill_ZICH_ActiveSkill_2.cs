using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��ũ�� ��Ƽ�� ��ų 2 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_ZICH_ActiveSkill_2 : ActiveSkill
    {
        public Skill_ZICH_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // �� �ִ� 5����� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            float skillDamage = e.actionUnit.AttackPoint + (e.actionUnit.AttackPoint * (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_ZICH_ActiveSkill_2");
            foreach (var targetUnit in e.targetUnits)
            {
                // ����� Ÿ���Ѵ�.
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