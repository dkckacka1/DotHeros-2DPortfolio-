using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * 진형을 결정짓는 팝업 UI 클래스
 */

namespace Portfolio.WorldMap
{
    public class FormationPopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect unitScrollView; // 유닛 리스트 스크롤 뷰

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>(Constant.UnitListMaxSizeCount); // 유닛 슬롯 리스트

        [SerializeField] List<FormationGrid> fomationGrids;     // 진형 그리드

        [SerializeField] TextMeshProUGUI mapNameText;           // 맵 이름 텍스트
        [SerializeField] TextMeshProUGUI consumEnergyText;      // 사용 에너지 텍스트

        Map choiceMap; // 선택한 맵

        private void Awake()
        {
            // 초기화 합니다.
            foreach (var unitSlotUI in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(unitSlotUI);
            }
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // 맵 정보를 표시합니다.
        public void ShowPopup(Map map)
        {
            choiceMap = map;
            ShowUnitList();
            mapNameText.text = map.MapName;
            consumEnergyText.text = "X "  + map.ConsumEnergy.ToString();
            foreach (var grid in fomationGrids)
            {
                grid.ReShow();
            }
            this.gameObject.SetActive(true);
        }

        // 유닛 리스트를 보여줍니다.
        public void ShowUnitList()
        {
            var userUnitList = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(userUnitList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }
        }

        // 전투로 돌입합니다.
        public void BTN_OnClick_GotoBattle()
        {
            List<Unit> userChoiceList = new List<Unit>();

            // 전체 진형에서 유닛 정보를 가져옵니다.
            foreach (var grid in fomationGrids)
            {
                // 빈 칸도 있기에 유닛정보가 없다면 null 값으로 가져옵니다.
                userChoiceList.Add(grid.GetCurrentUnit);
            }

            // 현재 선택한 유닛이 있는지 체크합니다
            bool isUserChoice = false;
            for (int i = 0; i < userChoiceList.Count; i++)
            {
                if (userChoiceList[i] != null)
                {
                    isUserChoice = true;
                }
            }

            if (!isUserChoice)
                // 선택한 유닛이 없다면
            {
                // 경고를 표시합니다.
                GameManager.UIManager.ShowAlert("진형을 채워주세요!");
                return;
            }

            if (GameManager.CurrentUser.IsLeftEnergy(choiceMap.ConsumEnergy))
                // 여분 에너지가 남아 있다면
            {
                // 에너지를 깍고 선택한 유닛 리스트와 선택한 맵 정보를 가지고 전투에 돌입합니다.
                GameManager.CurrentUser.CurrentEnergy -= choiceMap.ConsumEnergy;
                SceneLoader.LoadBattleScene(userChoiceList, choiceMap);
            }
            else
            // 에너지가 없다면 경고 표시
            {
                GameManager.UIManager.ShowAlert("에너지가 부족합니다!");
            }
        }
    }
}