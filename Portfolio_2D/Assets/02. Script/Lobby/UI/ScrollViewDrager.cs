using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class ScrollViewDrager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [SerializeField] ScrollRect scrollView;

        private void Awake()
        {
            if (scrollView == null)
            {
                Destroy(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            scrollView.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            scrollView.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            scrollView.OnEndDrag(eventData);
        }
    }
}