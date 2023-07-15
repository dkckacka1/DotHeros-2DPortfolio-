using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * �������� ������ ��Ƽ�� ��ų 1 Ŭ����
 * ������ ������ �Ѹ��� �����Ͽ� �ڽ��� ���ݷ��� (80 +{0})% ��ŭ �������� �����ϴ�. �׸��� ����� ���ֿ��Ե�  ������ �������� �����ϴ�.
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
            // 1. ���鸸 Ÿ���մϴ�.
            // 2. ���߿� �Ѹ� �����մϴ�.
            var list = new List<GridPosition>(1) { targetUnits.GetEnemyTarget(actionUnit, this).GetRandomTarget() };
            return list.SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų �������� 80% + ��ų���� * 20%
            float skillDamage = e.actionUnit.AttackPoint * (0.8f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
            // ����� ���ֿ��� �� �������� ��ų�������� ����
            float nextDamage = skillDamage / 2;

            // ��ų ����Ʈ �����ֱ�
            var effect = BattleManager.ObjectPool.SpawnSkillEffect();
            effect.PlayEffect("Anim_Skill_Effect_DeathBringer_ActiveSkill_1");
            effect.transform.position = e.targetUnits.ToList()[0].footPos.position;
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_DeathBringer_ActiveSkill_1");
            yield return new WaitForSeconds(0.8f);


            foreach (var targetUnit in e.targetUnits)
            {
                // ��󿡰� ���ظ� �ݴϴ�.
                e.actionUnit.HitTarget(targetUnit, skillDamage, false);

                // ��󿡰� ����� ���ֿ��� ���ظ� �ݴϴ�.
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