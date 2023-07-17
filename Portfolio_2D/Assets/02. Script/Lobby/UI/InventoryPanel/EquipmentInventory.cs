using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.UI;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Selector = Portfolio.Lobby.Inventory.EquipmentSlotSelector_EquipmentInventory; // Ŭ���� �̸��� �ʹ� �� ��Ī ���

/*
 * �������� �κ��丮�� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Inventory
{
    public class EquipmentInventory : MonoBehaviour
    {
        private List<EquipmentItemSlot> equipmentSlotUIList;  // ��� ���� ����Ʈ
        [SerializeField] ScrollRect slotScrollView;             // ��� ���� ��ũ�� ��

        [SerializeField] TextMeshProUGUI equipmentListSizeText; // ��� �κ��丮 ������ �ؽ�Ʈ
        [SerializeField] Toggle multipleSelectionToggle;        // ���� ���� ���
        [SerializeField] Button equipmentItemDumpBtn;           // ��� ������ ��ư

        List<Selector> currentSelectorList = new List<Selector>();

        public bool IsMultipleSelection => multipleSelectionToggle.isOn;

        public void Init()
        {
            equipmentSlotUIList = new List<EquipmentItemSlot>(Constant.EquipmentListMaxSizeCount);
            foreach (var slot in slotScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentSlotUIList.Add(slot);
            }
        }

        // �� â�� Ȱ��ȭ�Ǹ� �κ��丮�� ǥ���Ѵ�.
        private void OnEnable()
        {
            ShowEquipmentInventory();
        }

        // ��� �κ��丮�� ǥ���Ѵ�.
        private void ShowEquipmentInventory()
        {
            var equipmentItemInventory = GameManager.CurrentUser.GetInventoryEquipmentItem.OrderBy(item => item.equipmentType).ToList();

            for (int i = 0; i < equipmentSlotUIList.Count; i++)
            {
                // ��� �κ��丮�� ��� ������ŭ ��� ������ Ȱ��ȭ�Ѵ�.
                if (equipmentItemInventory.Count <= i)
                {
                    equipmentSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // ��� ��񽽷Կ� ǥ���Ѵ�.
                equipmentSlotUIList[i].ShowEquipment(equipmentItemInventory[i]);
                equipmentSlotUIList[i].gameObject.SetActive(true);
            }

            // ��� �ؽ�Ʈ�� ǥ���Ѵ�.
            ShowEquipmentInventorySizeText();
        }

        // â�� ���� �� �ʱ�ȭ ���ش�.
        private void OnDisable()
        {
            // ���� ���� ����� �⺻���� ���� ���� ���� ����
            multipleSelectionToggle.isOn = false;
            // ������ �͵� ���� ���
            ClearSelect();
        }

        // ORDER : ���� ���� ��ۿ� ���� �������� �Ϲ� ���� Ȥ�� ���� ���� �ý��� ����
        public void EquipmentSlotSelect(EquipmentSlotSelector_EquipmentInventory equipmentSlotUIInventorySelector)
        {
            if (multipleSelectionToggle.isOn)
                // ���� ������ Ȱ��ȭ ���¶��
            {
                if (currentSelectorList.Contains(equipmentSlotUIInventorySelector))
                    // �̹� ������ �����̶��
                {
                    // ������ ����ϰ� ����Ʈ���� ���� �����ش�.
                    equipmentSlotUIInventorySelector.IsSelect = false;
                    currentSelectorList.Remove(equipmentSlotUIInventorySelector);
                }
                else
                    // ������ ������ �ƴ϶��
                {
                    // �������ְ� ����Ʈ�� �߰��Ѵ�.
                    equipmentSlotUIInventorySelector.IsSelect = true;
                    currentSelectorList.Add(equipmentSlotUIInventorySelector);
                }
            }
            else
                // ���� ������ ��Ȱ��ȭ ���¶��
            {
                // ���� ����Ʈ���� ù��° ��Ҹ� �����´�.
                var currentSelect = currentSelectorList.SingleOrDefault();
                if (currentSelect != null)
                    // �̹� ������ ��� ������ �ִٸ�
                {
                    // �ش� ���� ������ ����ϰ� ����Ʈ���� ���ܽ����ش�.
                    currentSelect.IsSelect = false;
                    currentSelectorList.Remove(currentSelect);
                    if (currentSelect != equipmentSlotUIInventorySelector)
                        // �� ������ ������ �̹��� ������ ���԰� ���� �ʴٸ�
                    {
                        equipmentSlotUIInventorySelector.IsSelect = true;
                        currentSelectorList.Add(equipmentSlotUIInventorySelector);
                    }
                }
                else
                    // �� ������ ��� ������ ���ٸ� �׳� ����
                {
                    equipmentSlotUIInventorySelector.IsSelect = true;
                    currentSelectorList.Add(equipmentSlotUIInventorySelector);
                }
            }

            // ������ ������ ������ 1�� �̻��̶�� ��� ������ ��ư Ȱ��ȭ
            equipmentItemDumpBtn.interactable = currentSelectorList.Count > 0;
        }

        // ��ũ�� ���� �߾� ��ǥ�� ��ȯ�Ѵ�.
        public Vector2 GetScrollViewMiddlePoint()
        {
            return (slotScrollView.transform as RectTransform).position;
        }



        // ���� ���� ����� ����� ��� ��� ���� ������ ���� ����Ѵ�.
        public void TOGGLE_OnValueChanged_ClearSelect()
        {
            ClearSelect();
        }

        // ������ ��� �������� ������ ��ư
        public void BTN_OnClick_DumpEquipment()
        {
            // ������ �� Ȯ�� ���̾�α� â ǥ��
            GameManager.UIManager.ShowConfirmation("��� ������ ������", $"��� �������� �����ðڽ��ϱ�?\n���� �������� ���ƿ��� �ʽ��ϴ�.", DumpEquipment);
        }



        // ��� �κ��丮 �ִ� ����� �߰��ϴ� ��ư
        public void BTN_OnClick_EquipmentListSizeUp()
        {
            if (GameManager.CurrentUser.MaxEquipmentListCount >= Constant.EquipmentListMaxSizeCount)
                // ���̻� �߰��� �Ұ��� �ϸ� ���â ǥ��
            {
                GameManager.UIManager.ShowAlert("�̹� �ִ� ����� �����߽��ϴ�!");
            }
            else
                // �߰��� �����ϸ� Ȯ�� ���̾�α� â�� ǥ�� 
            {
                int consumeDia = Constant.EquipmentListSizeUpDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("�ִ� ��� ���� ����", $"�ִ� ��� ������ ���� ��Ű�ڽ��ϱ�?\n{consumeDia} ���̾ư� �Һ�Ǹ�\n{Constant.EquipmentListSizeUpCount}ĭ�� �þ�ϴ�.\n(�ִ� 100ĭ)", EquipmentInventorySizeUp);
            }
        }

        // ��� ���� ������ ����Ѵ�.
        private void ClearSelect()
        {
            foreach (var selector in currentSelectorList)
            {
                // ���� ������ش�.
                selector.IsSelect = false;
            }
            currentSelectorList.Clear();

            // ��� ������ ��ư ��Ȱ��ȭ
            equipmentItemDumpBtn.interactable = false;
        }

        // ��� �κ��丮 ����� �߰��Ѵ�.
        private void EquipmentInventorySizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.EquipmentListSizeUpCount))
                // ���� ���̾Ʒ��� �Һ� ���̾Ʒ��� ���Ѵ�.
            {
                GameManager.CurrentUser.Diamond -= Constant.EquipmentListSizeUpCount;
                GameManager.CurrentUser.MaxEquipmentListCount += Constant.EquipmentListSizeUpCount;
                ShowEquipmentInventorySizeText();
            }
            else
            {
                GameManager.UIManager.ShowAlert("���̾Ƹ�尡 �����մϴ�!");
            }
        }

        // ��� �κ��丮�� ������ �ؽ�Ʈ�� ǥ���Ѵ�.
        private void ShowEquipmentInventorySizeText()
        {
            var equipmentItemInventory = GameManager.CurrentUser.GetInventoryEquipmentItem;
            // ���� �ռ� ������ ���������� ��� �κ��丮 ����� �ʰ��ϴ� ��찡 ����� ������ �ؽ�Ʈ ���� �����ؼ� �˱� ���� �����ش�.
            if (equipmentItemInventory.Count >= GameManager.CurrentUser.MaxEquipmentListCount)
            {
                equipmentListSizeText.color = Color.red;
            }
            else
            {
                equipmentListSizeText.color = Color.white;
            }
            equipmentListSizeText.text = $"{equipmentItemInventory.Count} / {GameManager.CurrentUser.MaxEquipmentListCount}";
        }

        // ��� �������� ������.
        private void DumpEquipment()
        {
            // SOUND : ��� ������ ���� ���
            // ������ �������� �κ��丮���� �������ش�.
            foreach (var itemData in currentSelectorList.Select(selector => selector.unitEquipmentSlotUI.EquipmentData))
            {
                if (!GameManager.CurrentUser.TryRemoveEquipmentItem(itemData))
                {
                    Debug.LogError("�������� �������� �κ��丮�� �������� �ʽ��ϴ�.");
                    return;
                }
            }

            // ��� ������ �׸��� �����ش�.
            ClearSelect();
            // ��� �κ��丮�� �ٽ� ǥ�����ش�.
            ShowEquipmentInventory();
        }
    }

}