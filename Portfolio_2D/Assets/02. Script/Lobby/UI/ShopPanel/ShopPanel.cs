using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * 상점을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopPanel : PanelUI
    {
        [Header("Toggle")]
        [SerializeField] Toggle diaShopToggle;  // 다이아 상점 토글
        [SerializeField] Toggle goldShopToggle; // 골드 상점 토글
        [SerializeField] Toggle consumableItemShopToggle; // 소비아이템 상점 토글

        [Header("ScrollView")]
        [SerializeField] ScrollRect diaShopScrollRect;  // 다이아 상점 스크롤 뷰
        [SerializeField] ScrollRect goldShopScrollRect; // 골드 상점 스크롤 뷰
        [SerializeField] ScrollRect consumableItemScrollRect; // 골드 상점 스크롤 뷰

        [Header("Nodes")]
        [SerializeField] List<ShopConsumableItemNodeUI> consumableItemNodeList; // 소비아이템 상품 노드 리스트

        private void Awake()
        {
            // 노드를 세팅합니다.
            foreach (var node in consumableItemScrollRect.content.GetComponentsInChildren<ShopConsumableItemNodeUI>())
            {
                consumableItemNodeList.Add(node);
            }
        }

        private void Start()
        {
            // 상점에서 판매가능한 아이템 리스트를 세팅합니다.
            SetConsumableItemShop();
        }


        protected override void OnEnable()
        {
            base.OnEnable();
        }

        // 창이 꺼질때 초기화 한다.
        private void OnDisable()
        {
            diaShopToggle.isOn = true;
            diaShopToggle.onValueChanged?.Invoke(true);
            goldShopToggle.onValueChanged?.Invoke(false);
            consumableItemShopToggle.onValueChanged?.Invoke(false);
        }

        // 다이아 상점을 보여줍니다.
        public void TOGGLE_OnValueChanged_ShowDiaShop(bool isActive)
        {
            diaShopScrollRect.gameObject.SetActive(isActive);
        }

        // 골드 상점을 보여줍니다.
        public void TOGGLE_OnValueChanged_ShowGoldShop(bool isActive)
        {
            goldShopScrollRect.gameObject.SetActive(isActive);
        }

        // 소비아이템 상점을 보여줍니다.
        public void TOGGLE_OnValueChanged_ShowConsumableShop(bool isActive)
        {
            consumableItemScrollRect.gameObject.SetActive(isActive);
        }

        // 소비아이템 상점을 새로고침 합니다.
        public void BTN_OnClick_RefreshConsumableShop()
        {
            GameManager.UIManager.ShowConfirmation("상점 새로고침", $"소비아이템 상점을 새로고침 하시겠습니까?\n{Constant.ShopRefreshDiaConsumValue} 다이아가 소비됩니다.",
                () =>
                {
                    if (GameManager.CurrentUser.CanDIamondConsume(Constant.ShopRefreshDiaConsumValue))
                    // 소지 다이아량과 소비 다이아량을 비교한다.
                    {
                        GameManager.CurrentUser.Diamond -= Constant.ShopRefreshDiaConsumValue;
                        // 상점아이템을 세팅합니다.
                        SetConsumableItemShop();
                    }
                    else
                    {
                        GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
                    }
                });
        }

        // 상점에서 판매가능한 아이템 리스트를 세팅합니다.
        private void SetConsumableItemShop()
        {
            var itemList = GameManager.Instance.GetDatas<ConsumableItemData>().Where(item => item.isShopProduct).ToList();
            // 나올 수 있는 할인율의 갯수
            int discountLength = Constant.ShopProductDiscountValues.Length;
            foreach (var node in consumableItemNodeList)
            {
                // 랜덤한 소비아이템 데이터
                var itemData = itemList[Random.Range(0, (int)itemList.Count)];
                // 랜덤한 갯수
                int itemCount = Random.Range(1, Constant.ShopProductMaxCount);
                // 골드판매할지 다이아판매할지
                bool isGoldPayment = (Random.Range(0, 2) == 1) ? true : false;
                // 랜덤한 할인율
                float discountValue = Constant.ShopProductDiscountValues[Random.Range(0, discountLength)];

                // 노드에 아이템을 세팅한다.
                node.SetConsumableItemProduct(itemData, itemCount, isGoldPayment, discountValue);
                // 노드가 세팅한 아이템을 표시한다.
                node.ShowProduct();
            }

            // 1. 결제 수단순으로 정렬합니다.
            // 2. 할인률 순으로 정렬합니다.
            var nodeList = consumableItemNodeList.OrderByDescending(node => node.PaymentType).ThenBy(node => node.DiscountValue);
            // 정렬한 리스트를 레이아웃에 재정렬합니다.
            foreach (var node in nodeList)
            {
                node.transform.SetAsLastSibling();
            }
        }
    }
}