using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Portfolio.Battle
{
    public class WinResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;
        [SerializeField] ScrollRect unitScrollView;
        [SerializeField] ScrollRect itemScrollView;

        List<WinResultUnitSlot> unitSlotList = new List<WinResultUnitSlot>();
        List<ItemSlotUI> getItemSlotList = new List<ItemSlotUI>();

        [SerializeField] Button ReplayMapBtn;
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;
        [SerializeField] Button GotoNextMapBtn;
        [SerializeField] TextMeshProUGUI nextMapConsumEnergyValueText;

        private void Awake()
        {
            foreach (var unitSlot in unitScrollView.content.GetComponentsInChildren<WinResultUnitSlot>())
            {
                unitSlotList.Add(unitSlot);
            }

            foreach (var itemSlot in itemScrollView.content.GetComponentsInChildren<ItemSlotUI>())
            {
                getItemSlotList.Add(itemSlot);
            }
        }

        public void Show()
        {
            var currentMap = BattleManager.Instance.CurrentMap;
            var userUnitList = BattleManager.Instance.userChoiceUnits;
            var getExperience = BattleManager.Instance.CurrentMap.MapExperience;

            mapNameText.text = currentMap.MapName;
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                Debug.Log("유닛 설정");
                unitSlotList[i].InitSlot(userUnitList[i], getExperience);
                unitSlotList[i].gameObject.SetActive(true);
            }

            var getItemList = BattleManager.Instance.GetItemDic.ToList();
            for (int i = 0; i < getItemSlotList.Count; i++)
            {
                if (getItemList.Count <= i)
                {
                    getItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                Debug.Log("아이템 설정");
                getItemSlotList[i].ShowItem(getItemList[i].Key, getItemList[i].Value, false);
                getItemSlotList[i].gameObject.SetActive(true);
            }

            bool isCurrentMapExternMap = currentMap.IsExternMap;
            bool isNextMapValid = currentMap.IsNextMapVaild;
            // TODO 승리창 버튼 작업중
        }
    }
}