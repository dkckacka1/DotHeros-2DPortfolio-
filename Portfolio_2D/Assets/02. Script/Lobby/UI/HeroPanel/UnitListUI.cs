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
        }

        public void ShowUnitList()
        {
            var userUnitSortingList = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
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
            ShowUnitListCountText();
        }

        public void SetStatus()
        {
            SetSelector(unitSlotHeroStatusSelectors, true);
            SetSelector(unitSlotHeroCompositionSelectors, false);
            ResetDefaultList();
        }

        public void SetComposition()
        {
            SetSelector(unitSlotHeroStatusSelectors, false);
            SetSelector(unitSlotHeroCompositionSelectors, true);
            ResetCompositionList();
        }

        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.UserUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
        }

        private void SetSelector<T>(List<T> selectors, bool isTrue) where T : MonoBehaviour
        {
            foreach (var selector in selectors)
            {
                selector.enabled = isTrue;
            }
        }

        public void ResetCompositionList()
        {
            foreach (var selector in unitSlotHeroCompositionSelectors)
            {
                if (selector.CurrentUnit != null && selector.CurrentUnit.UnitGrade == 5)
                {
                    selector.CanSelect = false;
                    continue;
                }

                selector.CanSelect = true;
            }
        }

        public void SetCompositionList(UnitSlotHeroCompositionSelector mainSelector)
        {
            var siblingList = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy && slot.CurrentUnit.UnitID == mainSelector.CurrentUnit.UnitID).
                OrderByDescending(slot => slot.CurrentUnit == mainSelector.CurrentUnit).                // 메인 합성 유닛부터 표시
                ThenByDescending(slot => slot.CurrentUnit.UnitGrade == mainSelector.CurrentUnit.UnitGrade). // 같은 등급을 가진 유닛 표시
                ToList();
            for (int i = 0; i < siblingList.Count; i++)
            {
                siblingList[i].transform.SetSiblingIndex(i);
            }

            var list = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy).
                Where(slot => slot.CurrentUnit.UnitID != mainSelector.CurrentUnit.UnitID || slot.CurrentUnit.UnitGrade != mainSelector.CurrentUnit.UnitGrade);
                //Select(slot => slot.GetComponent<UnitSlotHeroCompositionSelector>().CanSelect = false) ;
                
            foreach (var item in list)
            {
                Debug.Log(item.GetComponent<UnitSlotHeroCompositionSelector>() == null);
                item.GetComponent<UnitSlotHeroCompositionSelector>().CanSelect = false;
            }
        }

        public void ResetDefaultList()
        {
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                unitSlotList[i].transform.SetSiblingIndex(i);
            }
        }
    }
}