using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태이상 시스템
namespace Portfolio.Condition
{
    public abstract class AbnormalCondition
    {
        public bool isBuff = true; // 벼프형 상태이상인지 디버프형 상태이상인지
        public bool isOverlaping;
        public bool isSumCount;
        public abstract void ApplyCondition(BattleUnit unit);
    }
}

