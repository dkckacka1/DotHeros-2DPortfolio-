using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/*
 * 전투중 사용할 액티브 스킬의 추상 클래스
 */

namespace Portfolio.skill
{
    public abstract class ActiveSkill : Skill
    {
        // 액티브 스킬의 데이터를 가져온다. 부모 클래스의 GetData를 호출하면 SkillData를 호출하기에 new 키워드를 통해 부모클래스의 GetData 메서드를 숨긴다.
        public new ActiveSkillData GetData { get => (this.skillData as ActiveSkillData); }
        public virtual int GetActiveSkillCooltime(int skillLevel) => GetData.skillCoolTime; // 스킬 쿨타임을 가져온다.

        public ActiveSkill(ActiveSkillData skillData) : base(skillData)
        {
        }
        public override void Action(object sender, SkillActionEventArgs e)
        {
            base.Action(sender, e);
            // 스킬을 사용한다.
            e.actionUnit.isSkillUsing = true;
            // 스킬 사용 연출 및 로직을 수행한다.
            e.actionUnit.StartCoroutine(PlaySkill(e));
        }

        // 액티브 스킬을 사용할 때 로그에 표시될 로그 텍스트
        protected override string GetLogString(SkillActionEventArgs e)
        {
            string playerUnit = string.Empty;

            if (!e.actionUnit.IsEnemy)
                // 사용 유닛이 플레이어면
            {
                // 녹색 이름
                playerUnit = $"<color=green>[{e.actionUnit.name}]</color>";
            }
            else
                // 적이면
            {
                // 적색 이름
                playerUnit = $"<color=red>[{e.actionUnit.name}]</color>";
            }

            string targetUnit = "";

            if (e.targetUnits.Count() > 1)
                // 타겟 유닛이 여러명이면
            {
                if (e.targetUnits.Any(unit => !unit.IsEnemy))
                    // 하나라도 플레이어 유닛이면
                {
                    // 녹색 이름
                    targetUnit = $"<color=green>[아군들]</color>";
                }
                else
                // 하나라도 플레이어 유닛이 아니면
                {
                    // 적색 이름
                    targetUnit = $"<color=red>[적군들]</color>";
                }
            }
            else
            {
                if (!e.targetUnits.First().IsEnemy)
                    // 타겟유닛이 한명이고 플레이어 유닛이면
                {
                    // 녹색 이름
                    targetUnit = $"<color=green>[{e.targetUnits.First().name}]</color>";
                }
                else
                    // 아니면
                {
                    // 적색 이름
                    targetUnit = $"<color=red>[{e.targetUnits.First().name}]</color>";
                }
            }

            // 위 정보로 로그 출력
            string log = $"{playerUnit}이(가) {targetUnit}에게 [{skillData.skillName}]을(를) 사용!";

            return log;
        }

        // 액티브 스킬은 타겟을 설정해야한다.
        public abstract IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits);

        // 액티브 스킬은 로직과 연출을 구현해야한다.
        protected abstract IEnumerator PlaySkill(SkillActionEventArgs e);
    }
}