using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���ֿ� ����� �����̻��� �����ϴ� Ŭ����
 */

namespace Portfolio.condition
{
    public class ConditionSystem
    {
        private int count;                          // �����̻��� ���ӽð�
        private int resetCountValue;                // �����̻��� �ʱ�ȭ �����ϴٸ� �ʱ�ȭ�� ���ӽð�
        private int overlapingCount = 1;            // �����̻� ��ø��
        private Condition condition;                // ����� �����̻�
        private BattleUnitConditionUI conditionUI;  // ���� ���ֿ� ǥ��� �����̻� UI
        public int Count
        {
            get
            {
                return count;
            }

            private set
            {
                // ���ӽð��� ����Ǹ� �����̻� UI�� ������Ʈ ���ش�.
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
                // �߫����� ����Ǹ� �����̻� UI�� ������Ʈ ���ش�.
                overlapingCount = value;
                conditionUI.SetOverlapCount(overlapingCount);
            }
        }
        public Condition Condition { get => condition; }
        public BattleUnitConditionUI ConditionUI { get => conditionUI; }

        public bool isBuff { get => condition.IsBuff; }
        public bool isOverlap { get => condition.IsOverlap; }
        public bool isResetCount { get => condition.IsReset; }
        public bool isCountEnd => count == 0; // ���ӽð��� 0����

        // ���� ���ֿ� �����̻��� �ɸ��� ȣ���Ѵ�.
        public ConditionSystem(int count, Condition condition, BattleUnitConditionUI conditionUI)
        {
            this.count = count;
            this.resetCountValue = count;
            this.condition = condition;
            this.conditionUI = conditionUI;

            conditionUI.SetCount(this.count);
            conditionUI.SetOverlapCount(this.overlapingCount);
        }

        // �����̻� ���ӽð� �ʱ�ȭ�Ѵ�.
        public void ResetCount()
        {
            Count = resetCountValue;
        }

        // ���ӽð��� ����Ѵ�.
        public void CountDown()
        {
            Count--;
        }

        // �����̻��� ���� ��� UI�� �����ش�.
        public void EndCondition()
        {
            conditionUI.gameObject.SetActive(false);
            Count = 0;
            OverlapingCount = 0;
        }


        // �����̻� ��ø���ش�.
        public void AddOverlap()
        {
            OverlapingCount++;
        }
    }
}