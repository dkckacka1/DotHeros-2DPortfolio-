using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Portfolio.UI;

/*
 * ������ ��� �������� �����ϱ� ���� �������� ����Ʈ �˾� UI Ŭ����
 */


// TODO : ��� ���� ���� ���� ���� �ʿ�

namespace Portfolio.Lobby.Hero
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        [SerializeField] List<UnitEquipmentSlotUI> equipmentSlotList;   // ��� ���� ����Ʈ
        [SerializeField] ScrollRect equipmentListScrollView;            // ��� ������ ��ũ�� ��
        [SerializeField] EquipmentTooltip equipmentTooltipUI;           // �������ۿ� �����͸� �÷����� �� ǥ���� �������� ����
        [SerializeField] Button equipmentChangeBtn;                     // ��� ���� ��ư
        [SerializeField] Button equipmentDiscardBtn;                    // ���� ���� ��ư
        [SerializeField] TextMeshProUGUI notingText;                    // ��� �������� ���� �� ǥ���� �ؽ�Ʈ

        // ���� ����
        public void Init()
        {
            equipmentSlotList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotList.Add(slot);
                slot.GetComponent<EquipmentSelectUI>().Init();
            }
            
            // ���������� ����� �� �̺�Ʈ
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowList;
        }

        // ��� ����Ʈ �����ֱ�
        public void ShowList(object sender, EventArgs eventArgs)
        {
            if (HeroPanelUI.SelectEquipmentItem != null)
                // ������ ��� ������ �ִٸ�
            {
                // ��� �����۰� ���� ����� �����Ѵ�.
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItem);
            }
            else
            {
                // ������ ��� ������ ĭ�� ��� Ÿ�԰� ���� ����� �����Ѵ�.
                ShowEquipmentList(HeroPanelUI.SelectEquipmentItemType);
            }
        }

        private void OnEnable()
        {
            // ������ ��� ������ ����Ʈ �ʱ�ȭ
            UnChoiceList();
        }

        // ��� ������ ����Ʈ �����ֱ�
        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            // ������ ���������� �����ֵ� ������ ��� �Ǵ� ��� Ÿ�԰� ���� �ͺ��� ����
            var listOrdered = (from item in GameManager.CurrentUser.userEquipmentItemDataList
                                orderby (item.equipmentType == equipmentItemData.equipmentType) descending
                                select item)
                                .ToList();

            for (int i = 0; i < equipmentSlotList.Count; i++)
            {
                // �������� ���� ��ŭ ��� ���� �����ֱ�
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
                    // ������ ��� �ִٸ�
                {
                    // ���� �̹��� ǥ��
                    selectedUI.ShowSelectedUI();
                    // ��� ��ü ��ư ��ȣ�ۿ� Ȱ��ȭ
                    equipmentChangeBtn.interactable = true;
                }
                else
                    // ������ ��� �ƴ϶��
                {
                    // ���� �̹��� �����
                    selectedUI.HideSelectedUI();
                }

                // ��� ���ð� ���� Ÿ������ Ȯ��
                selectedUI.ShowImpossibleSelectImage(equipmentItemData);
            }

            // �̹� ������ ��� ������ ���� ���� ��ư ��ȣ�ۿ� Ȱ��ȭ
            equipmentDiscardBtn.interactable = true;
            // ���� ��񸮽�Ʈ�� ����ִٸ� �� �ؽ�Ʈ ���
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        // ��� Ÿ������ ����Ʈ �����ֱ�
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

            // �̹� ������ ��� ������ ���� ���� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        public void UnChoiceList()
            // ��񸮽�Ʈ�� ��ȸ�ϸ� ������ �������� �ٲ��ش�.
        {
            foreach (var slot in equipmentSlotList)
            {
                slot.GetComponent<EquipmentSelectUI>().isChoice = false;
            }
            equipmentChangeBtn.interactable = false;
        }
    }
}