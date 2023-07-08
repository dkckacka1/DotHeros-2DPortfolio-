using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * ��ũ�� �並 �̵���Ű�� ���� UI Ŭ����
 * ��ũ�� �䳻�� ��Ұ� ������ ��ư�̳� Ʈ���Ÿ� ������ ������ ��ũ�� ��� ������ �ȵǱ� ������ ���� �������� �Ѵ�.
 */

namespace Portfolio.Lobby
{
    public class ScrollViewDrager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [SerializeField] ScrollRect scrollView; // �۵���ų ��ũ�� ��

        private void Awake()
        {
            if (scrollView == null)
            {
                Destroy(this);
            }
        }

        // �巡�� ���� �̺�Ʈ�� ��ũ�� �信 �����Ѵ�.
        public void OnBeginDrag(PointerEventData eventData)
        {
            scrollView.OnBeginDrag(eventData);
        }

        // �巡�� �� �̺�Ʈ�� ��ũ�� �信 �����Ѵ�.
        public void OnDrag(PointerEventData eventData)
        {
            scrollView.OnDrag(eventData);
        }

        // �巡�� ���� �̺�Ʈ�� ��ũ�� �信 �����Ѵ�.
        public void OnEndDrag(PointerEventData eventData)
        {
            scrollView.OnEndDrag(eventData);
        }
    }
}