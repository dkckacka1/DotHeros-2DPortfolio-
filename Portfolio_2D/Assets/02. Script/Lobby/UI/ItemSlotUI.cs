using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 아이템 슬롯 UI클래스
 * TODO : 소비아이템 셀렉터, 인벤토리 셀렉터로 분할 해줘야함
 */

namespace Portfolio.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] int defaultItemID = 2000;      // 아이템 데이터가 없을 경우 기본적으로 보여줄 아이템 데이터 ID
        [SerializeField] Button slotBtn;                // 슬롯 버튼
        [SerializeField] Image itemImage;               // 아이템 이미지
        [SerializeField] Image unSelcetImage;           // 아이템 선택 불가 이미지
        [SerializeField] TextMeshProUGUI itemCountText; // 아이템 갯수 텍스트

        // 아이템을 보여준다.
        public void ShowItem()
        {
            // 보여줄 아이템 데이터가 없으므로 기본 ID를 통해서 슬롯을 보여준다.
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            if (GameManager.CurrentUser.IsHaveComsumableItem(defaultItemID))
                // 기본 아이템을 유저가 가지고 있는 경우
            {
                // 버튼의 상호작용을 활성화 한다.
                slotBtn.interactable = true;
                // 가지고 있는 수량을 표기한다.
                itemCountText.text = GameManager.CurrentUser.UserConsumableItemDic[defaultItemID].ToString();
                // 선택 불가 이미지를 숨겨준다.
                unSelcetImage.gameObject.SetActive(false);
            }
            else
                 // 기본 아이템을 유저가 가지고 있지 않은 경우
            {
                // 버튼의 상호작용을 비활성화 한다.
                slotBtn.interactable = false;
                itemCountText.text = "0";
                unSelcetImage.gameObject.SetActive(true);
            }
        }

        // 아이템 데이터가 있으면 데이터를 통해 아이템을 표시한다.
        public void ShowItem(int ID, int count, bool btnInteractable = false)
        {
            defaultItemID = ID;
            itemImage.sprite = GetItemIconSprite(defaultItemID);
            slotBtn.interactable = btnInteractable;
            itemCountText.text = count.ToString();
            unSelcetImage.gameObject.SetActive(false);
        }

        // 아이템 ID만 들어왔을 때 해당 아이템의 아이콘 스프라이트를 불러온다.
        private Sprite GetItemIconSprite(int ID)
        {
            ConsumableItemData data;
            GameManager.Instance.TryGetData(ID, out data);
            return GameManager.Instance.GetSprite(data.itemIconSpriteName);
        }

        // 아이템을 사용한다.
        public void ConsumeItem(int count = 1)
        {
            // 사용할 소비 아이템 갯수만큼 아이템을 사용한다.
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            // 아이템 슬롯을 업데이트 한다.
            ShowItem();
        }

        // 인벤토리에서 툴팁을 보여준다.
        public void TRIGGER_OnPointerEnter_ShowTooltip(InventoryPanel inventoryPanel)
        {
            ConsumableItemData data;
            if (GameManager.Instance.TryGetData(defaultItemID, out data))
            {
                inventoryPanel.ShowTooltip(data, this.transform as RectTransform);
            }
        }

        // 툴팁을 숨겨준다.
        public void TRIGGER_OnPointerExit_HideTooltip(InventoryPanel inventoryPanel)
        {
            inventoryPanel.HideTooltip();
        }
    }
}