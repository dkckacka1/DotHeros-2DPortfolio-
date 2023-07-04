using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 전투 유닛의 상태이상을 표시해주는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitConditionUI : MonoBehaviour
    {
        [SerializeField] Image conditionIcon;                   // 상태이상 이미지 UI
        [SerializeField] TextMeshProUGUI conditionCountText;    // 상태이상의 남은 턴 표시 텍스트
        [SerializeField] TextMeshProUGUI overlapCountText;      // 상태이상 중첩 표시 텏스트

        // 상태이상 표시
        public void ShowCondition(Condition condition)
        {
            conditionIcon.sprite = condition.conditionIcon;
        }

        // 남은 턴 표시
        public void SetCount(int count)
        {
            conditionCountText.text = count.ToString();
        }

        // 중첩 표시
        public void SetOverlapCount(int overlapCount)
        {
            overlapCountText.text = overlapCount.ToString();
            // 만일 중첩수가 1 초과하면 중첩 텍스트 표시
            if (overlapCount > 1)
            {
                overlapCountText.gameObject.SetActive(true);
            }
            else
            {
                overlapCountText.gameObject.SetActive(false);
            }
        }
    }

}