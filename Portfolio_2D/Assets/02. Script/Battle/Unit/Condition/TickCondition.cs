using System;
using System.Collections;
using System.Collections.Generic;

/*
 * ƽ�� �����̻��� �߻� Ŭ����
 */

namespace Portfolio.condition
{
    public abstract class TickCondition : Condition
    {
        protected TickCondition(ConditionData conditionData) : base(conditionData)
        {
        }
    }
}