using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Portfolio.WorldMap
{
    public class FomationSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        UnitSlotUI mineSlot;
        [SerializeField] FomationTargetUnitUI targetUnitUI;

        [SerializeField] GameObject SelectedUI;

        public Unit CurrentUnit => mineSlot.CurrentUnit;

        private bool isSelect = false;

        private void Awake()
        {
            mineSlot = GetComponent<UnitSlotUI>();
            if (mineSlot == null)
            {
                this.enabled = false;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isSelect)
            {
                targetUnitUI.UnitSlotUI.Init(mineSlot.CurrentUnit, false, false);
                targetUnitUI.selectFomationSlotUI = this;
                targetUnitUI.gameObject.SetActive(true);
                targetUnitUI.transform.position = eventData.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isSelect)
            {
                targetUnitUI.transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            targetUnitUI.selectFomationSlotUI = null;
            targetUnitUI.gameObject.SetActive(false);
        }

        public void Select()
        {
            isSelect = true;
            SelectedUI.gameObject.SetActive(true);
        }

        public void UnSelect()
        {
            isSelect = false;
            SelectedUI.gameObject.SetActive(false);
        }
    }

}