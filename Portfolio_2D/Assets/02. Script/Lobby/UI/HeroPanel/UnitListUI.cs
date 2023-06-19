using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;
using System.Linq;
using System;

namespace Portfolio.Lobby.Hero
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;
        [SerializeField] ScrollRect unitScrollView;
        [SerializeField] TextMeshProUGUI unitListCountText;

        public void Init()
        {
            unitSlotList = new List<UnitSlotUI>();
            foreach (var slot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotList.Add(slot);
            }
        }

        private void OnEnable()
        {
            ShowUnitList();
            ShowUnitListCountText();
        }

        public void ShowUnitList()
        {
            var userUnitSortingList = GameManager.CurrentUser.userUnitList.OrderByDescending(GameLib.SortMethod).ToList();
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (userUnitSortingList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].Init(userUnitSortingList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }

        }


        public void ShowUnitListCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.userUnitList.Count} / {GameManager.CurrentUser.MaxUnitListCount}";
        }
    }
}