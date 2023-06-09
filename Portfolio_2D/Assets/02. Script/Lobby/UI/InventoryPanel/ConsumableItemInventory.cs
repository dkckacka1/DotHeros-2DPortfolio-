using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Inventory
{
    public class ConsumableItemInventory : MonoBehaviour
    {
        List<ItemSlotUI> itemSlotUIList;

        [SerializeField] ScrollRect slotScrollView;

        public void Init()
        {
            itemSlotUIList = new List<ItemSlotUI>();

            foreach (var slot in slotScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                itemSlotUIList.Add(slot);
            }
        }

        private void OnEnable()
        {
            var list = GameManager.CurrentUser.UserConsumableItemDic.ToList();

            for (int i = 0; i < itemSlotUIList.Count; i++)
            {
                if (list.Count <= i)
                {
                    itemSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                itemSlotUIList[i].ShowItem(list[i].Key, list[i].Value);
                itemSlotUIList[i].gameObject.SetActive(true);
            }
        }
    }
}