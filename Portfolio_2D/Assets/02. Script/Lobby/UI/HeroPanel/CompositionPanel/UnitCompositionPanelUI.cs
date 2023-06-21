using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitCompositionPanelUI : MonoBehaviour
    {
        [SerializeField] Button heroCompositionBtn;
        [SerializeField] Button releaseCompositionBtn;
        [SerializeField] TextMeshProUGUI explainText;
        [SerializeField] UnitListUI unitListUI;

        [Header("CompositionSlot")]
        [SerializeField] CompositionUnitSlot mainUnitSlot;
        [SerializeField] List<CompositionUnitSlot> subUnitSlotList;
        [HideInInspector] public CompositionUnitSlot selectedCompositionUnitSlot;
        private Stack<CompositionUnitSlot> insertUnitSlotStack = new Stack<CompositionUnitSlot>();

        private int putInUnitCount;


        private void OnDisable()
        {
            Reset();
        }

        private void OnEnable()
        {
            SelectSlot(mainUnitSlot);
        }

        private void Reset()
        {
            mainUnitSlot.Reset();
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }

            selectedCompositionUnitSlot = null;
            putInUnitCount = 0;
            heroCompositionBtn.interactable = false;
            releaseCompositionBtn.interactable = false;
            insertUnitSlotStack.Clear();
        }

        private void SelectSlot(CompositionUnitSlot slot)
        {
            if (selectedCompositionUnitSlot != null)
            {
                selectedCompositionUnitSlot.IsSelect = false;
            }

            selectedCompositionUnitSlot = slot;

            if (selectedCompositionUnitSlot != null)
            {
                selectedCompositionUnitSlot.IsSelect = true;
            }
        }

        public void InsertUnit(UnitSlotHeroCompositionSelector selector)
        {
            if (selectedCompositionUnitSlot == null) return;
            if (selector.IsSelected) return;

            insertUnitSlotStack.Push(selectedCompositionUnitSlot);
            selectedCompositionUnitSlot.ShowUnit(selector.CurrentUnit);
            selectedCompositionUnitSlot.selector = selector;

            if (selectedCompositionUnitSlot == mainUnitSlot)
            {
                SetMainUnit(selector);
                selector.IsMainSelect = true;
            }
            else
            {
                selector.IsSubSelect = true;
            }

            releaseCompositionBtn.interactable = true;
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;

            if (putInUnitCount > insertUnitSlotStack.Count)
            {
                SelectSlot(GetNextSlot(selectedCompositionUnitSlot));
            }
            else
            {
                Debug.Log("다음에 들어갈 슬롯이 없습니다.");
                SelectSlot(null);
            }
        }

        public void ReleaseUnit()
        {
            SelectSlot(insertUnitSlotStack.Pop());

            selectedCompositionUnitSlot.selector.ResetSelect();
            selectedCompositionUnitSlot.Reset();
            selectedCompositionUnitSlot.IsSelect = true;

            if (insertUnitSlotStack.Count == 0)
            {
                unitListUI.ResetCompositionList();
            }

            releaseCompositionBtn.interactable = insertUnitSlotStack.Count != 0;
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;
        }

        private CompositionUnitSlot GetNextSlot(CompositionUnitSlot currentSlot)
        {
            if (currentSlot == mainUnitSlot)
            {
                Debug.Log("메인슬롯의 다음것 리턴");
                return subUnitSlotList[0];
            }
            else
            {
                for (int i = 0; i < subUnitSlotList.Count; i++)
                {
                    if (currentSlot == subUnitSlotList[i])
                    {
                        if (i == subUnitSlotList.Count - 1)
                        {
                            break;
                        }
                        else
                        {
                            Debug.Log($"서브슬롯{i}번 의 다음것 리턴");
                            return subUnitSlotList[i + 1];
                        }
                    }
                }

                Debug.Log($"서브슬롯4번 의 다음은 없다");
                return null;
            }

        }

        private void SetMainUnit(UnitSlotHeroCompositionSelector mainSelector)
        {
            putInUnitCount = mainSelector.CurrentUnit.UnitGrade + 1;
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }

            for (int i = 0; i < subUnitSlotList.Count; i++)
            {
                if (putInUnitCount - 1 <= i)
                {
                    subUnitSlotList[i].ShowLock();
                }
            }

            unitListUI.SetCompositionList(mainSelector);
        }

        public void CompositionUnit()
        {
            int setUnitLevel = Mathf.Max(mainUnitSlot.CurrentUnit.UnitCurrentLevel, subUnitSlotList.Where(slot => slot.CurrentUnit != null).Select(slot => slot.CurrentUnit.UnitCurrentLevel).Max());
            mainUnitSlot.CurrentUnit.UnitCurrentLevel = setUnitLevel;
            mainUnitSlot.CurrentUnit.UnitGrade += 1;
            //Debug.Log(GameManager.CurrentUser.userUnitList.Contains(mainUnitSlot.CurrentUnit));
            foreach (var slot in subUnitSlotList)
            {
                if (slot.CurrentUnit != null)
                {
                    //Debug.Log(GameManager.CurrentUser.userUnitList.Contains(slot.CurrentUnit));
                    GameManager.CurrentUser.GetUnitEquipment(slot.CurrentUnit);
                    GameManager.CurrentUser.userUnitList.Remove(slot.CurrentUnit);
                }
            }
            Reset();
            unitListUI.ResetCompositionList();
            unitListUI.ShowUnitList();
            SelectSlot(mainUnitSlot);
        }
    }

}