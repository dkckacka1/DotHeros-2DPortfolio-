using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Portfolio.WorldMap
{
    public class FomationGrid : MonoBehaviour, IDropHandler
    {
        [SerializeField] UnitSlotUI unitSlotUI;
        [SerializeField] FomationTargetUnitUI fomationTargetSlotUI;

        FomationSlotUI currentFomationSlotUI;

        public Unit GetCurrentUnit => unitSlotUI.CurrentUnit;

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

                unitSlotUI.Init(fomationTargetSlotUI.SelectUnit);
                unitSlotUI.gameObject.SetActive(true);
            }
        }
    }

}