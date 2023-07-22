using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ��Ƽ�� ��ų 2 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LILY_ActiveSkill_2 : ActiveSkill
    {
        public Skill_LILY_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // �Ʊ����� ü���� ���� ������ 5��
            return targetUnits.GetAllyTarget(actionUnit).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LILY_ActiveSkill_Heal");
            foreach (var targetUnit in e.targetUnits)
            {
                // ü�� ȸ�� ��ġ�� ��� ü���� ��� ��ġ
                float healValue = targetUnit.MaxHP * (0.25f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                // ����� ü���� ȸ��
                e.actionUnit.HealTarget(targetUnit, healValue);
                // ��󿡰� ���� ���� �����̻� �ο�
                targetUnit.AddCondition(1003, conditionList[0], 2);
            }
            yield return new WaitForSeconds(0.5f);
            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}