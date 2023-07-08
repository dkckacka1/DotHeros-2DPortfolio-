using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * 스크롤 뷰를 이동시키기 위한 UI 클래스
 * 스크롤 뷰내의 요소가 별도의 버튼이나 트리거를 가지고 있으면 스크롤 뷰와 연동이 안되기 때문에 따로 만들어줘야 한다.
 */

namespace Portfolio.Lobby
{
    public class ScrollViewDrager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [SerializeField] ScrollRect scrollView; // 작동시킬 스크롤 뷰

        private void Awake()
        {
            if (scrollView == null)
            {
                Destroy(this);
            }
        }

        // 드래그 시작 이벤트를 스크롤 뷰에 전달한다.
        public void OnBeginDrag(PointerEventData eventData)
        {
            scrollView.OnBeginDrag(eventData);
        }

        // 드래그 중 이벤트를 스크롤 뷰에 전달한다.
        public void OnDrag(PointerEventData eventData)
        {
            scrollView.OnDrag(eventData);
        }

        // 드래그 종료 이벤트를 스크롤 뷰에 전달한다.
        public void OnEndDrag(PointerEventData eventData)
        {
            scrollView.OnEndDrag(eventData);
        }
    }
}