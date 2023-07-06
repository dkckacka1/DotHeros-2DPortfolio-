using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Portfolio.UI;
using Selector = Portfolio.Lobby.Hero.EquipmentItemSlotSelector_EquipmentListPopup; // Ŭ���� �̸��� �ʹ� �� ��Ī ���

/*
 * ������ ��� �������� �����ϱ� ���� �������� ����Ʈ �˾� UI Ŭ����
 */


// TODO : ��� ���� ���� ���� ���� �ʿ�

namespace Portfolio.Lobby.Hero
{
    public class EquipmentListPopupUI : MonoBehaviour
    {
        [SerializeField] List<EquipmentItemSlot> equipmentSlotList;   // ��� ���� ����Ʈ
        [SerializeField] ScrollRect equipmentListScrollView;            // ��� ������ ��ũ�� ��
        [SerializeField] EquipmentTooltip equipmentTooltipUI;           // �������ۿ� �����͸� �÷����� �� ǥ���� �������� ����
        [SerializeField] Button equipmentChangeBtn;                     // ��� ���� ��ư
        [SerializeField] Button equipmentDiscardBtn;                    // ���� ���� ��ư
        [SerializeField] TextMeshProUGUI notingText;                    // ��� �������� ���� �� ǥ���� �ؽ�Ʈ

        Selector equipmetSelector; // ������ ������ ���

        // ���� ����
        public void Init()
        {
            equipmentSlotList = new List<EquipmentItemSlot>();
            foreach (var slot in equipmentListScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentSlotList.Add(slot);
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

        // â�� ���� �� �� ������ ��� �ʱ�ȭ�Ѵ�.
        private void OnDisable()
        {
            ChoiceItemReset();
        }

        // ��� ������ ����Ʈ �����ֱ�
        public void ShowEquipmentList(EquipmentItemData equipmentItemData)
        {
            // ������ ���������� �����ֵ� ������ ��� �Ǵ� ��� Ÿ�԰� ���� �ͺ��� ����
            var listOrdered = (from item in GameManager.CurrentUser.GetInventoryEquipmentItem
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
                var selectedUI = equipmentSlotList[i].GetComponent<Selector>();
                // ��� ���ð� ���� Ÿ������ Ȯ��
                selectedUI.IsSameEuqipmentType = equipmentItemData.equipmentType == equipmentSlotList[i].EquipmentData.equipmentType;
            }

            // �̹� ������ ��� ������ ���� ���� ��ư ��ȣ�ۿ� Ȱ��ȭ
            equipmentDiscardBtn.interactable = true;
            // ���� ��񸮽�Ʈ�� ����ִٸ� �� �ؽ�Ʈ ���
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }



        // ��� Ÿ������ ����Ʈ �����ֱ�
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

            // �̹� ������ ��� ������ ���� ���� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
            equipmentDiscardBtn.interactable = false;
            notingText.gameObject.SetActive(listOrdered.Count == 0);
        }

        // ��� ������ �������� ��
        public void ChoiceItem(Selector equipmentSlotUISelector)
        {
            if (equipmetSelector != null)
            // �̹� ������ ��� ���� ���
            {
                if (equipmetSelector == equipmentSlotUISelector)
                // ���� ��� ������ ������ ���
                {
                    // ���õ� ������ ����Ѵ�.
                    equipmetSelector.IsSelect = false;
                    equipmetSelector = null;
                }
                else
                // �ٸ� ��� ������ ������ ���
                {
                    // �� ���õ� ������ ����Ѵ�.
                    equipmetSelector.IsSelect = false;

                    // �����͸� �����Ѵ�.
                    equipmetSelector = equipmentSlotUISelector;
                    equipmetSelector.IsSelect = true;

                }
            }
            else
            {
                // �����͸� �����Ѵ�.
                equipmetSelector = equipmentSlotUISelector;
                equipmetSelector.IsSelect = true;
            }

            // ����â�� ���õ� �����͸� �Ѱ��ش�.
            HeroPanelUI.ChoiceEquipmentItem = (equipmetSelector != null) ? equipmetSelector.EquipmentSlot.EquipmentData : null;
            // �����Ͱ� �����ϸ� ��� ���� ��ư ��ȣ�ۿ��� Ȱ��ȭ�Ѵ�.
            equipmentChangeBtn.interactable = (equipmetSelector != null);
        }

        public void ChoiceItemReset()
        {
            if (equipmetSelector != null)
                // �̹� ������ ��� ������ �ִٸ� �ʱ�ȭ
            {
                equipmetSelector.IsSelect = false;
                equipmetSelector = null;
            }

            // ��� ��ü ��ư ��ȣ�ۿ� Ȱ��ȭ
            equipmentChangeBtn.interactable = false;
        }
    }
}