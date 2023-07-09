using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 상품 패널에서 소비아이템을 구매하는 노드를 표시하는 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopConsumableItemNodeUI : ShopNodeUI
    {
        [Header("ConsumItemNode")]
        [SerializeField] Image consumItemImage;                 // 상품의 이미지
        [SerializeField] TextMeshProUGUI productNameText;       // 상품의 이름
        [SerializeField] TextMeshProUGUI productCountText;      // 상품의 개수
        [SerializeField] TextMeshProUGUI productDiscountText;   // 상품의 할인율
        [SerializeField] RectTransform saleCompletedObject;     // 판매 완료 오브젝트

        private ConsumableItemData productConsumalbeItem;       // 판매할 소비아이템 데이터
        private int productCount;                               // 판매할 개수
        private float discountValue;                            // 할인율

        public ConsumableItemData ConsumableItemData => productConsumalbeItem;

        public float DiscountValue => discountValue;

        // 판매할 아이템을 세팅합니다.
        public void SetConsumableItemProduct(ConsumableItemData itemData, int count, bool isGoldPayment, float discountValue)
        {
            productConsumalbeItem = itemData;
            productCount = count;

            int defaultPayment = 0;

            // 골드 구매인지 다이아 구매인지 결정합니다.
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
            
            // 상품 가격, 갯수, 할인율을 통해 금액을 결정합니다.
            base.paymentValue = Mathf.RoundToInt(defaultPayment * productCount * (1 - discountValue));
            this.discountValue = discountValue;
        }

        // 상품 정보를 표시합니다.
        public void ShowProduct()
        {
            // 이미지를 보여주고 이미지 크기를 자연스럽게 조정합니다.
            consumItemImage.sprite = GameManager.Instance.GetSprite(productConsumalbeItem.itemIconSpriteName);
            consumItemImage.SetNativeSize();
            productNameText.text = productConsumalbeItem.itemName;
            productCountText.text = productCount.ToString() + " 개";

            // 할인율이 0% 가 아니면 할인 오브젝트를 표시합니다.
            if (DiscountValue != 0)
            {
                productDiscountText.text = $"{(DiscountValue * 100).ToString("00")}%\n할인!";
                productDiscountText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                productDiscountText.transform.parent.gameObject.SetActive(false);
            }
            saleCompletedObject.gameObject.SetActive(false);

            ShowPayment();
        }

        // 판매 완료를 표시합니다.
        private void SetSaleCompleted()
        {
            saleCompletedObject.gameObject.SetActive(true);
        }

        // 유저에게 아이템 데이터를 추가합니다.
        protected override void GetProduct()
        {
            // 유저에게 아이템을 추가합니다.
            GameManager.CurrentUser.AddConsumableItem(productConsumalbeItem.ID, productCount);
            // 판매완료 표시합니다.
            SetSaleCompleted();
        }
    }

}