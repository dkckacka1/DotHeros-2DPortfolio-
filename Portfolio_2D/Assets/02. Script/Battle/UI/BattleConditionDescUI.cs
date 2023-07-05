using Portfolio.condition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 스킬의 상태이상 설명을 보여주는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleConditionDescUI : MonoBehaviour
    {
        [SerializeField] Image conditionIconImage;              // 상태이상 아이콘 이미지
        [SerializeField] TextMeshProUGUI conditionNameText;     // 상태이상 이름 텍스트
        [SerializeField] TextMeshProUGUI conditionDescText;     // 상태이상 설명 텍스트
        [SerializeField] TextMeshProUGUI conditionBuffText;     // 상태이상 버프 디버프 여부 텍스트
        [SerializeField] TextMeshProUGUI conditionOverlaptext;  // 상태이상 중첩 여부 텍스트
        [SerializeField] TextMeshProUGUI conditionResetText;    // 상태이상 초기화 여부 텍스트

        // 상태이상 설명을 보여준다.
        public void Show(Condition condition)
        {
            if (condition == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                conditionIconImage.sprite = condition.conditionIcon;
                conditionNameText.text = condition.ConditionName;
                conditionDescText.text = condition.ConditionDesc;
                conditionBuffText.text = (condition.IsBuff ? "버프" : "디버프");
                conditionOverlaptext.text = (condition.IsOverlap ? "중첩 가능" : "중첩 불가능");
                conditionResetText.text = (condition.IsReset ? "초기화 가능" : "초기화 불가능");

                this.gameObject.SetActive(true);
            }
        }
    }
}