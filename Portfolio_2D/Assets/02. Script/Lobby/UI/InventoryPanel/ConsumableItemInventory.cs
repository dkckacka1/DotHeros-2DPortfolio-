using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * 소비아이템을 보여주는 인벤토리 UI 클래스
 */

namespace Portfolio.Lobby.Inventory
{
    public class ConsumableItemInventory : MonoBehaviour
    {
        List<ItemSlotUI> itemSlotUIList;                // 아이템 슬롯 UI 리스트

        [SerializeField] ScrollRect slotScrollView;     // 아이템 슬롯 스크롤 뷰

        // 초기화 합니다.
        public void Init()
        {
            itemSlotUIList = new List<ItemSlotUI>();

            foreach (var slot in slotScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                itemSlotUIList.Add(slot);
            }
        }

        // 창이 켜질 때 유저가 가진 소비아이템을 표시합니다.
        private void OnEnable()
        {
            // 유저의 소비아이템 Dic를 참조 합니다.
            var list = GameManager.CurrentUser.UserConsumableItemDic.ToList();

            for (int i = 0; i < itemSlotUIList.Count; i++)
            {
                // 유저가 가지고 있는 소비아이템 개수만 큼 슬롯을 활성화 합니다.
                if (list.Count <= i)
                {
                    itemSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // 소비아이템 ID와 갯수로 슬롯을 표시합니다.
                itemSlotUIList[i].ShowItem(list[i].Key, list[i].Value);
                itemSlotUIList[i].gameObject.SetActive(true);
            }
        }
    }
}