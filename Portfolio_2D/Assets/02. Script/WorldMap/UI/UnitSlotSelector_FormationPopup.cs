using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TargetSelector = Portfolio.WorldMap.UnitSlotSelector_FormationPopupTarget;    // ������ �̸��� �ʹ� �� ��Ī ���

// ORDER : �巡�� �� ������� ���� ���� ������ ���� �ý���
/*
 * ���� ������ �巡�׾� ����ؼ� ������ ���� �� �ְ��ϴ� ������ Ŭ����
 * ���� ���� UI���ִ� ������Ʈ���� ������ �� �ִ�.
 */
namespace Portfolio.WorldMap
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_FormationPopup : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        UnitSlotUI mineSlot;                                    // �ڽ��� ���� ���� UI
        [SerializeField] TargetSelector targetUnitUI;           // �����̼��� �巡�� ����

        [SerializeField] GameObject SelectedUI;                 // ���������� �����ִ� ������Ʈ

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

        // �� ���Կ��� �巡�׸� ����������
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isSelect)
                // ���� �ȵ� �����̶��
            {
                // �巡�� ���Կ� �ڽ��� ������ �־��ݴϴ�
                targetUnitUI.UnitSlotUI.ShowUnit(mineSlot.CurrentUnit, false, false);
                targetUnitUI.selectFomationSlotUI = this;
                // �巡�� ������ �����ݴϴ�.
                targetUnitUI.gameObject.SetActive(true);
                // �巡�� ������ �ڽ��� ���� ��ġ���� ���۵ǰ� �մϴ�.
                targetUnitUI.transform.position = eventData.position;
            }
        }

        // �巡�����϶�
        public void OnDrag(PointerEventData eventData)
        {
            if (!isSelect)
                // ���� �ȵ� �����̶��
            {
                // �巡�� ������ ��ġ�� ���� ���콺 �������� ��ġ�� �̵��Ѵ�.
                targetUnitUI.transform.position = eventData.position;
            }
        }

        // �巡�װ� ���� ������
        public void OnEndDrag(PointerEventData eventData)
        {
            // �巡�� ������ �ʱ�ȭ �ϰ� �����ش�.
            targetUnitUI.selectFomationSlotUI = null;
            targetUnitUI.gameObject.SetActive(false);
        }

        // �� ������ �����մϴ�.
        public void Select()
        {
            isSelect = true;
            SelectedUI.gameObject.SetActive(true);
        }

        // �� ������ ���� ����մϴ�.
        public void UnSelect()
        {
            isSelect = false;
            SelectedUI.gameObject.SetActive(false);
        }
    }

}