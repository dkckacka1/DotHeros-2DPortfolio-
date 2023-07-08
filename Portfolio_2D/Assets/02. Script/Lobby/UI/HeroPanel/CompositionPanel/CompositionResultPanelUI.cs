using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * �ռ� ����� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class CompositionResultPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // ���� ����
        [SerializeField] List<UnitGradeUpUI> unitGradeUIList;           // ���� ��� �̹�����
        [SerializeField] TextMeshProUGUI unitNameText; // ���� �̸� �ؽ�Ʈ

        // ����� �����ش�.
        public void Show(Unit unit)
        {
            this.gameObject.SetActive(true);

            // ���� ���԰� �̸��� �����Ѵ�.
            unitSlotUI.ShowUnit(unit, false, false);
            unitNameText.text = unit.UnitName;

            // ���� ��޿� �°� �� Ȱ��ȭ
            for (int i = 0; i < 5; i++)
            {
                unitGradeUIList[i].IsActive = unit.UnitGrade >= (i + 1);
            }

            // ������ ����� ����Ʈ ���
            unitGradeUIList[unit.UnitGrade - 1].PlayEffect();

            LobbyManager.UIManager.AddUndo(this);
        }

        // ���â�� �� �� �ֵ��� Undo �������̽� ����
        public void Undo()
        {
            this.gameObject.SetActive(false);
        }
    }
}