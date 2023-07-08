using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * 맵 정보를 표시해주는 UI 클래스
 */

namespace Portfolio.WorldMap
{
    public class MapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;           // 맵 이름 텍스트
        [SerializeField] ScrollRect unitSlotScrollView;         // 유닛 슬롯 스크롤 뷰
        [SerializeField] TextMeshProUGUI consumEnergyText;      // 요구 에너지양 텍스트

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>(); // 이 맵에서 출현하는 몬스터 리스트
        Map choiceMap;  // 선택한 맵

        private void Awake()
        {
            // 초기화 합니다.
            foreach (var unitSlot in unitSlotScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(unitSlot);
            }
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // 맵 정보를 보여줍니다.
        public void ShowMapInfo(Map map)
        {
            choiceMap = map;
            mapNameText.text = map.MapName;
            // 맵 정보에서 몬스터 리스트를 가져옵니다.
            var monsterUnitList = map.GetMapUnitList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (monsterUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].ShowUnit(monsterUnitList[i], false, false);
                unitSlotList[i].gameObject.SetActive(true);
            }

            consumEnergyText.text = $"X {map.ConsumEnergy}";
        }

        //포메이션 팝업창을 보여줍니다.
        public void BTN_OnClicK_ReadyBattle(FormationPopupUI fomationPopupUI)
        {
            if (GameManager.CurrentUser.IsLeftEnergy(choiceMap.ConsumEnergy))
            // 에너지에 여분이 있다면 팝업 표시
            {
                fomationPopupUI.ShowPopup(choiceMap);
            }
            else
            // 에너지에 여분이 없다면 경고 표시
            {
                GameManager.UIManager.ShowAlert("에너지가 부족합니다!");
            }
        }
    }
}