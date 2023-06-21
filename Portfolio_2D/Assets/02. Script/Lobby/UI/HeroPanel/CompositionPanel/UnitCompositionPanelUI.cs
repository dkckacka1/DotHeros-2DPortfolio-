using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitCompositionPanelUI : MonoBehaviour
    {
        [SerializeField] CompositionUnitSlot mainUnitSlot;
        [SerializeField] List<CompositionUnitSlot> subUnitSlotList;
        [SerializeField] Button heroCompositionBtn;
        [SerializeField] TextMeshProUGUI explainText;
        [SerializeField] UnitListUI unitListUI;

        private UnitSlotHeroCompositionSelector mainSelector;
        private List<UnitSlotHeroCompositionSelector> subSelectors = new List<UnitSlotHeroCompositionSelector>(4) { null, null, null, null };

        [HideInInspector] public CompositionUnitSlot SelectedCompositionUnitSlot;


        private void OnDisable()
        {
            mainUnitSlot.Reset();
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }
        }

        private void OnEnable()
        {
            foreach (var slot in subUnitSlotList)
            {
                slot.ShowLock();
            }

            mainUnitSlot.GetComponent<Toggle>().isOn = true;
        }

        public void SelectUnit(UnitSlotHeroCompositionSelector selector)
        {
            if (SelectedCompositionUnitSlot == null) return;

            SelectedCompositionUnitSlot.ShowUnit(selector.CurrentUnit);

            if (SelectedCompositionUnitSlot == mainUnitSlot)
            {
                if (mainSelector != null)
                {
                    mainSelector.ResetSelect();
                }

                mainSelector = selector;
                mainSelector.IsMainSelect = true;
                SetMainUnit(mainSelector);
                unitListUI.SetCompositionList(mainSelector);
            }
            else
            {
                if (selector == mainSelector) return;

                for (int i = 0; i < subUnitSlotList.Count; i++)
                {
                    if (SelectedCompositionUnitSlot == subUnitSlotList[i])
                    {
                        Debug.Log($"¼±ÅÃµÈ ½½·ÔÀº ¼­ºê À¯´Ö {i}¹ø ½½·ÔÀÔ´Ï´Ù.");
                        if (subSelectors[i] != null)
                        {
                            subSelectors[i].ResetSelect();
                        }

                        subSelectors[i] = selector;
                        subSelectors[i].IsSubSelect = true;
                    }
                }
            }
        }

        private void SetMainUnit(UnitSlotHeroCompositionSelector mainSelector)
        {
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }

            for (int i = 0; i < subUnitSlotList.Count; i++)
            {
                if (mainSelector.CurrentUnit.UnitGrade < i + 1)
                {
                    subUnitSlotList[i].ShowLock();
                }

                if (subSelectors[i] != null)
                {
                    subSelectors[i].ResetSelect();
                    subSelectors[i] = null;
                }
            }
        }
    }

}