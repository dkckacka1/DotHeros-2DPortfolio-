using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.WorldMap
{
    public class FomationTargetUnitUI : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;

        public FomationSlotUI selectFomationSlotUI;
        public UnitSlotUI UnitSlotUI => unitSlotUI;
        public bool IsSelectUnit => selectFomationSlotUI != null;
        public Unit SelectUnit => selectFomationSlotUI.CurrentUnit;
    }
}