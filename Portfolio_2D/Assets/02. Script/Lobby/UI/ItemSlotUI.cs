using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] int defaultItemID = 2000;
        [SerializeField] Button slotBtn;
        [SerializeField] Image itemImage;
        [SerializeField] Image unSelcetImage;
        [SerializeField] TextMeshProUGUI itemCountText;

        public void ShowItem()
        {
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            if (GameManager.CurrentUser.IsHaveComsumableItem(defaultItemID))
            {
                slotBtn.interactable = true;
                itemCountText.text = GameManager.CurrentUser.UserConsumableItemDic[defaultItemID].ToString();
                unSelcetImage.gameObject.SetActive(false);
            }
            else
            {
                slotBtn.interactable = false;
                itemCountText.text = "0";
                unSelcetImage.gameObject.SetActive(true);
            }
        }

        public void ShowItem(int ID, int count, bool btnInteractable = false)
        {
            defaultItemID = ID;
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            slotBtn.interactable = btnInteractable;
            itemCountText.text = count.ToString();
            unSelcetImage.gameObject.SetActive(false);
        }

        private Sprite GetItemIconSprite(int ID)
        {
            ConsumableItemData data;
            GameManager.Instance.TryGetData(ID, out data);
            return GameManager.Instance.GetSprite(data.itemIconSpriteName);
        }

        public void ConsumeItem(int count = 1)
        {
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            ShowItem();
        }

        public void ShowTooltip(InventoryPanel inventoryPanel)
        {
            ConsumableItemData data;
            if (GameManager.Instance.TryGetData(defaultItemID, out data))
            {
                inventoryPanel.ShowTooltip(data, this.transform as RectTransform);
            }
        }

        public void HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}