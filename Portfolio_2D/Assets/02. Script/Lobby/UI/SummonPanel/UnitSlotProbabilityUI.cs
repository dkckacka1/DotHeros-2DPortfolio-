using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Lobby.Summon
{
    public class UnitSlotProbabilityUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] TextMeshProUGUI probabilityText;

        UnitSlotUI unitSlotUI;

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
        }

        public void Show(float probability)
        {
            unitNameText.text = unitSlotUI.CurrentUnitData.unitName;
            probabilityText.text = $"{(probability * 100).ToString("0.00")} %";
        }
    }

}