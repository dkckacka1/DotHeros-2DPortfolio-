using Portfolio.Lobby.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ���� UIŬ����
 */

namespace Portfolio.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;               // ������ �̹���
        [SerializeField] TextMeshProUGUI itemCountText; // ������ ���� �ؽ�Ʈ

        private ConsumableItemData itemData;    // ������ ������ ������

        public ConsumableItemData ItemData => itemData;

        // ������ �����Ͱ� ������ �����͸� ���� �������� ǥ���Ѵ�.
        public void ShowItem(int ID, int count)
        {
            if (GameManager.Instance.TryGetData(ID, out itemData))
            {
                itemImage.sprite = GetItemIconSprite(itemData.ID);
                itemCountText.text = count.ToString();
            }
        }

        // ������ ID�� ������ �� �ش� �������� ������ ��������Ʈ�� �ҷ��´�.
        private Sprite GetItemIconSprite(int ID)
        {
            ConsumableItemData data;
            GameManager.Instance.TryGetData(ID, out data);
            return GameManager.Instance.GetSprite(data.itemIconSpriteName);
        }

    }
}