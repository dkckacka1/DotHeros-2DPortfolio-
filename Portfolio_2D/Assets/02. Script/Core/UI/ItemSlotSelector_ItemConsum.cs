using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ���Կ� �ִ� �������� ����ϱ� ���� ���� ������ Ŭ����
 * ������ ���� ������Ʈ�� ������ ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.UI
{
    [RequireComponent(typeof(ItemSlotUI))]
    public class ItemSlotSelector_ItemConsum : MonoBehaviour
    {
        [SerializeField] int defaultItemID = 2000;      // ������ �����Ͱ� ���� ��� �⺻������ ������ ������ ������ ID
        [SerializeField] Image unSelcetImage;           // ������ ���� �Ұ� �̹���
        [SerializeField] Button slotButton;             // ������ ���� ��ư

        private ItemSlotUI itemSlotUI; // ������ ���� UI
        private void Awake()
        {
            itemSlotUI = GetComponent<ItemSlotUI>();
        }

        // ������ ��� �Һ�����ۿ� ���� UI�� �����ݴϴ�
        public void ShowSlot()
        {
            // ������ ���� �������� ����
            int itemCount = GameManager.CurrentUser.GetConsumItemCount(defaultItemID);
            itemSlotUI.ShowItem(defaultItemID, itemCount);

            // �������� ������ 0 ���� �׸��� ���� �մϴ�.
            unSelcetImage.gameObject.SetActive(itemCount == 0);
            slotButton.interactable = itemCount != 0;
        }

        // �������� ����Ѵ�.
        public void BTN_OnClick_ConsumeItem(int count = 1)
        {
            // ����� �Һ� ������ ������ŭ �������� ����Ѵ�.
            GameManager.CurrentUser.ConsumItem(defaultItemID, count);
            // ������ ������ ������Ʈ �Ѵ�.
            ShowSlot();
        }
    }
}