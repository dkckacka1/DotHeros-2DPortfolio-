using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Portfolio.UI;
using Selector = Portfolio.Lobby.Hero.EquipmentItemSlotSelector_EquipmentListPopup; // 클래스 이름이 너무 길어서 별칭 사용

/*
 * 유닛의 장비 아이템을 변경하기 위한 장비아이템 리스트 팝업 UI 클래스
 */


// TODO : 장비 슬롯 선택 비선택 개선 필요

namespace Portfolio.Lobby.Hero
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        [SerializeField] List<EquipmentItemSlot> equipmentSlotList;   // 장비 슬롯 리스트
        [SerializeField] ScrollRect equipmentListScrollView;            // 장비 슬롯의 스크롤 뷰
        [SerializeField] EquipmentTooltip equipmentTooltipUI;           // 장비아이템에 포인터를 올려놨을 때 표시할 장비아이템 툴팁
        [SerializeField] Button equipmentChangeBtn;                     // 장비 변경 버튼
        [SerializeField] Button equipmentDiscardBtn;                    // 장착 해제 버튼
        [SerializeField] TextMeshProUGUI notingText;                    // 장비 아이템이 없을 때 표시할 텍스트

        Selector equipmetSelector; // 유저가 선택한 장비

        // 최초 세팅
        public void Init()
        {
            equipmentSlotList = new List<EquipmentItemSlot>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentSlotList.Add(slot);
            }
            
            // 장비아이템이 변경될 때 이벤트
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowList;
        }

        // 장비 리스트 보여주기
        public void ShowList(object sender, EventArgs eventArgs)
        {
            if (HeroPanelUI.SelectEquipmentItem != null)
                // 선택한 장비 아이템 있다면
            {
                // 장비 아이템과 같은 장비들로 정렬한다.
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                // 선택한 장비 아이템 칸의 장비 타입과 같은 장비들로 정렬한다.
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        // 창이 꺼질 때 기 선택한 장비를 초기화한다.
        private void OnDisable()
        {
            ChoiceItemReset();
        }

        // 장비 아이템 리스트 보여주기
        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            // 유저의 장비아이템을 보여주되 선택한 장비 또는 장비 타입과 같은 것부터 정렬
            var listOrdered = (from item in GameManager.CurrentUser.GetInventoryEquipmentItem
                                orderby (item.equipmentType == equipmentItemData.equipmentType) descending
                                select item)
                                .ToList();

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                // 장비아이템 갯수 만큼 장비 슬롯 보여주기
                if (listOrdered.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = listOrdered[i];
                equipmentSlotList[i].ShowEquipment(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
                var selectedUI = equipmentSlotList[i].GetComponent<Selector>();
                // 장비 선택과 같은 타입인지 확인
                selectedUI.IsSameEuqipmentType = equipmentItemData.equipmentType == equipmentSlotList[i].EquipmentData.equipmentType;
            }

            // 이미 장착한 장비가 있으니 장착 해제 버튼 상호작용 활성화
            equipmentDiscardBtn.interactable = true;
            // 만약 장비리스트가 비어있다면 빈 텍스트 출력
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }



        // 장비 타입으로 리스트 보여주기
        public void ShowEquipmentList(EquipmentItemType itemType)
        {
            var listOrdered = (from item in GameManager.CurrentUser.GetInventoryEquipmentItem
                               orderby (item.equipmentType == itemType) descending
                               select item)
                        .ToList();

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                if (listOrdered.Count <= i)
                {
                    equipmentSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                var equipmentData = listOrdered[i];
                equipmentSlotList[i].ShowEquipment(equipmentData);
                equipmentSlotList[i].gameObject.SetActive(true);
                var selectedUI = equipmentSlotList[i].GetComponent<Selector>();
                selectedUI.IsSameEuqipmentType = itemType == equipmentSlotList[i].EquipmentData.equipmentType;
            }

            // 이미 장착한 장비가 없으니 장착 해제 버튼 상호작용 비활성화
            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        // 장비 슬롯을 선택했을 시
        public void ChoiceItem(Selector equipmentSlotUISelector)
        {
            if (equipmetSelector != null)
            // 이미 선택한 장비가 있을 경우
            {
                if (equipmetSelector == equipmentSlotUISelector)
                // 같은 장비 슬롯을 선택한 경우
                {
                    // 선택된 내용을 취소한다.
                    equipmetSelector.IsSelect = false;
                    equipmetSelector = null;
                }
                else
                // 다른 장비 슬롯을 선택한 경우
                {
                    // 기 선택된 내용을 취소한다.
                    equipmetSelector.IsSelect = false;

                    // 셀렉터를 변경한다.
                    equipmetSelector = equipmentSlotUISelector;
                    equipmetSelector.IsSelect = true;

                }
            }
            else
            {
                // 셀렉터를 선택한다.
                equipmetSelector = equipmentSlotUISelector;
                equipmetSelector.IsSelect = true;
            }

            // 영웅창에 선택된 데이터를 넘겨준다.
            HeroPanelUI.ChoiceEquipmentItem = (equipmetSelector != null) ? equipmetSelector.EquipmentSlot.EquipmentData : null;
            // 데이터가 존재하면 장비 변경 버튼 상호작용을 활성화한다.
            equipmentChangeBtn.interactable = (equipmetSelector != null);
        }

        public void ChoiceItemReset()
        {
            if (equipmetSelector != null)
                // 이미 선택한 장비 슬롯이 있다면 초기화
            {
                equipmetSelector.IsSelect = false;
                equipmetSelector = null;
            }

            // 장비 교체 버튼 상호작용 활성화
            equipmentChangeBtn.interactable = false;
        }
    }
}