using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ���� UIŬ����
 * TODO : �Һ������ ������, �κ��丮 �����ͷ� ���� �������
 */

namespace Portfolio.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] int defaultItemID = 2000;      // ������ �����Ͱ� ���� ��� �⺻������ ������ ������ ������ ID
        [SerializeField] Button slotBtn;                // ���� ��ư
        [SerializeField] Image itemImage;               // ������ �̹���
        [SerializeField] Image unSelcetImage;           // ������ ���� �Ұ� �̹���
        [SerializeField] TextMeshProUGUI itemCountText; // ������ ���� �ؽ�Ʈ

        // �������� �����ش�.
        public void ShowItem()
        {
            // ������ ������ �����Ͱ� �����Ƿ� �⺻ ID�� ���ؼ� ������ �����ش�.
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            if (GameManager.CurrentUser.IsHaveComsumableItem(defaultItemID))
                // �⺻ �������� ������ ������ �ִ� ���
            {
                // ��ư�� ��ȣ�ۿ��� Ȱ��ȭ �Ѵ�.
                slotBtn.interactable = true;
                // ������ �ִ� ������ ǥ���Ѵ�.
                itemCountText.text = GameManager.CurrentUser.UserConsumableItemDic[defaultItemID].ToString();
                // ���� �Ұ� �̹����� �����ش�.
                unSelcetImage.gameObject.SetActive(false);
            }
            else
                 // �⺻ �������� ������ ������ ���� ���� ���
            {
                // ��ư�� ��ȣ�ۿ��� ��Ȱ��ȭ �Ѵ�.
                slotBtn.interactable = false;
                itemCountText.text = "0";
                unSelcetImage.gameObject.SetActive(true);
            }
        }

        // ������ �����Ͱ� ������ �����͸� ���� �������� ǥ���Ѵ�.
        public void ShowItem(int ID, int count, bool btnInteractable = false)
        {
            defaultItemID = ID;
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            slotBtn.interactable = btnInteractable;
            itemCountText.text = count.ToString();
            unSelcetImage.gameObject.SetActive(false);
        }

        // ������ ID�� ������ �� �ش� �������� ������ ��������Ʈ�� �ҷ��´�.
        private Sprite GetItemIconSprite(int ID)
        {
            ConsumableItemData data;
            GameManager.Instance.TryGetData(ID, out data);
            return GameManager.Instance.GetSprite(data.itemIconSpriteName);
        }

        // �������� ����Ѵ�.
        public void ConsumeItem(int count = 1)
        {
            // ����� �Һ� ������ ������ŭ �������� ����Ѵ�.
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            // ������ ������ ������Ʈ �Ѵ�.
            ShowItem();
        }

        // �κ��丮���� ������ �����ش�.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            ConsumableItemData data;
            if (GameManager.Instance.TryGetData(defaultItemID, out data))
            {
                inventoryPanel.ShowTooltip(data, this.transform as RectTransform);
            }
        }

        // ������ �����ش�.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}