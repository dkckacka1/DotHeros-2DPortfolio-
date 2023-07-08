using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 상품 패널에서 골드를 구매하는 노드를 표시하는 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopGoldNodeUI : ShopNodeUI
    {
        [Header("GoldNode")]
        [SerializeField] TextMeshProUGUI getDiaText;    // 얻을 골드양을 표시하는 텍스트
        [SerializeField] int GetGoldValue = 100;         // 얻을 골드양

        protected override void Start()
        {
            base.Start();
            // 얻을 골드를 표시합니다.
            getDiaText.text = $"{GetGoldValue}";
        }

        // 상품을 구매합니다.
        protected override void GetProduct()
        {
            // 유저에 다이아를 추가합니다.
            GameManager.CurrentUser.Gold += GetGoldValue;
        }
    }

}