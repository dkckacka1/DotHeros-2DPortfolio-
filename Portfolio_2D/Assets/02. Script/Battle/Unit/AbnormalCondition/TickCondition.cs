using System;
using System.Collections;
using System.Collections.Generic;

namespace Portfolio.condition
{
    public abstract class TickCondition : Condition
    {
        protected TickCondition(ConditionData conditionData) : base(conditionData)
        {
        }
    }
}