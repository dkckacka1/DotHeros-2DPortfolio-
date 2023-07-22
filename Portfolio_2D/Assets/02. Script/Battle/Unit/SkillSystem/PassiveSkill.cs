using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 패시브 스킬을 정의할 추상 클래스
 */

namespace Portfolio.skill
{
    public abstract class PassiveSkill : Skill
    {
        // 패시브 스킬의 데이터를 가져온다. 부모 클래스의 GetData를 호출하면 SkillData를 호출하기에 new 키워드를 통해 부모클래스의 GetData 메서드를 숨긴다.
        public new PassiveSkillData GetData { get => (this.skillData as PassiveSkillData); }

        public PassiveSkill(PassiveSkillData skillData) : base(skillData)
        {
        }

        // 패시브 스킬을 사용할 때 로그에 표시될 로그 텍스트
        protected override string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
                // 사용 유닛이 플레이어 유닛이면
            {
                // 녹색 이름
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
                // 아니면
            {
                // 적색 이름
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            // 위 정보로 로그 출력
            string log = $"{playerUnit}의 패시브 스킬[{GetData.skillName}] 발동!";

            return log;
        }

        // 패시브 스킬은 타겟을 설정해야 한다.
        public abstract void SetPassiveSkill(SkillActionEventArgs e);
    }
}