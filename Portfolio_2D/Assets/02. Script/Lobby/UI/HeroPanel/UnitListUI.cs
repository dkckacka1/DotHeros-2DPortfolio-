using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;
using System.Linq;
using HeroSelector = Portfolio.Lobby.Hero.UnitSlotSelector_HeroStatus;                          // // 클래스 이름이 너무 길어서 별칭 사용
using CompositionSelector = Portfolio.Lobby.Hero.Composition.UnitSlotSelector_HeroComposition;  // // 클래스 이름이 너무 길어서 별칭 사용

/*
 * 유저의 유닛을 보여주는 리스트 패널 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;             // 유닛 슬롯 리스트
        [SerializeField] ScrollRect unitScrollView;                 // 유닛 슬롯 스크롤 뷰
        [SerializeField] TextMeshProUGUI unitListCountText;         // 유닛 슬롯 개수를 표시하는 텍스트

        List<HeroSelector> unitSlotHeroStatusSelectors = new List<HeroSelector>();                      // 유닛 상태창과 연결된 셀렉터
        List<CompositionSelector> unitSlotHeroCompositionSelectors = new List<CompositionSelector>();   // 유닛 합성창과 연결된 셀렉터


        // 초기화 합니다.
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

        // 창이 켜지면 유닛 리스트를 보여줍니다.
        private void OnEnable()
        {
            ShowUnitList();
        }

        // 창이 꺼지면 유닛 리스트들을 스테이터스창과 연동하도록 초기화합니다.
        private void OnDisable()
        {
            SetStatus();
        }

        // 유닛 리스트를 보여줍니다.
        public void ShowUnitList()
        {
            var userUnitSortingList = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                // 유저가 가진 유닛 수 만큼 유닛 슬롯을 활성화 합니다.
                if (userUnitSortingList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(userUnitSortingList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }

            // 유닛 슬롯 개수를 업데이트 합니다.
            ShowUnitListCountText();
        }

        // 유닛 슬롯의 스테이터스 연동 셀렉터만 활성화 시킵니다.
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

            // 보여줄 순서를 초기화 합니다.
            ResetDefaultList();
        }

        // 유닛 슬롯의 합성창 연동 셀렉터만 활성화 시킵니다.
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

            // 보여줄 순서를 초기화 합니다.
            ResetCompositionList();
        }

        // 유닛 슬롯 갯수를 보여줍니다.
        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.UserUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
        }

        // 합성창을 눌렀을 때 합성 가능한 유닛들을 체크해줍니다.
        public void ResetCompositionList()
        {
            foreach (var selector in unitSlotHeroCompositionSelectors)
            {
                Debug.Log($"{selector == null} : {selector.CurrentUnit == null}");

                if (selector.CurrentUnit != null && selector.CurrentUnit.UnitGrade == 5)
                {
                    selector.CanSelect = false;
                    continue;
                }

                selector.CanSelect = true;
            }
        }

        // 합성시 메인 슬롯에 유닛을 넣었을때 유닛 리스트를 재정렬 해서 보여줍니다.
        public void SetCompositionList(CompositionSelector mainSelector)
        {
            // 1. 활성화된 슬롯중 메인 슬롯 유닛과 동일한 ID를 가진 슬롯만 추려냅니다.
            // 2. 개중 메인 합성 유닛을 가장 먼저 있도록 정렬합니다.
            // 3. 이후 같은 등급을 가진 유닛 순으로 정렬합니다.
            var siblingList = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy && slot.CurrentUnit.UnitID == mainSelector.CurrentUnit.UnitID).
                OrderByDescending(slot => slot.CurrentUnit == mainSelector.CurrentUnit). 
                ThenByDescending(slot => slot.CurrentUnit.UnitGrade == mainSelector.CurrentUnit.UnitGrade). 
                ToList();

            // 위 정렬 리스트부터 레이아웃을 재정렬 합니다.
            for (int i = 0; i < siblingList.Count; i++)
            {
                siblingList[i].transform.SetSiblingIndex(i);
            }

            // 이후 정렬되지 않은 순으로 표시합니다.
            var list = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy).
                Where(slot => slot.CurrentUnit.UnitID != mainSelector.CurrentUnit.UnitID || slot.CurrentUnit.UnitGrade != mainSelector.CurrentUnit.UnitGrade);
                
            // 위에 정렬되지 않은 리스트는 선택 불가합니다.
            foreach (var item in list)
            {
                item.GetComponent<CompositionSelector>().CanSelect = false;
            }
        }

        // 유닛 리스트의 레이아웃을 다시 재정렬합니다.
        public void ResetDefaultList()
        {
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                unitSlotList[i].transform.SetSiblingIndex(i);
            }
        }
    }
}