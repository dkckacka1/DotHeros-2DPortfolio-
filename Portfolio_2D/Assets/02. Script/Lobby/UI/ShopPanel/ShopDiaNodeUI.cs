using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ��ǰ �гο��� ���̾Ƹ� �����ϴ� ��带 ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public class ShopDiaNodeUI : ShopNodeUI
    {
        [Header("DiaNode")]
        [SerializeField] TextMeshProUGUI getDiaText;    // ���� ���̾ƾ��� ǥ���ϴ� �ؽ�Ʈ
        [SerializeField] int GetDiaValue = 100;         // ���� ���̾ƾ�

        protected override void Start()
        {
            base.Start();
            // ���� ���̾Ƹ� ǥ���մϴ�.
            getDiaText.text = $"{GetDiaValue} ��";
        }

        // ��ǰ�� �����մϴ�.
        protected override void GetProduct()
        {
            // ������ ���̾Ƹ� �߰��մϴ�.
            GameManager.CurrentUser.Diamond += GetDiaValue;
        }
    }
}
