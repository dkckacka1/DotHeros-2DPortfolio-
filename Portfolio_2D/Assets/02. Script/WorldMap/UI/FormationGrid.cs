using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * 전투 진형을 설정하기 위한 유닛 슬롯
 */

namespace Portfolio.WorldMap
{
    public class FormationGrid : MonoBehaviour, IDropHandler
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // 들어올 유닛 정보를 보여줄 유닛 슬롯
        [SerializeField] UnitSlotSelector_FormationPopupTarget targetSelector;    // 드래그로 들어온 유닛 슬롯 셀렉터
        [SerializeField] TextMeshProUGUI unitNameText;                  // 유닛 이름 텍스트
        [SerializeField] TextMeshProUGUI emptyLabel;                    // 비어있음을 표시할 텍스트
        [SerializeField] GameObject btnLayout;                          // 들어온 유닛을 제거할 수있는 버튼이 들어있는 레이아웃

        UnitSlotSelector_FormationPopup currentFomationSlotUI;  // 현재 유닛슬롯

        public Unit GetCurrentUnit => unitSlotUI.CurrentUnit;

        // 새로고침합니다.
        public void ReShow()
        {
            ShowUnit(GetCurrentUnit);
        }

        // 유닛 정보를 보여줍니다.
        public void ShowUnit(Unit unit)
        {
            bool isUnit = unit != null;
            // 유닛 여부에 따라 텍스트와 정보를 표기합니다.
            emptyLabel.gameObject.SetActive(!isUnit);
            unitNameText.gameObject.SetActive(isUnit);
            unitSlotUI.gameObject.SetActive(isUnit);

            if (isUnit)
            {
                unitSlotUI.ShowUnit(unit);
                unitNameText.text = unit.UnitName;
            }
        }

        // 이 슬롯위에서 타겟슬롯이 드랍되었다면 드랍된 슬롯의 정보를 입력합니다.
        public void OnDrop(PointerEventData eventData)
        {
            if (targetSelector.IsSelectUnit)
                // 만약 선택된 타겟슬롯이 있다면
            {
                if (currentFomationSlotUI == null)
                    // 해당 진형에 유닛이 존재하지 않는다면
                {
                    // 이 슬롯에 드랍된 정보를 입력한다.
                    currentFomationSlotUI = targetSelector.selectFomationSlotUI;
                    // 선택된 슬롯을 선택한다.
                    currentFomationSlotUI.Select();
                }
                else
                // 기존에 슬롯이 이미 있다면
                {
                    // 기선택된 슬롯을 비선택한다.
                    currentFomationSlotUI.UnSelect();
                    // 이 슬롯에 드랍된 정보를 입력한다.
                    currentFomationSlotUI = targetSelector.selectFomationSlotUI;
                    // 선택된 슬롯을 선택한다.
                    currentFomationSlotUI.Select();
                }

                // SOUND : 유닛 진형 팝업창 출정 사운드 재생
                // 선택한 유닛의 정보를 보여줍니다.
                Unit selectUnit = targetSelector.SelectUnit;
                ShowUnit(selectUnit);
            }
        }

        // 버튼 레이아웃을 보여줍니다.
        public void BTN_OnClick_SetBtn()
        {
            // 만약 선택된 유닛이 없다면 리턴
            if (GetCurrentUnit == null) return;

            btnLayout.gameObject.SetActive(!btnLayout.gameObject.activeInHierarchy);
        }

        // 선택한 유닛을 취소합니다.
        public void BTN_OnClick_DisableUnit()
        {
            if (GetCurrentUnit == null) return;

            // 선택한 유닛을 취소하고 초기화합니다.
            currentFomationSlotUI.UnSelect();
            currentFomationSlotUI = null;
            unitSlotUI.ShowUnit(null, true, true);
            ReShow();

            btnLayout.gameObject.SetActive(false);
        }
    }

}