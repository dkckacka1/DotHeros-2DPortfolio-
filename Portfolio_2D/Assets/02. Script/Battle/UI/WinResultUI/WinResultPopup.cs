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
            var userUnitList = BattleManager.Instance.userChoiceUnits;
            Debug.Log(BattleManager.Instance == null);
            Debug.Log(BattleManager.Instance.currentMap == null);
            Debug.Log(BattleManager.Instance.currentMap.MapData == null);
            var getExperience = BattleManager.Instance.currentMap.MapData.experienceValue;
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].InitSlot(userUnitList[i], getExperience);
                unitSlotList[i].gameObject.SetActive(true);
            }

            var getItemList = BattleManager.Instance.GetItemDic.ToList();
            for (int i = 0; i < getItemList.Count; i++)
            {
                if (getItemList.Count <= i)
                {
                    getItemSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                getItemSlotList[i].ShowItem(getItemList[i].Key, getItemList[i].Value, false);
                getItemSlotList[i].gameObject.SetActive(true);
            }

            transform.parent.gameObject.SetActive(true);
        }
    }
}