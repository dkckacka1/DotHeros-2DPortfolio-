using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 상점을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopPanel : PanelUI
    {
        [SerializeField] ScrollRect diaShopScrollRect;  // 다이아 상점 스크롤 뷰
        [SerializeField] ScrollRect goldShopScrollRect; // 골드 상점 스크롤 뷰

        protected override void OnEnable()
        {
            base.OnEnable();
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
    }
}