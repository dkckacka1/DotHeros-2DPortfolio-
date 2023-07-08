using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopPanel : PanelUI
    {
        [Header("Toggle")]
        [SerializeField] Toggle diaShopToggle;  // ���̾� ���� ���
        [SerializeField] Toggle goldShopToggle; // ��� ���� ���
        [SerializeField] Toggle consumableItemShopToggle; // �Һ������ ���� ���

        [Header("ScrollView")]
        [SerializeField] ScrollRect diaShopScrollRect;  // ���̾� ���� ��ũ�� ��
        [SerializeField] ScrollRect goldShopScrollRect; // ��� ���� ��ũ�� ��
        [SerializeField] ScrollRect consumableItemScrollRect; // ��� ���� ��ũ�� ��

        [Header("Nodes")]
        [SerializeField] List<ShopConsumableItemNodeUI> consumableItemNodeList; // �Һ������ ��ǰ ��� ����Ʈ

        private void Awake()
        {
            // ��带 �����մϴ�.
            foreach (var node in consumableItemScrollRect.content.GetComponentsInChildren<ShopConsumableItemNodeUI>())
            {
                consumableItemNodeList.Add(node);
            }
        }

        private void Start()
        {
            // �������� �ǸŰ����� ������ ����Ʈ�� �����´�.
            var itemList = GameManager.Instance.GetDatas<ConsumableItemData>().Where(item => item.isShopProduct).ToList();
            // ���� �� �ִ� �������� ����
            int discountLength = Constant.shopProductDiscountValues.Length;
            foreach (var node in consumableItemNodeList)
            {
                // ������ �Һ������ ������
                var itemData = itemList[Random.Range(0, (int)itemList.Count)];
                // ������ ����
                int itemCount = Random.Range(1, Constant.shopProductMaxCount);
                // ����Ǹ����� ���̾��Ǹ�����
                bool isGoldPayment = (Random.Range(0, 2) == 1) ? true : false;
                // ������ ������
                float discountValue = Constant.shopProductDiscountValues[Random.Range(0, discountLength)];

                // ��忡 �������� �����Ѵ�.
                node.SetConsumableItemProduct(itemData, itemCount, isGoldPayment, discountValue);
                // ��尡 ������ �������� ǥ���Ѵ�.
                node.ShowProduct();
            }

            // ��� ����Ʈ�� ������ �������� ID�� �����մϴ�.
            consumableItemNodeList.OrderBy(item => item.ConsumableItemData.ID);
            // ������ ����Ʈ�� ���̾ƿ��� �������մϴ�.
            foreach (var node in consumableItemNodeList)
            {
                node.transform.SetAsFirstSibling();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        // â�� ������ �ʱ�ȭ �Ѵ�.
        private void OnDisable()
        {
            diaShopToggle.Select();
            diaShopToggle.onValueChanged?.Invoke(true);
            goldShopToggle.onValueChanged?.Invoke(false);
            consumableItemShopToggle.onValueChanged?.Invoke(false);
        }

        // ���̾� ������ �����ݴϴ�.
        public void TOGGLE_OnValueChanged_ShowDiaShop(bool isActive)
        {
            diaShopScrollRect.gameObject.SetActive(isActive);
        }

        // ��� ������ �����ݴϴ�.
        public void TOGGLE_OnValueChanged_ShowGoldShop(bool isActive)
        {
            goldShopScrollRect.gameObject.SetActive(isActive);
        }

        // �Һ������ ������ �����ݴϴ�.
        public void TOGGLE_OnValueChanged_ShowConsumableShop(bool isActive)
        {
            consumableItemScrollRect.gameObject.SetActive(isActive);
        }
    }
}