using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 지속형 상태이상인 방어력 증가 상태이상 클래스
 */

namespace Portfolio.condition
{
    public class IncreaseDefencePoint : ContinuationCondition
    {
        public IncreaseDefencePoint(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // 방어력을 증가 시킨다.
            unit.DefencePoint *= 1.3f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // 방어력을 복구한다.
            unit.DefencePoint /= 1.3f;
        }
    }
}