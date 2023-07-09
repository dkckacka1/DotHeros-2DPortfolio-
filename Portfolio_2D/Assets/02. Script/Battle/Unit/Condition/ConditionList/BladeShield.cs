using Portfolio.Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  지속형 상태이상인 칼날 방패 상태이상 클래스
 */

namespace Portfolio.condition
{
    public class BladeShield : ContinuationCondition
    {
        public BladeShield(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // 전투 유닛의 피격시 이벤트에 구독
            unit.OnTakeDamagedEvent += Unit_OnTakeDamagedEvent;
        }


        public override void UnApplyCondition(BattleUnit unit)
        {
            // 전투 유닛의 피격시 이벤트에 구독 해제
            unit.OnTakeDamagedEvent -= Unit_OnTakeDamagedEvent;
        }

        private void Unit_OnTakeDamagedEvent(object sender, TakeDamageEventArgs e)
        {
            // 피해를 입었을 때 타격한 유닛에게 입은 데미지의 반절 데미지로 타격한다.
            // 이 공격으로는 피격 이벤트가 호출 되지 않는다.
            e.hitUnit.TakeDamage(e.damage / 2, e.hitUnit ,false, false);
        }
    }

}