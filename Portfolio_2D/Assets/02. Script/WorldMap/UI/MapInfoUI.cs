using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

namespace Portfolio.WorldMap
{
    public class MapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;
        [SerializeField] ScrollRect unitSlotScrollView;
        [SerializeField] TextMeshProUGUI consumEnergyText;

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>();
        Map choiceMap;

        private void Awake()
        {
            foreach (var unitSlot in unitSlotScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(unitSlot);
            }
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowMapInfo(Map map)
        {
            choiceMap = map;
            mapNameText.text = map.MapName;
            var monsterUnitList = map.GetMapUnitList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (monsterUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].Init(monsterUnitList[i], false, false);
                unitSlotList[i].gameObject.SetActive(true);
            }

            consumEnergyText.text = $"X {map.ConsumEnergy}";
        }

        public void BTN_ONCLICK_ReadyBattle(FomationPopupUI fomationPopupUI)
        {
            if (GameManager.CurrentUser.IsLeftEnergy(choiceMap.ConsumEnergy))
            {
                fomationPopupUI.ShowPopup(choiceMap);
            }
            else
            {
                GameManager.UIManager.ShowAlert("에너지가 부족합니다!");
            }
        }
    }
}