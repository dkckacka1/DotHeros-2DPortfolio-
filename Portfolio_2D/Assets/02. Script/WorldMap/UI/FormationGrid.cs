using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * ���� ������ �����ϱ� ���� ���� ����
 */

namespace Portfolio.WorldMap
{
    public class FormationGrid : MonoBehaviour, IDropHandler
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // ���� ���� ������ ������ ���� ����
        [SerializeField] UnitSlotSelector_FormationPopupTarget targetSelector;    // �巡�׷� ���� ���� ���� ������
        [SerializeField] TextMeshProUGUI unitNameText;                  // ���� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI emptyLabel;                    // ��������� ǥ���� �ؽ�Ʈ
        [SerializeField] GameObject btnLayout;                          // ���� ������ ������ ���ִ� ��ư�� ����ִ� ���̾ƿ�

        UnitSlotSelector_FormationPopup currentFomationSlotUI;  // ���� ���ֽ���

        public Unit GetCurrentUnit => unitSlotUI.CurrentUnit;

        // ���ΰ�ħ�մϴ�.
        public void ReShow()
        {
            ShowUnit(GetCurrentUnit);
        }

        // ���� ������ �����ݴϴ�.
        public void ShowUnit(Unit unit)
        {
            bool isUnit = unit != null;
            // ���� ���ο� ���� �ؽ�Ʈ�� ������ ǥ���մϴ�.
            emptyLabel.gameObject.SetActive(!isUnit);
            unitNameText.gameObject.SetActive(isUnit);
            unitSlotUI.gameObject.SetActive(isUnit);

            if (isUnit)
            {
                unitSlotUI.ShowUnit(unit);
                unitNameText.text = unit.UnitName;
            }
        }

        // �� ���������� Ÿ�ٽ����� ����Ǿ��ٸ� ����� ������ ������ �Է��մϴ�.
        public void OnDrop(PointerEventData eventData)
        {
            if (targetSelector.IsSelectUnit)
                // ���� ���õ� Ÿ�ٽ����� �ִٸ�
            {
                if (currentFomationSlotUI == null)
                    // �ش� ������ ������ �������� �ʴ´ٸ�
                {
                    // �� ���Կ� ����� ������ �Է��Ѵ�.
                    currentFomationSlotUI = targetSelector.selectFomationSlotUI;
                    // ���õ� ������ �����Ѵ�.
                    currentFomationSlotUI.Select();
                }
                else
                // ������ ������ �̹� �ִٸ�
                {
                    // �⼱�õ� ������ �����Ѵ�.
                    currentFomationSlotUI.UnSelect();
                    // �� ���Կ� ����� ������ �Է��Ѵ�.
                    currentFomationSlotUI = targetSelector.selectFomationSlotUI;
                    // ���õ� ������ �����Ѵ�.
                    currentFomationSlotUI.Select();
                }

                // SOUND : ���� ���� �˾�â ���� ���� ���
                // ������ ������ ������ �����ݴϴ�.
                Unit selectUnit = targetSelector.SelectUnit;
                ShowUnit(selectUnit);
            }
        }

        // ��ư ���̾ƿ��� �����ݴϴ�.
        public void BTN_OnClick_SetBtn()
        {
            // ���� ���õ� ������ ���ٸ� ����
            if (GetCurrentUnit == null) return;

            btnLayout.gameObject.SetActive(!btnLayout.gameObject.activeInHierarchy);
        }

        // ������ ������ ����մϴ�.
        public void BTN_OnClick_DisableUnit()
        {
            if (GetCurrentUnit == null) return;

            // ������ ������ ����ϰ� �ʱ�ȭ�մϴ�.
            currentFomationSlotUI.UnSelect();
            currentFomationSlotUI = null;
            unitSlotUI.ShowUnit(null, true, true);
            ReShow();

            btnLayout.gameObject.SetActive(false);
        }
    }

}