using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ��ǰ �гο��� ��带 �����ϴ� ��带 ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopGoldNodeUI : ShopNodeUI
    {
        [Header("GoldNode")]
        [SerializeField] TextMeshProUGUI getDiaText;    // ���� ������ ǥ���ϴ� �ؽ�Ʈ
        [SerializeField] int GetGoldValue = 100;         // ���� ����

        protected override void Start()
        {
            base.Start();
            // ���� ��带 ǥ���մϴ�.
            getDiaText.text = $"{GetGoldValue}";
        }

        // ��ǰ�� �����մϴ�.
        protected override void GetProduct()
        {
            // ������ ���̾Ƹ� �߰��մϴ�.
            GameManager.CurrentUser.Gold += GetGoldValue;
        }
    }

}