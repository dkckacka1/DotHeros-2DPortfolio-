using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 포메이션 팝업창에서 유닛 슬롯을 드래그 할때 보여주는 드래그 전용 유닛 슬롯
 * 유닛 슬롯 UI가있는 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.WorldMap
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_FormationPopupTarget : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // 유닛 슬롯 UI

        public UnitSlotSelector_FormationPopup selectFomationSlotUI;    // 선택한 셀렉터ㅣ
        public UnitSlotUI UnitSlotUI => unitSlotUI;                     // 현재 유닛 슬롯 UI
        public bool IsSelectUnit => selectFomationSlotUI != null;       // 현재 선택한 유닛이 있는지
        public Unit SelectUnit => selectFomationSlotUI.CurrentUnit;     // 현재 선택한 유닛
    }
}