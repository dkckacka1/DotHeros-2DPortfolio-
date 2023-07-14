using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 데스블링어 유닛의 액티브 스킬 1 클래스
 * 랜덤한 적 한명을 선택하여 공격하는데 그 주위 연결된 유닛들도 데미지의 50%를 입는다.
 */

namespace Portfolio.skill
{
    public class Skill_DeathBringer_ActiveSkill_1 : ActiveSkill
    {
        // TODO : 데스브링어 액티브 스킬 1 구현해야함
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