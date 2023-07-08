using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ���� Ȯ�� �˾�â���� ���� ���� �� ���� Ȯ���� �����ִ� UI Ŭ����
 * ���� ���� UI ������Ʈ�� ������ ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.Lobby.Summon
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitProbaility : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unitNameText;      // ������ �̸��� �����ִ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI probabilityText;   // Ȯ���� �����ִ� �ؽ�Ʈ

        UnitSlotUI unitSlotUI;

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
        }

        // Ȯ���� �����ݴϴ�.
        public void Show(float probability)
        {
            unitNameText.text = unitSlotUI.CurrentUnitData.unitName;
            // �Ҽ��� 2�ڸ����� ǥ���մϴ�.
            probabilityText.text = $"{(probability * 100).ToString("0.00")} %";
        }
    }

}