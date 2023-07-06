using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Portfolio.UI;

/*
 * 유닛의 장비 아이템을 변경하기 위한 장비아이템 리스트 팝업 UI 클래스
 */


// TODO : 장비 슬롯 선택 비선택 개선 필요

namespace Portfolio.Lobby.Hero
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        [SerializeField] List<UnitEquipmentSlotUI> equipmentSlotList;   // 장비 슬롯 리스트
        [SerializeField] ScrollRect equipmentListScrollView;            // 장비 슬롯의 스크롤 뷰
        [SerializeField] EquipmentTooltip equipmentTooltipUI;           // 장비아이템에 포인터를 올려놨을 때 표시할 장비아이템 툴팁
        [SerializeField] Button equipmentChangeBtn;                     // 장비 변경 버튼
        [SerializeField] Button equipmentDiscardBtn;                    // 장착 해제 버튼
        [SerializeField] TextMeshProUGUI notingText;                    // 장비 아이템이 없을 때 표시할 텍스트

        // 최초 세팅
        public void Init()
        {
            equipmentSlotList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotList.Add(slot);
                slot.GetComponent<EquipmentSelectUI>().Init();
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

        private void OnEnable()
        {
            // 선택한 장비가 없도록 리스트 초기화
            UnChoiceList();
        }

        // 장비 아이템 리스트 보여주기
        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            // 유저의 장비아이템을 보여주되 선택한 장비 또는 장비 타입과 같은 것부터 정렬
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
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
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                if (selectedUI.isChoice)
                    // 선택한 장비가 있다면
                {
                    // 선택 이미지 표시
                    selectedUI.ShowSelectedUI();
                    // 장비 교체 버튼 상호작용 활성화
                    equipmentChangeBtn.interactable = true;
                }
                else
                    // 선택한 장비가 아니라면
                {
                    // 선택 이미지 숨기기
                    selectedUI.HideSelectedUI();
                }

                // 장비 선택과 같은 타입인지 확인
                selectedUI.ShowImpossibleSelectImage(equipmentItemData);
            }

            // 이미 장착한 장비가 있으니 장착 해제 버튼 상호작용 활성화
            equipmentDiscardBtn.interactable = true;
            // 만약 장비리스트가 비어있다면 빈 텍스트 출력
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        // 장비 타입으로 리스트 보여주기
        public void ShowEquipmentList(EquipmentItemType itemType)
        {
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
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
                var selectedUI = equipmentSlotList[i].GetComponent<EquipmentSelectUI>();
                if (selectedUI.isChoice)
                {
                    selectedUI.ShowSelectedUI();
                    equipmentChangeBtn.interactable = true;
                }
                else
                {
                    selectedUI.HideSelectedUI();
                }
                selectedUI.ShowImpossibleSelectImage(itemType);
            }

            // 이미 장착한 장비가 없으니 장착 해제 버튼 상호작용 비활성화
            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void UnChoiceList()
            // 장비리스트를 순회하며 선택을 비선택으로 바꿔준다.
        {
            foreach (var slot in equipmentSlotList)
            {
                slot.GetComponent<EquipmentSelectUI>().isChoice = false;
            }
            equipmentChangeBtn.interactable = false;
        }
    }
}