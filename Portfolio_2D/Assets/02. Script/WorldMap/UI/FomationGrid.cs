using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Portfolio.WorldMap
{
    public class FomationGrid : MonoBehaviour, IDropHandler
    {
        [SerializeField] UnitSlotUI unitSlotUI;
        [SerializeField] FomationTargetUnitUI fomationTargetSlotUI;
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] TextMeshProUGUI emptyLabel;
        [SerializeField] GameObject btnLayout;

        FomationSlotUI currentFomationSlotUI;

        public Unit GetCurrentUnit => unitSlotUI.CurrentUnit;

        public void ReShow()
        {
            ShowUnit(GetCurrentUnit);
        }

        public void ShowUnit(Unit unit)
        {
            bool isUnit = unit != null;
            emptyLabel.gameObject.SetActive(!isUnit);
            unitNameText.gameObject.SetActive(isUnit);
            unitSlotUI.gameObject.SetActive(isUnit);

            if (isUnit)
            {
                unitSlotUI.ShowUnit(unit);
                unitNameText.text = unit.UnitName;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (fomationTargetSlotUI.IsSelectUnit)
            {
                if (currentFomationSlotUI == null)
                {
                    currentFomationSlotUI = fomationTargetSlotUI.selectFomationSlotUI;
                    currentFomationSlotUI.Select();
                }
                else
                {
                    currentFomationSlotUI.UnSelect();
                    currentFomationSlotUI = fomationTargetSlotUI.selectFomationSlotUI;
                    currentFomationSlotUI.Select();
                }

                Unit selectUnit = fomationTargetSlotUI.SelectUnit;

                ShowUnit(selectUnit);
            }
        }

        public void BTN_ONCLICK_SetBtn()
        {
            if (GetCurrentUnit == null) return;

            btnLayout.gameObject.SetActive(!btnLayout.gameObject.activeInHierarchy);
        }

        public void BTN_ONCLICK_DisableUnit()
        {
            if (GetCurrentUnit == null) return;

            currentFomationSlotUI.UnSelect();
            currentFomationSlotUI = null;
            unitSlotUI.ShowUnit(null, true, true);
            ReShow();

            btnLayout.gameObject.SetActive(false);
        }
    }

}