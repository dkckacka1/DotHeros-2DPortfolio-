using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����̼� �˾�â���� ���� ������ �巡�� �Ҷ� �����ִ� �巡�� ���� ���� ����
 * ���� ���� UI���ִ� ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.WorldMap
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_FormationPopupTarget : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // ���� ���� UI

        public UnitSlotSelector_FormationPopup selectFomationSlotUI;    // ������ �����ͤ�
        public UnitSlotUI UnitSlotUI => unitSlotUI;                     // ���� ���� ���� UI
        public bool IsSelectUnit => selectFomationSlotUI != null;       // ���� ������ ������ �ִ���
        public Unit SelectUnit => selectFomationSlotUI.CurrentUnit;     // ���� ������ ����
    }
}