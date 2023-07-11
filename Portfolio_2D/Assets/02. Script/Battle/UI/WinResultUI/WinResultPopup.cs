using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
 * 전투 승리 팝업창 UI 클래스
 */

namespace Portfolio.Battle
{
    public class WinResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;                           // 맵 이름 표시 텍스트
        [SerializeField] RectTransform unitListLayout;                          // 승리 시 보여줄 유닛 레이아웃
        [SerializeField] ScrollRect itemScrollView;                             // 얻은 아이템 표시 스크롤뷰

        List<WinResultUnitSlot> unitSlotList = new List<WinResultUnitSlot>();   // 전투 참가 유저 유닛 리스트
        List<EquipmentItemSlot> equipmentItemSlotList = new List<EquipmentItemSlot>();  // 획득 장비아이템 슬롯 리스트
        List<ItemSlotUI> getItemSlotList = new List<ItemSlotUI>();              // 획득 소비아이템 슬롯 리스트

        [SerializeField] Button ReplayMapBtn;                                   // 맵 재시작 버튼
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;       // 현재 맵 에너지 소비량 텍스트
        [SerializeField] Button GotoNextMapBtn;                                 // 다음 맵 이동 버튼
        [SerializeField] TextMeshProUGUI nextMapConsumEnergyValueText;          // 다음 맵 에너지 소비량 텍스트
        [SerializeField] TextMeshProUGUI goldText;                              // 얻은 골드량 텍스트

        // 초기 세팅
        private void Awake()
        {
            foreach (var unitSlot in unitListLayout.GetComponentsInChildren<WinResultUnitSlot>())
            {
                unitSlotList.Add(unitSlot);
            }

            foreach (var equipmentItemSlot in itemScrollView.content.GetComponentsInChildren<EquipmentItemSlot>())
            {
                equipmentItemSlotList.Add(equipmentItemSlot);
            }

            foreach (var itemSlot in itemScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                getItemSlotList.Add(itemSlot);
            }
        }

        // 승리 팝업창 보여주기
        public void Show()
        {
            // 필요한 정보 참조
            var currentMap = BattleManager.Instance.CurrentMap;
            var userUnitList = BattleManager.Instance.userChoiceUnits;
            var getExperience = BattleManager.Instance.CurrentMap.MapExperience;

            // 맵이름 보여주기
            mapNameText.text = currentMap.MapName;
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                // 전투 참가한 유저 유닛들만 보여주기
                if (userUnitList.Count <= i || userUnitList[i] == null)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // 유닛 슬롯 보여주기
                unitSlotList[i].InitSlot(userUnitList[i], getExperience);
                unitSlotList[i].gameObject.SetActive(true);
            }

            // 얻은 장비 아이템참조
            var getEquipmentItemList = BattleManager.Instance.getEquipmentItemList;
            for (int i = 0; i < equipmentItemSlotList.Count; i++)
            {
                if (getEquipmentItemList.Count <= i)
                {
                    equipmentItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                equipmentItemSlotList[i].ShowEquipment(getEquipmentItemList[i]);
                equipmentItemSlotList[i].gameObject.SetActive(true);
            }

            // 얻은 소비아이템 참조
            var getItemList = BattleManager.Instance.getConsumableItemDic.ToList();
            for (int i = 0; i < getItemSlotList.Count; i++)
            {
                // 얻은 아이템들만 보여주기
                if (getItemList.Count <= i)
                {
                    getItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // 아이템 슬롯 보여주기
                getItemSlotList[i].ShowItem(getItemList[i].Key, getItemList[i].Value);
                getItemSlotList[i].gameObject.SetActive(true);
            }

            // 현재 맵의 다음맵이 있는지 확인
            bool isNextMapValid = currentMap.IsNextMapVaild;
            if (isNextMapValid)
                // 있다면
            {
                // 맵정보를 얻어온다.
                GameManager.Instance.TryGetMap(currentMap.MapID + 1, out Map nextMap);
                if (GameManager.CurrentUser.IsLeftEnergy(nextMap.ConsumEnergy))
                    // 다음맵을 도전할 에너지가 충분하면
                {
                    // 다음맵 가기 버튼 상호작용 활성화
                    GotoNextMapBtn.interactable = true;
                }
                else
                    // 불충분하면
                {
                    // 다음맵 가기 버튼 상호작용 비활성화
                    GotoNextMapBtn.interactable = false;
                }
                // 다음맵 소비 에너지량 표기
                nextMapConsumEnergyValueText.text = nextMap.ConsumEnergy.ToString();
            }
            else
                // 다음맵이 없다면
            {
                // 다음맵 가기 버튼 상호작용 비활성화
                GotoNextMapBtn.interactable = false;
                nextMapConsumEnergyValueText.text = "-";
            }

            // 현재 맵 재도전할 에너지량이 충분하면 재도전 버튼 상호작용 활성화
            ReplayMapBtn.interactable = GameManager.CurrentUser.IsLeftEnergy(currentMap.ConsumEnergy);
            // 현재 맵 에너지 소비량 표기
            currentMapConsumEnergyValueText.text = currentMap.ConsumEnergy.ToString();
            // 얻을 골드량 표기
            goldText.text = currentMap.GetGoldValue.ToString();
        }

        // 현재 맵 재도전 버튼
        public void BTN_OnClick_RePlayMapBtn()
        {
            // 지금 유닛 그대로 전투 씬을 다시 불러온다.
            SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, BattleManager.Instance.CurrentMap);
        }

        // 다음맵 도전
        public void BTN_OnClick_PlayNextMap()
        {
            if (GameManager.Instance.TryGetMap(BattleManager.Instance.CurrentMap.MapID + 1, out Map nextMap))
                // 다음맵 정보를 불러와서 전투씬을 다시 불러온다.
            {
                SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, nextMap);
            }
        }

        // 로비로 돌아가기 버튼
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}