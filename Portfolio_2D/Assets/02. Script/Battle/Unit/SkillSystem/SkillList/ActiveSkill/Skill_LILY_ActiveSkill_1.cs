using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ��Ƽ�� ��ų 1 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_LILY_ActiveSkill_1 : ActiveSkill
    {
        public Skill_LILY_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            // �Ʊ����� ü���� ���� ������ 1�� Ÿ��
            return targetUnits.GetAllyTarget(actionUnit).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            foreach (var targetUnit in e.targetUnits)
            {
                // ü�� ȸ�� ��ġ�� ��� ü���� ��� ��ġ
                float healValue = targetUnit.MaxHP * (0.3f + (e.skillLevel * GetData.skillLevelValue_1 * 0.01f));
                // ����� ü���� ȸ��
                e.actionUnit.HealTarget(targetUnit, healValue);
            }
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_LILY_ActiveSkill_Heal");
            yield return new WaitForSeconds(0.5f);

            // ��ų ����
            e.actionUnit.isSkillUsing = false;
        }
    }
}