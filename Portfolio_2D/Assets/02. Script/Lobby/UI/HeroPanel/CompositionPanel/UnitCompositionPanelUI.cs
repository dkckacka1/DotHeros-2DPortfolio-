using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �ռ� â UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitCompositionPanelUI : MonoBehaviour
    {
        [SerializeField] Button heroCompositionBtn;         // ���� �ռ� ��ư
        [SerializeField] Button releaseCompositionBtn;      // �ǵ����� ��ư
        [SerializeField] TextMeshProUGUI explainText;       // ���� �ռ��� ���� ����
        [SerializeField] UnitListUI unitListUI;             // ����â�� ���� ����Ʈ UI

        [Header("CompositionSlot")]
        [SerializeField] CompositionUnitSlot mainUnitSlot;                                          // �ռ��� ���� ����
        [SerializeField] List<CompositionUnitSlot> subUnitSlotList;                                 // ��� ���� ����
        [HideInInspector] public CompositionUnitSlot selectedCompositionUnitSlot;                   // ���� �־���� ���� ����
        private Stack<CompositionUnitSlot> insertUnitSlotStack = new Stack<CompositionUnitSlot>();  // ���� ���� ����Ʈ

        private int putInUnitCount; // �־���� ��� ���� ��

        // â�� ������ �ʱ�ȭ �Ѵ�.
        private void OnDisable()
        {
            Reset();
        }

        // ó�� �־���� ������ ���� ����
        private void OnEnable()
        {
            SelectSlot(mainUnitSlot);
        }

        // �ʱ�ȭ ���ش�.
        private void Reset()
        {
            // ���� ���԰� ��� ������ �ʱ�ȭ ���ش�.
            mainUnitSlot.Reset();
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }

            // ���� ������ ���� �����͸� �ʱ�ȭ �Ѵ�.
            selectedCompositionUnitSlot = null;
            // ���� ������ �ʱ�ȭ
            putInUnitCount = 0;
            // ��ư ��ȣ�ۿ뵵 ��Ȱ��ȭ �Ѵ�.
            heroCompositionBtn.interactable = false;
            releaseCompositionBtn.interactable = false;
            // ���õ� ����ش�.
            insertUnitSlotStack.Clear();
        }

        // ������ �����Ѵ�.
        private void SelectSlot(CompositionUnitSlot slot)
        {
            if (selectedCompositionUnitSlot != null)
                // �⼱�õ� ������ �ִٸ�
            {
                // ���� ��� ���·� �����.
                selectedCompositionUnitSlot.IsSelect = false;
            }

            // ���� ����
            selectedCompositionUnitSlot = slot;

            if (selectedCompositionUnitSlot != null)
                // ������ ������ null �� �ƴ϶��
            {
                // ���� ���·θ�����ش�.
                selectedCompositionUnitSlot.IsSelect = true;
            }
        }

        // ���Կ� ������ �ִ´�.
        public void InsertUnit(UnitSlotHeroCompositionSelector selector)
        {
            // ���� ������ null �̸� ����
            if (selectedCompositionUnitSlot == null) return;
            // �̹� ������ ���� �����̸� ����
            if (selector.IsSelected) return;

            // ���� ���� ���ÿ� �־��ش�.
            insertUnitSlotStack.Push(selectedCompositionUnitSlot);
            // ���Կ� ������ �־��ش�.
            selectedCompositionUnitSlot.ShowUnit(selector.CurrentUnit);
            selectedCompositionUnitSlot.selector = selector;

            if (selectedCompositionUnitSlot == mainUnitSlot)
                // ���� ������ ���� �����̸�
            {
                // ���� �������� ����
                SetMainUnit(selector);
                selector.IsMainSelect = true;
            }
            else
                // ���� ������ ��� �����̸�
            {
                // ��� �������� ����
                selector.IsSubSelect = true;
            }

            // �ǵ����� ��ư ��ȣ�ۿ� Ȱ��ȭ
            releaseCompositionBtn.interactable = true;
            // ���� ���� ���� �־���� ���� ���� �����ϴٸ� �ռ� ��ư Ȱ��ȭ
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;

            if (putInUnitCount > insertUnitSlotStack.Count)
                // ��� ���� ���� �������� �ʾ��� ���
            {
                // ���� ��� ������ Ȱ��ȭ �Ѵ�.
                SelectSlot(GetNextSlot(selectedCompositionUnitSlot));
            }
            else
            {
                Debug.LogWarning("������ �� ������ �����ϴ�.");
                SelectSlot(null);
            }
        }

        // ���� ������ ã�´�.
        private CompositionUnitSlot GetNextSlot(CompositionUnitSlot currentSlot)
        {
            if (currentSlot == mainUnitSlot)
                // ���� ������ ���� �����̶��
            {
                // ù�� ° ��� ������ �����Ѵ�.
                return subUnitSlotList[0];
            }
            else
            {
                for (int i = 0; i < subUnitSlotList.Count; i++)
                {
                    if (currentSlot == subUnitSlotList[i])
                    {
                        if (i == subUnitSlotList.Count - 1)
                            // ���� ������ ������ ��� �����̸� ����
                        {
                            break;
                        }
                        else
                            // ���� ������ ������ ��� ������ �ƴϸ� ���� ���� ����
                        {
                            return subUnitSlotList[i + 1];
                        }
                    }
                }

                return null;
            }

        }

        // ���� ���Կ� ������ �־��� ��
        private void SetMainUnit(UnitSlotHeroCompositionSelector mainSelector)
        {
            // �־���� ������ ���� ������ ��� + 1 �� (�̹� ���ν��Կ� �Ѱ��� �� �ֱ� ����)
            putInUnitCount = mainSelector.CurrentUnit.UnitGrade + 1;
            foreach (var slot in subUnitSlotList)
                // ��� ������ �ʱ�ȭ �Ѵ�.
            {
                slot.Reset();
            }

            for (int i = 0; i < subUnitSlotList.Count; i++)
            {
                if (putInUnitCount - 1 <= i)
                    // �ʿ��� ��� �� ��ŭ ��� ������ Ȱ��ȭ �Ѵ�.
                {
                    subUnitSlotList[i].ShowLock();
                }
            }

            // ���� ���ֿ� �°� ���� ����Ʈ�� �������ش�.
            unitListUI.SetCompositionList(mainSelector);
        }

        // �ǵ����� ��ư
        public void BTN_OnClick_ReleaseUnit()
        {
            // ���ÿ� ���������� �� ������ �����Ѵ�.
            SelectSlot(insertUnitSlotStack.Pop());

            // �ش� ������ �ʱ�ȭ ���ش�.
            selectedCompositionUnitSlot.selector.ResetSelect();
            selectedCompositionUnitSlot.Reset();
            selectedCompositionUnitSlot.IsSelect = true;

            if (insertUnitSlotStack.Count == 0)
                // ���� ���ν����� ������ �����ٸ� ���� �ռ�â�� �ʱ�ȭ ���ش�.
            {
                unitListUI.ResetCompositionList();
            }

            // ������ ������ٸ� �ǵ����� ��ư ��ȣ�ۿ��� ��Ȱ��ȭ �Ѵ�.
            releaseCompositionBtn.interactable = insertUnitSlotStack.Count != 0;
            // ���� ���� ���� �־���� ���� ���� �����ϴٸ� �ռ� ��ư Ȱ��ȭ
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;
        }

        // ���� �ռ� ��ư
        public void BTN_OnClick_CompositionUnit()
        {
            // ���� �ռ� �� �ռ� �Ϸ�� ������ ����, ��� ������ ���� ���� ������ ����
            int setUnitLevel = Mathf.Max(mainUnitSlot.CurrentUnit.UnitCurrentLevel, subUnitSlotList.Where(slot => slot.CurrentUnit != null).Select(slot => slot.CurrentUnit.UnitCurrentLevel).Max());
            mainUnitSlot.CurrentUnit.UnitCurrentLevel = setUnitLevel;
            // ���� ������ ��� ��
            mainUnitSlot.CurrentUnit.UnitGrade += 1;

            // ��� ���Կ� �� ���ֵ��� ������ ���� ����Ʈ���� ���ش�.
            foreach (var slot in subUnitSlotList)
            {
                if (slot.CurrentUnit != null)
                {
                    // ���� �� ���� ������ ��� �������� ���� �ϰ� �־��� ��� ��� �������� ���� ���ش�.
                    GameManager.CurrentUser.GetUnitEquipment(slot.CurrentUnit);
                    GameManager.CurrentUser.userUnitList.Remove(slot.CurrentUnit);
                }
            }

            // ���� �ռ�â�� �ʱ�ȭ ���ش�.
            Reset();
            unitListUI.ResetCompositionList();
            unitListUI.ShowUnitList();
            SelectSlot(mainUnitSlot);
        }
    }

}