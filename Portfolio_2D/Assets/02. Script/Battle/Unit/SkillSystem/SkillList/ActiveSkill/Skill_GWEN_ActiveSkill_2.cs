using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ��Ƽ�� ��ų 2 Ŭ����
 */

namespace Portfolio.skill
{
    public class Skill_GWEN_ActiveSkill_2 : ActiveSkill
    {
        public Skill_GWEN_ActiveSkill_2(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> grids)
        {
            // �Ʊ��� Ÿ������ ����
            return grids.GetAllyTarget(actionUnit).GetTargetNum(this).SelectBattleUnit();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_Skill_GWEN_ActiveSkill_2");
            // ��ų ������ ���� ���� �� ����
            int conditionTime = 1 + (e.skillLevel);
            foreach (var targetUnit in e.targetUnits)
            {
                // Ÿ�ٿ��� Į�� ���� �����̻� �ο�
                targetUnit.AddCondition(GetData.conditinID_1, conditionList[0], conditionTime);
                // �� Ÿ���� �߹ؿ��� ����Ʈ �ߵ�
                var effect = BattleManager.ObjectPool.SpawnSkillEffect();
                effect.PlayEffect("Anim_Skill_Effect_GWEN_ActiveSkill_2");
                effect.transform.position = targetUnit.footPos.position;
            }
            yield return new WaitForSeconds(1f);
            e.actionUnit.isSkillUsing = false;
        }
    }

}