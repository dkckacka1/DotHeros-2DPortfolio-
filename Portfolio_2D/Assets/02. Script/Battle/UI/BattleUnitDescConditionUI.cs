using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 전투 유닛 설명 팝업창에 표시될 현재 걸려있는 상태이상 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescConditionUI : MonoBehaviour
    {
        [SerializeField] Image conditionIconImage; // 상태이상 이미지

        ConditionSystem condition;    // 걸린 상태이상

        // 걸린 상태이상을 보여준다.
        public void Show(ConditionSystem condition)
        {
            if (condition != null)
            {
                this.condition = condition;
                conditionIconImage.sprite = condition.Condition.conditionIcon;
                this.gameObject.SetActive(true);
            }
            else
            {
                this.condition = null;
                this.gameObject.SetActive(false);
            }
        }

        // 포인터가 위로 올라갔을때 툴팁을 표시한다.
        public void TRIGGER_OnPointerEnter_ShowTooltip(BattleUnitDescConditionTooltipUI tooltip)
        {
            tooltip.Show(condition);
        }

        // 포인터가 위로 올라갔을때 툴팁을 숨긴다.
        public void TRIGGER_OnPointerExit_HideTooltip(BattleUnitDescConditionTooltipUI tooltip)
        {
            tooltip.Hide();
        }
    }
}