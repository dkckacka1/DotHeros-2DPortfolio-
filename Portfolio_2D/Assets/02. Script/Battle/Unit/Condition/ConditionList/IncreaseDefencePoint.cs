using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �����̻��� ���� ���� �����̻� Ŭ����
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
            // ������ ���� ��Ų��.
            unit.DefencePoint *= 1.3f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // ������ �����Ѵ�.
            unit.DefencePoint /= 1.3f;
        }
    }
}