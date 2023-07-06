using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유저 포트레이트 이미지를 변경하는 셀렉터
 * 유닛 슬롯 UI가있는 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.UI
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_UserPortraitChange : MonoBehaviour
    {
        [SerializeField] RectTransform currentChoice;   // 현재 사용중임을 표시하는 오브젝트
        [SerializeField] RectTransform userSelect;      // 유저가 선택함을 표시하는 오브젝트

        public Sprite GetCurrentSprite => GetComponent<UnitSlotUI>().CurrentUnit.portraitSprite;
        public bool IsCurrentChoice => currentChoice.gameObject.activeInHierarchy;
        public bool IsUserSelect => userSelect.gameObject.activeInHierarchy;

        // 현재 사용중을 표시한다.
        public void CurrentChoice()
        {
            currentChoice.gameObject.SetActive(true);
        }

        // 사용을 취소한다.
        public void ChangeChoice()
        {
            currentChoice.gameObject.SetActive(false);
        }

        // 유저가 선택한 이미지를 표시한다.
        public void UserSelect()
        {
            userSelect.gameObject.SetActive(true);
        }

        // 유저가 선택을 취소한다.
        public void UnSelect()
        {
            userSelect.gameObject.SetActive(false);
        }

        // 유저가 이미지를 선택한다.
        public void BTN_OnClick_Select(UserPortraitChangePopupUI popupUI)
        {
            if (IsCurrentChoice) return; // 이미 선택한 거라면 리턴
            if (IsUserSelect) return; // 이미 선택한 거라면 리턴

            // 유저가 선택한거를 UserPortraitChangePopupUI에 알려준다.
            popupUI.SelectPortrait(this);
            UserSelect();
        }
    }
}