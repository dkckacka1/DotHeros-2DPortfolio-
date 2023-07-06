using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Selector = Portfolio.UI.UnitSlotSelector_UserPortraitChange; // 셀렉터 이름이 너무 길어서 별명 사용


/*
 *  유저 이미지 변경 팝업 UI 클래스
 */

namespace Portfolio.UI
{
    public class UserPortraitChangePopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect portraitScrollView;             // 유저 포트레이트 이미지 스크롤 뷰

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();   // 유저 유닛 포트레이트 리스트

        Selector currentChoice;                   // 현재 사용중인 유저 선택 포트레이트
        Selector selectPortrait;                  // 현재 선택한 유저 선택 포트레이트

        public void Awake()
        {
            // 초기 세팅
            foreach (var slot in portraitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(slot);
            }
            this.gameObject.SetActive(false);
        }

        // 팝업창이 꺼지면 유저가 선택한 이미지를 초기화 해준다.
        private void OnDisable()
        {
            selectPortrait = null;
            foreach (var slot in unitSlotUIList)
            {
                slot.GetComponent<Selector>().UnSelect();
            }
        }

        // 팝업창을 표시한다.
        public void Show()
        {
            this.gameObject.SetActive(true);
            // 유저가 가지고있는 중복 없는 유닛 리스트
            var collectList = GameManager.CurrentUser.GetUserCollectUnitList();
            // 현재 사용중인 유저 포트레이트 이미지
            string currentPortraitName = GameManager.CurrentUser.UserPortrait.name;
            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                // 유저가 가지고 있는 유닛 슬롯만 표시
                if (collectList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // 유닛 슬롯 세팅
                unitSlotUIList[i].Init(collectList[i], false, false);
                if (unitSlotUIList[i].CurrentUnit.portraitSprite.name == currentPortraitName)
                    // 현재 유닛 슬롯 이미지가 유저 이미지 포트레이트 이름과 같다면
                {
                    // 현재 사용중 이미지 표시
                    currentChoice = unitSlotUIList[i].GetComponent<UnitSlotSelector_UserPortraitChange>();
                    currentChoice.CurrentChoice();
                }

                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        // 유저 포트레이트 이미지를 선택한다.
        public void SelectPortrait(Selector userProfilPortraitSelector)
        {
            if (selectPortrait != null)
                // 기존 선택한 이미지가 있다면 선택 취소 해준다.
            {
                selectPortrait.UnSelect();
            }

            // 새롭게 선택한 이미지를 선택한다.
            selectPortrait = userProfilPortraitSelector;
            selectPortrait.UserSelect();
        }

        // 선택한 유저 포트레이트 이미지를 변경한다.
        public void ChangePortrait()
        {
            // 기존 사용중인 이미지 포트레이트를 취소한다.
            currentChoice.ChangeChoice();
            // 유저 포트레이트 이미지를 현재 선택한 이미지 포트레이트로 변경한다.
            GameManager.CurrentUser.UserPortrait = selectPortrait.GetCurrentSprite;
            // 유저 정보를 업데이트한다.
            GameManager.UIManager.ShowUserInfo();
            // 유저 데이터를 저장한다.
            GameManager.Instance.SaveUser();
            // 팝업창을 꺼준다.
            this.gameObject.SetActive(false);
        }
    }

}