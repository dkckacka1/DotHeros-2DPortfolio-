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
        [SerializeField] UnitSlotUI targetSlot;

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
            targetSlot.Init(mineSlot.CurrentUnit, false, false);
            targetSlot.gameObject.SetActive(true);
            targetSlot.transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            targetSlot.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            targetSlot.gameObject.SetActive(false);
        }
    }

}