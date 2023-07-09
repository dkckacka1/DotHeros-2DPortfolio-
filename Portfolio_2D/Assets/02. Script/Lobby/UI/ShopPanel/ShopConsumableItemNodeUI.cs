using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��ǰ �гο��� �Һ�������� �����ϴ� ��带 ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopConsumableItemNodeUI : ShopNodeUI
    {
        [Header("ConsumItemNode")]
        [SerializeField] Image consumItemImage;                 // ��ǰ�� �̹���
        [SerializeField] TextMeshProUGUI productNameText;       // ��ǰ�� �̸�
        [SerializeField] TextMeshProUGUI productCountText;      // ��ǰ�� ����
        [SerializeField] TextMeshProUGUI productDiscountText;   // ��ǰ�� ������
        [SerializeField] RectTransform saleCompletedObject;     // �Ǹ� �Ϸ� ������Ʈ

        private ConsumableItemData productConsumalbeItem;       // �Ǹ��� �Һ������ ������
        private int productCount;                               // �Ǹ��� ����
        private float discountValue;                            // ������

        public ConsumableItemData ConsumableItemData => productConsumalbeItem;

        public float DiscountValue => discountValue;

        // �Ǹ��� �������� �����մϴ�.
        public void SetConsumableItemProduct(ConsumableItemData itemData, int count, bool isGoldPayment, float discountValue)
        {
            productConsumalbeItem = itemData;
            productCount = count;

            int defaultPayment = 0;

            // ��� �������� ���̾� �������� �����մϴ�.
            if (isGoldPayment)
            {
                defaultPayment = productConsumalbeItem.productGoldValue;
                PaymentType = PaymentType.Gold;
            }
            else
            {
                defaultPayment = productConsumalbeItem.productDiaValue;
                PaymentType = PaymentType.Diamond;
            }
            
            // ��ǰ ����, ����, �������� ���� �ݾ��� �����մϴ�.
            base.paymentValue = Mathf.RoundToInt(defaultPayment * productCount * (1 - discountValue));
            this.discountValue = discountValue;
        }

        // ��ǰ ������ ǥ���մϴ�.
        public void ShowProduct()
        {
            // �̹����� �����ְ� �̹��� ũ�⸦ �ڿ������� �����մϴ�.
            consumItemImage.sprite = GameManager.Instance.GetSprite(productConsumalbeItem.itemIconSpriteName);
            consumItemImage.SetNativeSize();
            productNameText.text = productConsumalbeItem.itemName;
            productCountText.text = productCount.ToString() + " ��";

            // �������� 0% �� �ƴϸ� ���� ������Ʈ�� ǥ���մϴ�.
            if (DiscountValue != 0)
            {
                productDiscountText.text = $"{(DiscountValue * 100).ToString("00")}%\n����!";
                productDiscountText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                productDiscountText.transform.parent.gameObject.SetActive(false);
            }
            saleCompletedObject.gameObject.SetActive(false);

            ShowPayment();
        }

        // �Ǹ� �ϷḦ ǥ���մϴ�.
        private void SetSaleCompleted()
        {
            saleCompletedObject.gameObject.SetActive(true);
        }

        // �������� ������ �����͸� �߰��մϴ�.
        protected override void GetProduct()
        {
            // �������� �������� �߰��մϴ�.
            GameManager.CurrentUser.AddConsumableItem(productConsumalbeItem.ID, productCount);
            // �ǸſϷ� ǥ���մϴ�.
            SetSaleCompleted();
        }
    }

}