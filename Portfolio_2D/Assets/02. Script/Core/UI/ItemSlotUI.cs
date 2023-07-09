using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 아이템 슬롯 UI클래스
 */

namespace Portfolio.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;               // 아이템 이미지
        [SerializeField] TextMeshProUGUI itemCountText; // 아이템 갯수 텍스트

        private ConsumableItemData itemData;    // 슬롯의 아이템 데이터

        public ConsumableItemData ItemData => itemData;

        // 아이템 데이터가 있으면 데이터를 통해 아이템을 표시한다.
        public void ShowItem(int ID, int count)
        {
            if (GameManager.Instance.TryGetData(ID, out itemData))
            {
                itemImage.sprite = GetItemIconSprite(itemData.ID);
                itemCountText.text = count.ToString();
            }
        }

        // 아이템 ID만 들어왔을 때 해당 아이템의 아이콘 스프라이트를 불러온다.
        private Sprite GetItemIconSprite(int ID)
        {
            ConsumableItemData data;
            GameManager.Instance.TryGetData(ID, out data);
            return GameManager.Instance.GetSprite(data.itemIconSpriteName);
        }

    }
}