using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class ConditionSystem
    {
        private int count;
        private int resetCountValue;
        private int overlapingCount = 1;
        private Condition condition;
        private BattleUnitConditionUI conditionUI;
        public int Count
        {
            get
            {
                return count;
            }

            private set
            {
                count = value;
                conditionUI.SetCount(count);
            }
        }
        public int OverlapingCount
        {
            get
            {
                return overlapingCount;
            }
            private set
            {
                overlapingCount = value;
                conditionUI.SetOverlapCount(overlapingCount);
            }
        }
        public Condition Condition { get => condition; }
        public BattleUnitConditionUI ConditionUI { get => conditionUI; }

        public bool isBuff { get => condition.IsBuff; }
        public bool isOverlap { get => condition.IsOverlap; }
        public bool isResetCount { get => condition.IsReset; }

        public ConditionSystem(int count, Condition condition, BattleUnitConditionUI conditionUI)
        {
            this.count = count;
            this.resetCountValue = count;
            this.condition = condition;
            this.conditionUI = conditionUI;

            conditionUI.SetCount(this.count);
            conditionUI.SetOverlapCount(this.overlapingCount);
        }

        public void ResetCount()
        {
            Count = resetCountValue;
        }

        public void CountDown()
        {
            Count--;
        }

        public void EndCondition()
        {
            conditionUI.isActive = false;
            conditionUI.gameObject.SetActive(false);
        }

        public bool isCountEnd()
        {
            return count == 0;
        }

        public void AddOverlap()
        {
            OverlapingCount++;
        }
    }
}