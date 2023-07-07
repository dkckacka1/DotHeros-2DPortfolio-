using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 상품 패널에서 다이아를 구매하는 노드를 표시하는 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopDiaNodeUI : ShopNodeUI
    {
        [Header("DiaNode")]
        [SerializeField] TextMeshProUGUI getDiaText;    // 얻을 다이아양을 표시하는 텍스트
        [SerializeField] int GetDiaValue = 100;         // 얻을 다이아양

        protected override void Start()
        {
            base.Start();
            // 얻을 다이아를 표시합니다.
            getDiaText.text = $"{GetDiaValue} 개";
        }

        // 상품을 구매합니다.
        protected override void GetProduct()
        {
            // 유저에 다이아를 추가합니다.
            GameManager.CurrentUser.Diamond += GetDiaValue;
        }
    }
}
