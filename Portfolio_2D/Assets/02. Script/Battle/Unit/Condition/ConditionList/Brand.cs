using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �����̻��� ������ �����̻� Ŭ����
 */

namespace Portfolio.condition
{
    public class Brand : ContinuationCondition
    {

        public Brand(ConditionData conditionData) : base(conditionData)
        {
        }

        public override void ApplyCondition(BattleUnit unit)
        {
            // ���� ����
            unit.DefencePoint *= 0.8f;
        }

        public override void UnApplyCondition(BattleUnit unit)
        {
            // ���� ����
            unit.DefencePoint /= 0.8f;
        }
    }
}