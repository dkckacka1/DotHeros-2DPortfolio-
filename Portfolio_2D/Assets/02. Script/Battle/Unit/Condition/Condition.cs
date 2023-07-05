using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 전투 유닛의 상태이상 클래스
 */

namespace Portfolio.condition
{
    public abstract class Condition
    {
        private ConditionData conditionData;                    // 상태이상 데이터
        public Sprite conditionIcon;                            // 데이터에서 참조할 상태이상 이미지 스프라이트
        public int conditionID => conditionData.ID;
        public string ConditionName => conditionData.conditionName;
        public string ConditionDesc => conditionData.conditionDesc;
        public bool IsBuff => conditionData.isBuff;             // 버프인지
        public bool IsOverlap => conditionData.isOverlaping;    // 중첩 가능한지
        public bool IsReset => conditionData.isResetCount;      // 지속시간 초기화가 가능한지

        public Condition(ConditionData conditionData)
        {
            this.conditionData = conditionData;
            conditionIcon = GameManager.Instance.GetSprite(conditionData.conditionIconSpriteName);
        }

        // 전투유닛에 상태이상 적용한다.
        public abstract void ApplyCondition(BattleUnit unit);
    }
}

