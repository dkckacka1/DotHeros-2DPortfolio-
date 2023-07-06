using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitSlotSelector_HeroStatus : MonoBehaviour
    {
        UnitSlotUI unitSlotUI;
        Button button;

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(UnitStatusSelect);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(UnitStatusSelect);
        }

        public void UnitStatusSelect()
        {
            HeroPanelUI.SelectUnit = unitSlotUI.CurrentUnit;
        }
    }
}