using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ �����̻� Ŭ����
 */

namespace Portfolio.condition
{
    public abstract class Condition
    {
        private ConditionData conditionData;                    // �����̻� ������
        public Sprite conditionIcon;                            // �����Ϳ��� ������ �����̻� �̹��� ��������Ʈ
        public int conditionID => conditionData.ID;
        public string ConditionName => conditionData.conditionName;
        public string ConditionDesc => conditionData.conditionDesc;
        public bool IsBuff => conditionData.isBuff;             // ��������
        public bool IsOverlap => conditionData.isOverlaping;    // ��ø ��������
        public bool IsReset => conditionData.isResetCount;      // ���ӽð� �ʱ�ȭ�� ��������

        public Condition(ConditionData conditionData)
        {
            this.conditionData = conditionData;
            conditionIcon = GameManager.Instance.GetSprite(conditionData.conditionIconSpriteName);
        }

        // �������ֿ� �����̻� �����Ѵ�.
        public abstract void ApplyCondition(BattleUnit unit);
    }
}

