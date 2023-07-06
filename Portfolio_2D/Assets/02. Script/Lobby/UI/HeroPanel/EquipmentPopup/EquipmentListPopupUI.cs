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

        public void ShowList(object sender, EventArgs eventArgs)
        {
            if (HeroPanelUI.SelectEquipmentItem != null)
            {
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        private void OnEnable()
        {
            UnChoiceList();
        }

        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                                orderby (item.equipmentType == equipmentItemData.equipmentType) descending
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
                selectedUI.ShowImpossibleSelectImage(equipmentItemData);
            }

            equipmentDiscardBtn.interactable = true;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

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

            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void UnChoiceList()
        {
            foreach (var slot in equipmentSlotList)
            {
                slot.GetComponent<EquipmentSelectUI>().isChoice = false;
            }
            equipmentChangeBtn.interactable = false;
        }
    }
}