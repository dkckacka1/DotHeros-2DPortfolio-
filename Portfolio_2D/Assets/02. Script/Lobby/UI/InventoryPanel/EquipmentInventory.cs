using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.UI;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

namespace Portfolio.Lobby.Inventory
{
    public class EquipmentInventory : MonoBehaviour
    {
        private List<UnitEquipmentSlotUI> equipmentSlotUIList;  // ��� ���� ����Ʈ
        [SerializeField] ScrollRect slotScrollView;             // ��� ���� ��ũ�� ��

        [SerializeField] TextMeshProUGUI equipmentListSizeText; // ��� �κ��丮 ������ �ؽ�Ʈ
        [SerializeField] Toggle multipleSelectionToggle;        // ���� ���� ���
        [SerializeField] Button equipmentItemDumpBtn;           // ��� ������ ��ư

        List<EquipmentSlotUIInventorySelector> currentSelectorList = new List<EquipmentSlotUIInventorySelector>();

        public bool IsMultipleSelection => multipleSelectionToggle.isOn;

        public void Init()
        {
            equipmentSlotUIList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in slotScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
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
            var equipmentItemInventory = GameManager.CurrentUser.GetInventoryEquipmentItem;

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
            ClearSelect();
        }

        public void EquipmentSlotSelect(EquipmentSlotUIInventorySelector equipmentSlotUIInventorySelector)
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

        public Vector2 GetScrollViewMiddlePoint()
        {
            return (slotScrollView.transform as RectTransform).position;
        }

        // ���� ���� ����� ����� ��� ��� ���� ������ ���� ����Ѵ�.
        public void TOGGLE_OnValueChanged_ClearSelect()
        {
            ClearSelect();
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

        // ������ ��� �������� ������ ��ư
        public void BTN_OnClick_DumpEquipment()
        {
            // ������ �� Ȯ�� ���̾�α� â ǥ��
            GameManager.UIManager.ShowConfirmation("��� ������ ������", $"��� �������� �����ðڽ��ϱ�?\n���� �������� ���ƿ��� �ʽ��ϴ�.", DumpEquipment);
        }

        // ��� �������� ������.
        private void DumpEquipment()
        {
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

        // ��� �κ��丮 �ִ� ����� �߰��ϴ� ��ư
        public void BTN_OnClick_EquipmentListSizeUp()
        {
            if (GameManager.CurrentUser.MaxEquipmentListCount >= Constant.equipmentListMaxSizeCount)
                // ���̻� �߰��� �Ұ��� �ϸ� ���â ǥ��
            {
                GameManager.UIManager.ShowAlert("�̹� �ִ� ����� �����߽��ϴ�!");
            }
            else
                // �߰��� �����ϸ� Ȯ�� ���̾�α� â�� ǥ�� 
            {
                int consumeDia = Constant.equipmentListSizeUpDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("�ִ� ��� ���� ����", $"�ִ� ��� ������ ���� ��Ű�ڽ��ϱ�?\n{consumeDia} ���̾ư� �Һ�Ǹ�\n{Constant.equipmentListSizeUpCount}ĭ�� �þ�ϴ�.\n(�ִ� 100ĭ)", EquipmentInventorySizeUp);
            }
        }

        // ��� �κ��丮 ����� �߰��Ѵ�.
        private void EquipmentInventorySizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.equipmentListSizeUpCount))
                // ���� ���̾Ʒ��� �Һ� ���̾Ʒ��� ���Ѵ�.
            {
                GameManager.CurrentUser.Diamond -= Constant.equipmentListSizeUpCount;
                GameManager.CurrentUser.MaxEquipmentListCount += Constant.equipmentListSizeUpCount;
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
    }

}