using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 포메이션 팝업창에서 유닛 슬롯을 드래그 할때 보여주는 드래그 전용 유닛 슬롯
 */

namespace Portfolio.WorldMap
{
    public class FormationTargetUnitUI : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // 유닛 슬롯 UI

        public FormationSlotUI selectFomationSlotUI;                    // 선택한 셀렉터
        public UnitSlotUI UnitSlotUI => unitSlotUI;                     // 현재 유닛 슬롯 UI
        public bool IsSelectUnit => selectFomationSlotUI != null;       // 현재 선택한 유닛이 있는지
        public Unit SelectUnit => selectFomationSlotUI.CurrentUnit;     // 현재 선택한 유닛
    }
}