using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.UI;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Selector = Portfolio.Lobby.Inventory.EquipmentSlotSelector_EquipmentInventory; // 클래스 이름이 너무 길어서 별칭 사용

/*
 * 장비아이템 인벤토리를 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Inventory
{
    public class EquipmentInventory : MonoBehaviour
    {
        private List<EquipmentItemSlot> equipmentSlotUIList;  // 장비 슬롯 리스트
        [SerializeField] ScrollRect slotScrollView;             // 장비 슬롯 스크롤 뷰

        [SerializeField] TextMeshProUGUI equipmentListSizeText; // 장비 인벤토리 사이즈 텍스트
        [SerializeField] Toggle multipleSelectionToggle;        // 다중 선택 토글
        [SerializeField] Button equipmentItemDumpBtn;           // 장비 버리기 버튼

        List<Selector> currentSelectorList = new List<Selector>();

        public bool IsMultipleSelection => multipleSelectionToggle.isOn;

        public void Init()
        {
            equipmentSlotUIList = new List<EquipmentItemSlot>(Constant.EquipmentListMaxSizeCount);
            foreach (var slot in slotScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentSlotUIList.Add(slot);
            }
        }

        // 이 창이 활성화되면 인벤토리를 표시한다.
        private void OnEnable()
        {
            ShowEquipmentInventory();
        }

        // 장비 인벤토리를 표시한다.
        private void ShowEquipmentInventory()
        {
            var equipmentItemInventory = GameManager.CurrentUser.GetInventoryEquipmentItem.OrderBy(item => item.equipmentType).ToList();

            for (int i = 0; i < equipmentSlotUIList.Count; i++)
            {
                // 장비 인벤토리의 장비 갯수만큼 장비 슬롯을 활성화한다.
                if (equipmentItemInventory.Count <= i)
                {
                    equipmentSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // 장비를 장비슬롯에 표시한다.
                equipmentSlotUIList[i].ShowEquipment(equipmentItemInventory[i]);
                equipmentSlotUIList[i].gameObject.SetActive(true);
            }

            // 장비 텍스트를 표시한다.
            ShowEquipmentInventorySizeText();
        }

        // 창이 꺼질 때 초기화 해준다.
        private void OnDisable()
        {
            // 다중 선택 토글의 기본값은 다중 선택 안함 상태
            multipleSelectionToggle.isOn = false;
            // 선택한 것들 전부 취소
            ClearSelect();
        }

        // ORDER : 다중 선택 토글에 의한 장비아이템 일반 선택 혹은 다중 선택 시스템 구현
        public void EquipmentSlotSelect(EquipmentSlotSelector_EquipmentInventory equipmentSlotUIInventorySelector)
        {
            if (multipleSelectionToggle.isOn)
                // 다중 선택이 활성화 상태라면
            {
                if (currentSelectorList.Contains(equipmentSlotUIInventorySelector))
                    // 이미 선택한 슬롯이라면
                {
                    // 선택을 취소하고 리스트에서 제외 시켜준다.
                    equipmentSlotUIInventorySelector.IsSelect = false;
                    currentSelectorList.Remove(equipmentSlotUIInventorySelector);
                }
                else
                    // 선택한 슬롯이 아니라면
                {
                    // 선택해주고 리스트에 추가한다.
                    equipmentSlotUIInventorySelector.IsSelect = true;
                    currentSelectorList.Add(equipmentSlotUIInventorySelector);
                }
            }
            else
                // 다중 선택이 비활성화 상태라면
            {
                // 선택 리스트에서 첫번째 요소를 가져온다.
                var currentSelect = currentSelectorList.SingleOrDefault();
                if (currentSelect != null)
                    // 이미 선택한 장비 슬롯이 있다면
                {
                    // 해당 슬롯 선택을 취소하고 리스트에서 제외시켜준다.
                    currentSelect.IsSelect = false;
                    currentSelectorList.Remove(currentSelect);
                    if (currentSelect != equipmentSlotUIInventorySelector)
                        // 기 선택한 슬롯이 이번에 선택한 슬롯과 같지 않다면
                    {
                        equipmentSlotUIInventorySelector.IsSelect = true;
                        currentSelectorList.Add(equipmentSlotUIInventorySelector);
                    }
                }
                else
                    // 기 선택한 장비 슬롯이 없다면 그냥 선택
                {
                    equipmentSlotUIInventorySelector.IsSelect = true;
                    currentSelectorList.Add(equipmentSlotUIInventorySelector);
                }
            }

            // 선택한 아이템 갯수가 1개 이상이라면 장비 버리기 버튼 활성화
            equipmentItemDumpBtn.interactable = currentSelectorList.Count > 0;
        }

        // 스크롤 뷰의 중앙 좌표를 반환한다.
        public Vector2 GetScrollViewMiddlePoint()
        {
            return (slotScrollView.transform as RectTransform).position;
        }



        // 다중 선택 토글이 변경될 경우 모든 슬롯 선택한 것을 취소한다.
        public void TOGGLE_OnValueChanged_ClearSelect()
        {
            ClearSelect();
        }

        // 선택한 장비 아이템을 버리는 버튼
        public void BTN_OnClick_DumpEquipment()
        {
            // 버리기 전 확인 다이얼로그 창 표시
            GameManager.UIManager.ShowConfirmation("장비 아이템 버리기", $"장비 아이템을 버리시겠습니까?\n버린 아이템은 돌아오지 않습니다.", DumpEquipment);
        }



        // 장비 인벤토리 최대 사이즈를 추가하는 버튼
        public void BTN_OnClick_EquipmentListSizeUp()
        {
            if (GameManager.CurrentUser.MaxEquipmentListCount >= Constant.EquipmentListMaxSizeCount)
                // 더이상 추가가 불가능 하면 경고창 표시
            {
                GameManager.UIManager.ShowAlert("이미 최대 사이즈에 도달했습니다!");
            }
            else
                // 추가가 가능하면 확인 다이얼로그 창을 표시 
            {
                int consumeDia = Constant.EquipmentListSizeUpDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("최대 장비 개수 증가", $"최대 장비 개수를 증가 시키겠습니까?\n{consumeDia} 다이아가 소비되며\n{Constant.EquipmentListSizeUpCount}칸이 늘어납니다.\n(최대 100칸)", EquipmentInventorySizeUp);
            }
        }

        // 모든 선택 내용을 취소한다.
        private void ClearSelect()
        {
            foreach (var selector in currentSelectorList)
            {
                // 선택 취소해준다.
                selector.IsSelect = false;
            }
            currentSelectorList.Clear();

            // 장비 버리기 버튼 비활성화
            equipmentItemDumpBtn.interactable = false;
        }

        // 장비 인벤토리 사이즈를 추가한다.
        private void EquipmentInventorySizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.EquipmentListSizeUpCount))
                // 소지 다이아량과 소비 다이아량을 비교한다.
            {
                GameManager.CurrentUser.Diamond -= Constant.EquipmentListSizeUpCount;
                GameManager.CurrentUser.MaxEquipmentListCount += Constant.EquipmentListSizeUpCount;
                ShowEquipmentInventorySizeText();
            }
            else
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
        }

        // 장비 인벤토리의 사이즈 텍스트를 표시한다.
        private void ShowEquipmentInventorySizeText()
        {
            var equipmentItemInventory = GameManager.CurrentUser.GetInventoryEquipmentItem;
            // 영웅 합성 등으로 장비아이템이 장비 인벤토리 사이즈를 초과하는 경우가 생기기 때문에 텍스트 색을 변경해서 알기 쉽게 보여준다.
            if (equipmentItemInventory.Count >= GameManager.CurrentUser.MaxEquipmentListCount)
            {
                equipmentListSizeText.color = Color.red;
            }
            else
            {
                equipmentListSizeText.color = Color.white;
            }
            equipmentListSizeText.text = $"{equipmentItemInventory.Count} / {GameManager.CurrentUser.MaxEquipmentListCount}";
        }

        // 장비 아이템을 버린다.
        private void DumpEquipment()
        {
            // SOUND : 장비 버리는 사운드 재생
            // 선택한 아이템을 인벤토리에서 제거해준다.
            foreach (var itemData in currentSelectorList.Select(selector => selector.unitEquipmentSlotUI.EquipmentData))
            {
                if (!GameManager.CurrentUser.TryRemoveEquipmentItem(itemData))
                {
                    Debug.LogError("버릴려는 아이템이 인벤토리에 존재하지 않습니다.");
                    return;
                }
            }

            // 모든 선택한 항목을 지워준다.
            ClearSelect();
            // 장비 인벤토리를 다시 표시해준다.
            ShowEquipmentInventory();
        }
    }

}