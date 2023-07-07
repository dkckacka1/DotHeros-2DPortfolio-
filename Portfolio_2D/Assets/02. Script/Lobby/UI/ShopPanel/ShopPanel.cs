using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopPanel : PanelUI
    {
        [SerializeField] ScrollRect diaShopScrollRect;  // ���̾� ���� ��ũ�� ��
        [SerializeField] ScrollRect goldShopScrollRect; // ��� ���� ��ũ�� ��

        protected override void OnEnable()
        {
            base.OnEnable();
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
    }
}