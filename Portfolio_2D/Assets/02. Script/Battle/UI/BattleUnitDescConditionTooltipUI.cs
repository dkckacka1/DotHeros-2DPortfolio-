using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 전투 유닛 설명창에서 상태이상 아이콘에 마우스 포인터를 올릴경우 표시될 상태이상 설명 툴팁 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitDescConditionTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI conditionNameText;             // 상태이상 이름 텍스트
        [SerializeField] TextMeshProUGUI conditionDescText;             // 상태이상 설명 텍스트 
        [SerializeField] TextMeshProUGUI conditionOverlapCountText;     // 상태이상 중첩 횟수 텍스트 
        [SerializeField] TextMeshProUGUI conditionRemainTurnText;       // 상태이상 남은 지속 시간 텍스트 

        private void OnDisable()
        {
            // 만약 툴팁이 표시되는 도중 전투 유닛 상태 팝업창이 닫힐 경우를 대비하여 꺼둔다.
            this.gameObject.SetActive(false);
        }

        // 현재 걸린 상태이상을 툴팁을 표시한다.
        public void Show(ConditionSystem conditionSystem)
        {
            conditionNameText.text = conditionSystem.Condition.ConditionName;
            conditionDescText.text = conditionSystem.Condition.ConditionDesc;
            conditionOverlapCountText.text = (conditionSystem.isOverlap ? $"중첩 횟수 : {conditionSystem.OverlapingCount}" : "중첩 불가능");
            conditionRemainTurnText.text = $"남은 지속시간 : {conditionSystem.Count}";

            this.gameObject.SetActive(true);
        }

        // 툴팁을 숨겨준다.
        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}