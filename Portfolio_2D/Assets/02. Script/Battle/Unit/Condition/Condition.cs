using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태이상 시스템
namespace Portfolio.condition
{
    public abstract class Condition
    {
        private ConditionData conditionData;
        public ConditionData ConditionData { get => conditionData; }

        public Condition(ConditionData conditionData)
        {
            this.conditionData = conditionData;
        }


        public abstract void ApplyCondition(BattleUnit unit);
    }
}

