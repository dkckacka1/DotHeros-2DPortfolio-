using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����̻� �ý���
namespace Portfolio.Condition
{
    public abstract class AbnormalCondition
    {
        public bool isBuff = true; // ������ �����̻����� ������� �����̻�����
        public bool isOverlaping;
        public bool isSumCount;
        public abstract void ApplyCondition(BattleUnit unit);
    }
}

