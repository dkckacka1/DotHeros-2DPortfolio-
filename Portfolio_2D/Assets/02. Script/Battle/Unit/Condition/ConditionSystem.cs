using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 전투 유닛에 적용된 상태이상을 관리하는 클래스
 */

namespace Portfolio.condition
{
    public class ConditionSystem
    {
        private int count;                          // 상태이상의 지속시간
        private int resetCountValue;                // 상태이상이 초기화 가능하다면 초기화될 지속시간
        private int overlapingCount = 1;            // 상태이상 중첩량
        private Condition condition;                // 적용될 상태이상
        private BattleUnitConditionUI conditionUI;  // 전투 유닛에 표기될 상태이상 UI
        public int Count
        {
            get
            {
                return count;
            }

            private set
            {
                // 지속시간이 변경되면 상태이상 UI도 업데이트 해준다.
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
                // 중쳡량이 변경되면 상태이상 UI도 업데이트 해준다.
                overlapingCount = value;
                conditionUI.SetOverlapCount(overlapingCount);
            }
        }
        public Condition Condition { get => condition; }
        public BattleUnitConditionUI ConditionUI { get => conditionUI; }

        public bool isBuff { get => condition.IsBuff; }
        public bool isOverlap { get => condition.IsOverlap; }
        public bool isResetCount { get => condition.IsReset; }
        public bool isCountEnd => count == 0; // 지속시간이 0인지

        // 전투 유닛에 상태이상이 걸리면 호출한다.
        public ConditionSystem(int count, Condition condition, BattleUnitConditionUI conditionUI)
        {
            this.count = count;
            this.resetCountValue = count;
            this.condition = condition;
            this.conditionUI = conditionUI;

            conditionUI.SetCount(this.count);
            conditionUI.SetOverlapCount(this.overlapingCount);
        }

        // 상태이상 지속시간 초기화한다.
        public void ResetCount()
        {
            Count = resetCountValue;
        }

        // 지속시간이 경과한다.
        public void CountDown()
        {
            Count--;
        }

        // 상태이상이 끝날 경우 UI를 없애준다.
        public void EndCondition()
        {
            conditionUI.gameObject.SetActive(false);
            Count = 0;
            OverlapingCount = 0;
        }


        // 상태이상 중첩해준다.
        public void AddOverlap()
        {
            OverlapingCount++;
        }
    }
}