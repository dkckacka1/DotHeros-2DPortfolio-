using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * �ƹ�Ÿ ������ ��Ƽ�� ��ų 2 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_AVATA_ActiveSkill_2 : ActiveSkill
    {
        public Skill_AVATA_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        // ��ų ������ ���� ��Ÿ���� �����ϱ⿡ �θ� Ŭ������ ��ų ��Ÿ���� �����ϴ� �Լ��� ������ ���ش�.
        public override int GetActiveSkillCooltime(int skillLevel)
        {
            return base.GetActiveSkillCooltime(skillLevel) - skillLevel;
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // ü���� ���� ���� �������� 5�� Ÿ��
            return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            // �ڽ����� ����Ʈ ���
            SkillEffect effect = BattleManager.ObjectPool.SpawnSkillEffect();
            effect.PlayEffect("Anim_Skill_Effect_AVATA_ActiveSkill2");
            effect.transform.position = e.actionUnit.transform.position;

            int manaValue = 0;

            foreach (var targetUnit in e.targetUnits)
            {
                if (targetUnit.IsEffectHit(e.actionUnit.EffectHit))
                    // ��󿡰� ȿ�� ���� �Ǵ��� �ϰ� ����������
                {
                    // ���� ȸ�� ��ġ ����
                    manaValue++;
                }
            }

            // ���� ȸ��
            if (manaValue > 0)
                e.actionUnit.AddMana(manaValue);

            yield return new WaitForSeconds(0.5f);

            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }

}