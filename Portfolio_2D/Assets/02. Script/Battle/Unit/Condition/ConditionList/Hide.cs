using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ������ �����̻��� ������ �����̻� Ŭ����
 *  ���� ���ֿ� ������ ���� �ʱ� ������ �ڵ� ������ ����.
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