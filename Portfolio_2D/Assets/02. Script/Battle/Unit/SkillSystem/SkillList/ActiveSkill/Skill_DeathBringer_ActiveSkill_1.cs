using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �������� ������ ��Ƽ�� ��ų 1 Ŭ����
 * ������ �� �Ѹ��� �����Ͽ� �����ϴµ� �� ���� ����� ���ֵ鵵 �������� 50%�� �Դ´�.
 */

namespace Portfolio.skill
{
    public class Skill_DeathBringer_ActiveSkill_1 : ActiveSkill
    {
        // TODO : �����긵�� ��Ƽ�� ��ų 1 �����ؾ���
        public Skill_DeathBringer_ActiveSkill_1(ActiveSkillData skillData) : base(skillData)
        {
        }

        public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator PlaySkill(SkillActionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}