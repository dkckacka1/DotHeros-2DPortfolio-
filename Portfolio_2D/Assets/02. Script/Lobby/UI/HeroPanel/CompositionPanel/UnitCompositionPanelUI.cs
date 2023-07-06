using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 영웅 합성 창 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitCompositionPanelUI : MonoBehaviour
    {
        [SerializeField] Button heroCompositionBtn;         // 영웅 합성 버튼
        [SerializeField] Button releaseCompositionBtn;      // 되돌리기 버튼
        [SerializeField] TextMeshProUGUI explainText;       // 영웅 합성에 대한 설명
        [SerializeField] UnitListUI unitListUI;             // 영웅창의 유닛 리스트 UI

        [Header("CompositionSlot")]
        [SerializeField] CompositionUnitSlot mainUnitSlot;                                          // 합성할 유닛 슬롯
        [SerializeField] List<CompositionUnitSlot> subUnitSlotList;                                 // 재료 유닛 슬롯
        [HideInInspector] public CompositionUnitSlot selectedCompositionUnitSlot;                   // 현재 넣어야할 유닛 슬롯
        private Stack<CompositionUnitSlot> insertUnitSlotStack = new Stack<CompositionUnitSlot>();  // 넣은 유닛 리스트

        private int putInUnitCount; // 넣어야할 재료 유닛 수

        // 창이 꺼질때 초기화 한다.
        private void OnDisable()
        {
            Reset();
        }

        // 처음 넣어야할 슬롯은 메인 슬롯
        private void OnEnable()
        {
            SelectSlot(mainUnitSlot);
        }

        // 초기화 해준다.
        private void Reset()
        {
            // 메인 슬롯과 재료 슬롯을 초기화 해준다.
            mainUnitSlot.Reset();
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }

            // 현재 선택한 유닛 셀렉터를 초기화 한다.
            selectedCompositionUnitSlot = null;
            // 넣은 갯수도 초기화
            putInUnitCount = 0;
            // 버튼 상호작용도 비활성화 한다.
            heroCompositionBtn.interactable = false;
            releaseCompositionBtn.interactable = false;
            // 스택도 비워준다.
            insertUnitSlotStack.Clear();
        }

        // 슬롯을 변경한다.
        private void SelectSlot(CompositionUnitSlot slot)
        {
            if (selectedCompositionUnitSlot != null)
                // 기선택된 슬롯이 있다면
            {
                // 선택 취소 상태로 만든다.
                selectedCompositionUnitSlot.IsSelect = false;
            }

            // 슬롯 변경
            selectedCompositionUnitSlot = slot;

            if (selectedCompositionUnitSlot != null)
                // 변경한 슬롯이 null 이 아니라면
            {
                // 선택 상태로만들어준다.
                selectedCompositionUnitSlot.IsSelect = true;
            }
        }

        // 슬롯에 유닛을 넣는다.
        public void InsertUnit(UnitSlotHeroCompositionSelector selector)
        {
            // 넣을 슬롯이 null 이면 리턴
            if (selectedCompositionUnitSlot == null) return;
            // 이미 선택한 유닛 슬롯이면 리턴
            if (selector.IsSelected) return;

            // 넣은 유닛 스택에 넣어준다.
            insertUnitSlotStack.Push(selectedCompositionUnitSlot);
            // 슬롯에 유닛을 넣어준다.
            selectedCompositionUnitSlot.ShowUnit(selector.CurrentUnit);
            selectedCompositionUnitSlot.selector = selector;

            if (selectedCompositionUnitSlot == mainUnitSlot)
                // 넣은 슬롯이 메인 슬롯이면
            {
                // 메인 유닛으로 설정
                SetMainUnit(selector);
                selector.IsMainSelect = true;
            }
            else
                // 넣은 슬롯이 재료 슬롯이면
            {
                // 재료 유닛으로 설정
                selector.IsSubSelect = true;
            }

            // 되돌리기 버튼 상호작용 활성화
            releaseCompositionBtn.interactable = true;
            // 넣은 유닛 수가 넣어야할 유닛 수와 동일하다면 합성 버튼 활성화
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;

            if (putInUnitCount > insertUnitSlotStack.Count)
                // 재료 유닛 수가 충족되지 않았을 경우
            {
                // 다음 재료 슬롯을 활성화 한다.
                SelectSlot(GetNextSlot(selectedCompositionUnitSlot));
            }
            else
            {
                Debug.LogWarning("다음에 들어갈 슬롯이 없습니다.");
                SelectSlot(null);
            }
        }

        // 다음 슬롯을 찾는다.
        private CompositionUnitSlot GetNextSlot(CompositionUnitSlot currentSlot)
        {
            if (currentSlot == mainUnitSlot)
                // 현재 슬롯이 메인 슬롯이라면
            {
                // 첫번 째 재료 슬롯을 리턴한다.
                return subUnitSlotList[0];
            }
            else
            {
                for (int i = 0; i < subUnitSlotList.Count; i++)
                {
                    if (currentSlot == subUnitSlotList[i])
                    {
                        if (i == subUnitSlotList.Count - 1)
                            // 현재 슬롯이 마지막 재료 슬롯이면 중지
                        {
                            break;
                        }
                        else
                            // 현재 슬롯이 마지막 재료 슬롯이 아니면 다음 슬롯 리턴
                        {
                            return subUnitSlotList[i + 1];
                        }
                    }
                }

                return null;
            }

        }

        // 메인 슬롯에 유닛을 넣었을 때
        private void SetMainUnit(UnitSlotHeroCompositionSelector mainSelector)
        {
            // 넣어야할 유닛은 메인 유닛의 등급 + 1 개 (이미 메인슬롯에 한개가 들어가 있기 때문)
            putInUnitCount = mainSelector.CurrentUnit.UnitGrade + 1;
            foreach (var slot in subUnitSlotList)
                // 재료 슬롯을 초기화 한다.
            {
                slot.Reset();
            }

            for (int i = 0; i < subUnitSlotList.Count; i++)
            {
                if (putInUnitCount - 1 <= i)
                    // 필요한 재료 수 만큼 재료 슬롯을 활성화 한다.
                {
                    subUnitSlotList[i].ShowLock();
                }
            }

            // 메인 유닛에 맞게 영웅 리스트를 정렬해준다.
            unitListUI.SetCompositionList(mainSelector);
        }

        // 되돌리기 버튼
        public void BTN_OnClick_ReleaseUnit()
        {
            // 스택에 마지막으로 들어간 슬롯을 선택한다.
            SelectSlot(insertUnitSlotStack.Pop());

            // 해당 슬롯을 초기화 해준다.
            selectedCompositionUnitSlot.selector.ResetSelect();
            selectedCompositionUnitSlot.Reset();
            selectedCompositionUnitSlot.IsSelect = true;

            if (insertUnitSlotStack.Count == 0)
                // 만약 메인슬롯의 유닛이 빠졌다면 영웅 합성창을 초기화 해준다.
            {
                unitListUI.ResetCompositionList();
            }

            // 스택이 비워졌다면 되돌리기 버튼 상호작용을 비활성화 한다.
            releaseCompositionBtn.interactable = insertUnitSlotStack.Count != 0;
            // 넣은 유닛 수가 넣어야할 유닛 수와 동일하다면 합성 버튼 활성화
            heroCompositionBtn.interactable = putInUnitCount == insertUnitSlotStack.Count;
        }

        // 유닛 합성 버튼
        public void BTN_OnClick_CompositionUnit()
        {
            // 영웅 합성 시 합성 완료된 영웅은 메인, 재료 유닛의 가장 높은 레벨로 설정
            int setUnitLevel = Mathf.Max(mainUnitSlot.CurrentUnit.UnitCurrentLevel, subUnitSlotList.Where(slot => slot.CurrentUnit != null).Select(slot => slot.CurrentUnit.UnitCurrentLevel).Max());
            mainUnitSlot.CurrentUnit.UnitCurrentLevel = setUnitLevel;
            // 메인 유닛의 등급 업
            mainUnitSlot.CurrentUnit.UnitGrade += 1;

            // 재료 슬롯에 들어간 유닛들을 유저의 유닛 리스트에서 뺴준다.
            foreach (var slot in subUnitSlotList)
            {
                if (slot.CurrentUnit != null)
                {
                    // 빼기 전 없앨 유닛이 장비 아이템을 장착 하고 있었던 경우 장비 아이템을 해제 해준다.
                    GameManager.CurrentUser.GetUnitEquipment(slot.CurrentUnit);
                    GameManager.CurrentUser.userUnitList.Remove(slot.CurrentUnit);
                }
            }

            // 영웅 합성창을 초기화 해준다.
            Reset();
            unitListUI.ResetCompositionList();
            unitListUI.ShowUnitList();
            SelectSlot(mainUnitSlot);
        }
    }

}