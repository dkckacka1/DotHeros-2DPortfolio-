using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * 전투 진행을 알려주는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleSequenceUI : MonoBehaviour
    {
        [SerializeField] Image sequenceUI;                      // 턴 진행 바
        [SerializeField] BattleUnitSequenceUI sequenceUIObject; // 진행 UI 오브젝트

        private float sequenceUIHeight;                         // 턴 진행바의 높이
        private float unitSequenceUIHeight;                     // 유닛 턴 진행 UI의 높이
        private float sequenceDefaultYPos;                      // 턴 진행 바에서 유닛 턴 진행 UI가 빠져나가지 않도록 제한

        private void Awake()
        {
            // 턴 진행 바 이미지의 높이
            sequenceUIHeight = sequenceUI.rectTransform.rect.height;
            // 유닛 턴 진행 UI의 높이
            unitSequenceUIHeight = (sequenceUIObject.transform as RectTransform).rect.height;
            // 턴 진행 바에서 유닛 턴 진행 UI가 빠져나가지 않도록 제한
            sequenceDefaultYPos = sequenceUIHeight - unitSequenceUIHeight;
        }

        // 유닛 턴 진행 UI가 턴 진행바위에 위치하도록 표시해준다.
        public void SetSequenceUnitUIYPosition(BattleUnitSequenceUI targetSequenceUI, float normalizedYPos)
        {
            // 유닛 턴 진행 UI가 턴 진행바의 어디에 위치해야하는지 정규화된 y좌표값을 받고
            // 턴 진행바 이미지에 알맞는 위치에 위치하게 한다.
            float yPos = ((1 - normalizedYPos) * sequenceDefaultYPos) * -1;
            (targetSequenceUI.transform as RectTransform).anchoredPosition = new Vector2(0, yPos);
        }
    }

}