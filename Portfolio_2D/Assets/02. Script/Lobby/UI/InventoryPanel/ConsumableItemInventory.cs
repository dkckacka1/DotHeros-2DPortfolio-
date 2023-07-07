using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * �Һ�������� �����ִ� �κ��丮 UI Ŭ����
 */

namespace Portfolio.Lobby.Inventory
{
    public class ConsumableItemInventory : MonoBehaviour
    {
        List<ItemSlotUI> itemSlotUIList;                // ������ ���� UI ����Ʈ

        [SerializeField] ScrollRect slotScrollView;     // ������ ���� ��ũ�� ��

        // �ʱ�ȭ �մϴ�.
        public void Init()
        {
            itemSlotUIList = new List<ItemSlotUI>();

            foreach (var slot in slotScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                itemSlotUIList.Add(slot);
            }
        }

        // â�� ���� �� ������ ���� �Һ�������� ǥ���մϴ�.
        private void OnEnable()
        {
            // ������ �Һ������ Dic�� ���� �մϴ�.
            var list = GameManager.CurrentUser.UserConsumableItemDic.ToList();

            for (int i = 0; i < itemSlotUIList.Count; i++)
            {
                // ������ ������ �ִ� �Һ������ ������ ŭ ������ Ȱ��ȭ �մϴ�.
                if (list.Count <= i)
                {
                    itemSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // �Һ������ ID�� ������ ������ ǥ���մϴ�.
                itemSlotUIList[i].ShowItem(list[i].Key, list[i].Value);
                itemSlotUIList[i].gameObject.SetActive(true);
            }
        }
    }
}