using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ORDER : 드래그 앤 드랍으로 만든 유닛 포지션 배정 시스템
/*
 * 유닛 슬롯을 드래그앤 드랍해서 진형을 만들 수 있게하는 셀렉터 클래스
 */
// TODO : 셀렉터로 변경
namespace Portfolio.WorldMap
{
    public class FormationSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        UnitSlotUI mineSlot;                                    // 자신의 유닛 슬롯 UI
        [SerializeField] FormationTargetUnitUI targetUnitUI;    // 포메이션의 드래그 슬롯

        [SerializeField] GameObject SelectedUI;                 // 선택했음을 보여주는 오브젝트 // TODO 깃발 이미지로 바꿔주기

        public Unit CurrentUnit => mineSlot.CurrentUnit;

        private bool isSelect = false;
        public bool IsSelect => isSelect;

        private void Awake()
        {
            mineSlot = GetComponent<UnitSlotUI>();
            if (mineSlot == null)
            {
                this.enabled = false;
            }
        }

        // 이 슬롯에서 드래그를 시작했을때
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isSelect)
                // 선택 안된 슬롯이라면
            {
                // 드래그 슬롯에 자신의 정보를 넣어줍니다
                targetUnitUI.UnitSlotUI.ShowUnit(mineSlot.CurrentUnit, false, false);
                targetUnitUI.selectFomationSlotUI = this;
                // 드래그 슬롯을 보여줍니다.
                targetUnitUI.gameObject.SetActive(true);
                // 드래그 슬롯을 자신의 슬롯 위치부터 시작되게 합니다.
                targetUnitUI.transform.position = eventData.position;
            }
        }

        // 드래그중일때
        public void OnDrag(PointerEventData eventData)
        {
            if (!isSelect)
                // 선택 안된 슬롯이라면
            {
                // 드래그 슬롯의 위치를 현재 마우스 포인터의 위치로 이동한다.
                targetUnitUI.transform.position = eventData.position;
            }
        }

        // 드래그가 종료 됬을때
        public void OnEndDrag(PointerEventData eventData)
        {
            // 드래그 슬롯을 초기화 하고 숨겨준다.
            targetUnitUI.selectFomationSlotUI = null;
            targetUnitUI.gameObject.SetActive(false);
        }

        // 이 슬롯을 선택합니다.
        public void Select()
        {
            isSelect = true;
            SelectedUI.gameObject.SetActive(true);
        }

        // 이 슬롯을 선택 취소합니다.
        public void UnSelect()
        {
            isSelect = false;
            SelectedUI.gameObject.SetActive(false);
        }
    }

}