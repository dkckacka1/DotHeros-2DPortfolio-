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

        private bool isActive = true;

        public bool IsActive { get => isActive; set => isActive = value; }

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
            button = GetComponent<Button>();
        }

        public void BTN_OnClick_UnitStatusSelect()
        {
            if(IsActive)
            {
                HeroPanelUI.SelectUnit = unitSlotUI.CurrentUnit;
            }
        }
    }
}