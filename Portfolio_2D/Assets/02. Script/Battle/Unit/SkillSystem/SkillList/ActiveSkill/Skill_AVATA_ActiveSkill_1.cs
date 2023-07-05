using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ƹ�Ÿ ������ ��Ƽ�� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_1 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ü���� ���� ���� �������� 5�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // ��ų ������
            float skillDamage = e.actionUnit.AttackPoint * (0.5f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));

            foreach (var targetUnit in e.targetUnits)
            {
                // Ÿ�ٿ��� ���ظ� ������.
                e.actionUnit.HitTarget(targetUnit, skillDamage);
                // Ÿ�ٿ��� �Ҿ��� �����̻��� �ο��Ѵ�.
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], 3);
                // ��ų ����Ʈ�� Ÿ���� �߹ؿ��� ����Ѵ�.
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_AVATA_ActiveSkill1");
                effect.transform.position = targetUnit.footPos.position;
            }

            yield return new WaitForSeconds(1f);

            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}