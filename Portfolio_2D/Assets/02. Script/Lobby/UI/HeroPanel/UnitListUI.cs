using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;
using System.Linq;
using System;

namespace Portfolio.Lobby.Hero
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;
        [SerializeField] ScrollRect unitScrollView;
        [SerializeField] TextMeshProUGUI unitListCountText;

        List<UnitSlotHeroStatusSelector> unitSlotHeroStatusSelectors = new List<UnitSlotHeroStatusSelector>();
        List<UnitSlotHeroCompositionSelector> unitSlotHeroCompositionSelectors = new List<UnitSlotHeroCompositionSelector>();


        public void Init()
        {
            unitSlotList = new List<UnitSlotUI>();
            foreach (var slot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(slot);
                unitSlotHeroStatusSelectors.Add(slot.GetComponent<UnitSlotHeroStatusSelector>());
                unitSlotHeroCompositionSelectors.Add(slot.GetComponent<UnitSlotHeroCompositionSelector>());
            }
        }

        private void OnEnable()
        {
            ShowUnitList();
            ShowUnitListCountText();
        }

        public void ShowUnitList()
        {
            var userUnitSortingList = GameManager.CurrentUser.userUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitSortingList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].Init(userUnitSortingList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }
        }

        public void SetStatus()
        {
            SetSelector(unitSlotHeroStatusSelectors, true);
            SetSelector(unitSlotHeroCompositionSelectors, false);
        }

        public void SetComposition()
        {
            SetSelector(unitSlotHeroStatusSelectors, false);
            SetSelector(unitSlotHeroCompositionSelectors, true);
            SetCompositionList();
        }

        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.userUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
        }

        private void SetSelector<T>(List<T> selectors, bool isTrue) where T : MonoBehaviour
        {
            foreach (var selector in selectors)
            {
                selector.enabled = isTrue;
            }
        }

        private void SetCompositionList()
        {
            foreach (var selector in unitSlotHeroCompositionSelectors)
            {
                if (selector.CurrentUnit == null && selector.CurrentUnit.UnitGrade == 5)
                {
                    selector.CanSelect = false;
                    continue;
                }

                selector.CanSelect = true;
            }
        }

        public void SetCompositionList(UnitSlotHeroCompositionSelector mainSelector)
        {
            // TODO 유닛 선택 가능 작업중
            var userUnitSortingList = GameManager.CurrentUser.userUnitList.OrderByDescending(unit => unit.UnitID == mainSelector.CurrentUnit.UnitID).ThenByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitSortingList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].Init(userUnitSortingList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }
        }
    }
}