using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;
using System.Linq;
using HeroSelector = Portfolio.Lobby.Hero.UnitSlotSelector_HeroStatus;                          // // Ŭ���� �̸��� �ʹ� �� ��Ī ���
using CompositionSelector = Portfolio.Lobby.Hero.Composition.UnitSlotSelector_HeroComposition;  // // Ŭ���� �̸��� �ʹ� �� ��Ī ���

/*
 * ������ ������ �����ִ� ����Ʈ �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;             // ���� ���� ����Ʈ
        [SerializeField] ScrollRect unitScrollView;                 // ���� ���� ��ũ�� ��
        [SerializeField] TextMeshProUGUI unitListCountText;         // ���� ���� ������ ǥ���ϴ� �ؽ�Ʈ

        List<HeroSelector> unitSlotHeroStatusSelectors = new List<HeroSelector>();                      // ���� ����â�� ����� ������
        List<CompositionSelector> unitSlotHeroCompositionSelectors = new List<CompositionSelector>();   // ���� �ռ�â�� ����� ������


        // �ʱ�ȭ �մϴ�.
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

        // â�� ������ ���� ����Ʈ�� �����ݴϴ�.
        private void OnEnable()
        {
            ShowUnitList();
        }

        // â�� ������ ���� ����Ʈ���� �������ͽ�â�� �����ϵ��� �ʱ�ȭ�մϴ�.
        private void OnDisable()
        {
            SetStatus();
        }

        // ���� ����Ʈ�� �����ݴϴ�.
        public void ShowUnitList()
        {
            var userUnitSortingList = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                // ������ ���� ���� �� ��ŭ ���� ������ Ȱ��ȭ �մϴ�.
                if (userUnitSortingList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(userUnitSortingList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }

            // ���� ���� ������ ������Ʈ �մϴ�.
            ShowUnitListCountText();
        }

        // ���� ������ �������ͽ� ���� �����͸� Ȱ��ȭ ��ŵ�ϴ�.
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

            // ������ ������ �ʱ�ȭ �մϴ�.
            ResetDefaultList();
        }

        // ���� ������ �ռ�â ���� �����͸� Ȱ��ȭ ��ŵ�ϴ�.
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

            // ������ ������ �ʱ�ȭ �մϴ�.
            ResetCompositionList();
        }

        // ���� ���� ������ �����ݴϴ�.
        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.UserUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
        }

        // �ռ�â�� ������ �� �ռ� ������ ���ֵ��� üũ���ݴϴ�.
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

        // �ռ��� ���� ���Կ� ������ �־����� ���� ����Ʈ�� ������ �ؼ� �����ݴϴ�.
        public void SetCompositionList(CompositionSelector mainSelector)
        {
            // 1. Ȱ��ȭ�� ������ ���� ���� ���ְ� ������ ID�� ���� ���Ը� �߷����ϴ�.
            // 2. ���� ���� �ռ� ������ ���� ���� �ֵ��� �����մϴ�.
            // 3. ���� ���� ����� ���� ���� ������ �����մϴ�.
            var siblingList = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy && slot.CurrentUnit.UnitID == mainSelector.CurrentUnit.UnitID).
                OrderByDescending(slot => slot.CurrentUnit == mainSelector.CurrentUnit). 
                ThenByDescending(slot => slot.CurrentUnit.UnitGrade == mainSelector.CurrentUnit.UnitGrade). 
                ToList();

            // �� ���� ����Ʈ���� ���̾ƿ��� ������ �մϴ�.
            for (int i = 0; i < siblingList.Count; i++)
            {
                siblingList[i].transform.SetSiblingIndex(i);
            }

            // ���� ���ĵ��� ���� ������ ǥ���մϴ�.
            var list = unitSlotList.Where(slot => slot.gameObject.activeInHierarchy).
                Where(slot => slot.CurrentUnit.UnitID != mainSelector.CurrentUnit.UnitID || slot.CurrentUnit.UnitGrade != mainSelector.CurrentUnit.UnitGrade);
                
            // ���� ���ĵ��� ���� ����Ʈ�� ���� �Ұ��մϴ�.
            foreach (var item in list)
            {
                item.GetComponent<CompositionSelector>().CanSelect = false;
            }
        }

        // ���� ����Ʈ�� ���̾ƿ��� �ٽ� �������մϴ�.
        public void ResetDefaultList()
        {
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                unitSlotList[i].transform.SetSiblingIndex(i);
            }
        }
    }
}