using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;
using System.Linq;
using HeroSelector = Portfolio.Lobby.Hero.UnitSlotSelector_HeroStatus;                          // // 클래스 이름이 너무 길어서 별칭 사용
using CompositionSelector = Portfolio.Lobby.Hero.Composition.UnitSlotSelector_HeroComposition;  // // 클래스 이름이 너무 길어서 별칭 사용

namespace Portfolio.Lobby.Hero
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;
        [SerializeField] ScrollRect unitScrollView;
        [SerializeField] TextMeshProUGUI unitListCountText;

        List<HeroSelector> unitSlotHeroStatusSelectors = new List<HeroSelector>();
        List<CompositionSelector> unitSlotHeroCompositionSelectors = new List<CompositionSelector>();


        public void Init()
        {
            unitSlotList = new List<UnitSlotUI>();
            foreach (var slot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(slot);
                unitSlotHeroStatusSelectors.Add(slot.GetComponent<HeroSelector>());
                unitSlotHeroCompositionSelectors.Add(slot.GetComponent<CompositionSelector>());
            }
        }

        private void OnEnable()
        {
            ShowUnitList();
        }

        private void OnDisable()
        {
            SetStatus();
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
            foreach(var statusSelector in unitSlotHeroStatusSelectors)
            {
                statusSelector.IsActive = true;
            }

            foreach (var compositionSelector in unitSlotHeroCompositionSelectors)
            {
                compositionSelector.IsActive = false;
            }
            ResetDefaultList();
        }

        public void SetComposition()
        {
            foreach (var statusSelector in unitSlotHeroStatusSelectors)
            {
                statusSelector.IsActive = false;
            }

            foreach (var compositionSelector in unitSlotHeroCompositionSelectors)
            {
                compositionSelector.IsActive = true;
            }
            ResetCompositionList();
        }

        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.UserUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
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

        public void SetCompositionList(CompositionSelector mainSelector)
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
                
            foreach (var item in list)
            {
                Debug.Log(item.GetComponent<CompositionSelector>() == null);
                item.GetComponent<CompositionSelector>().CanSelect = false;
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