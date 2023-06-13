using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;

namespace Portfolio.WorldMap
{
    public class MapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;
        [SerializeField] ScrollRect unitSlotScrollView;
        [SerializeField] TextMeshProUGUI consumEnergyText;

        List<UnitSlotUI> unitSlotList = new List<UnitSlotUI>();

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
            mapNameText.text = map.MapData.mapName;
            // TODO : 맵 인포의 몬스터 종류 불러오기
            consumEnergyText.text = $"X {map.MapData.consumEnergy}";
        }

        public void LoadBattleScene()
        {
            // TODO : 전투 시작 만들기(포메이션 UI 만들기)
        }
    }
}