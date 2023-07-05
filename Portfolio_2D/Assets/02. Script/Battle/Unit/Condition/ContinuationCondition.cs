using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �����̻��� �߻� Ŭ����
 */

namespace Portfolio.condition
{
    public abstract class ContinuationCondition : Condition
    {
        protected ContinuationCondition(ConditionData conditionData) : base(conditionData)
        {
        }

        // �����̻��� ������ �� ȣ���� ���� ���� �Լ�
        public abstract void UnApplyCondition(BattleUnit unit);
    }

}