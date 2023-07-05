using System;
using System.Collections;
using System.Collections.Generic;

/*
 * 틱형 상태이상의 추상 클래스
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