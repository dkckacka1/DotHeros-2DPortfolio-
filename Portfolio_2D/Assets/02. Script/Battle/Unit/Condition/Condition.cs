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
        public int conditionID => conditionData.ID;
        public bool IsBuff => conditionData.isBuff;
        public bool IsOverlap => conditionData.isOverlaping;
        public bool IsReset => conditionData.isResetCount;
        public Sprite conditionIcon;

        public Condition(ConditionData conditionData)
        {
            this.conditionData = conditionData;
            conditionIcon = GameManager.Instance.GetSprite(conditionData.conditionIconSpriteName);
        }


        public abstract void ApplyCondition(BattleUnit unit);
    }
}

