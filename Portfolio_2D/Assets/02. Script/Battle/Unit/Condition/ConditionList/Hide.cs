using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  지속형 상태이상인 은신의 상태이상 클래스
 *  전투 유닛에 영향을 주지 않기 때문에 코드 내용이 없다.
 */

namespace Portfolio.condition
{
    public class Hide : ContinuationCondition
    {
        public Hide(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
        }
    }
}