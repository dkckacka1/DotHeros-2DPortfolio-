using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitSlotHeroStatusSelector : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;
        [SerializeField] Button button;

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