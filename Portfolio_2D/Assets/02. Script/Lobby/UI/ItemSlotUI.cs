using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
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
            slotBtn.interactable = btnInteractable;
            itemCountText.text = count.ToString();
            unSelcetImage.gameObject.SetActive(false);
        }

        public void ConsumeItem(int count = 1)
        {
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            ShowItem();
        }
    }
}