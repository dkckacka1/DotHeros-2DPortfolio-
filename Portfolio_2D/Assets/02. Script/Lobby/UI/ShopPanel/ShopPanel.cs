using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Shop
{
    public class ShopPanel : PanelUI
    {
        [SerializeField] ScrollRect diaShopScrollRect;
        [SerializeField] ScrollRect goldShopScrollRect;


        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public void ShowDiaShop(bool isActive)
        {
            diaShopScrollRect.gameObject.SetActive(isActive);
        }

        public void ShowGoldShop(bool isActive)
        {
            goldShopScrollRect.gameObject.SetActive(isActive);
        }
    }
}