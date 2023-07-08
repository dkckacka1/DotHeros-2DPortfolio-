using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����̼� �˾�â���� ���� ������ �巡�� �Ҷ� �����ִ� �巡�� ���� ���� ����
 */

namespace Portfolio.WorldMap
{
    public class FormationTargetUnitUI : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // ���� ���� UI

        public FormationSlotUI selectFomationSlotUI;                    // ������ ������
        public UnitSlotUI UnitSlotUI => unitSlotUI;                     // ���� ���� ���� UI
        public bool IsSelectUnit => selectFomationSlotUI != null;       // ���� ������ ������ �ִ���
        public Unit SelectUnit => selectFomationSlotUI.CurrentUnit;     // ���� ������ ����
    }
}