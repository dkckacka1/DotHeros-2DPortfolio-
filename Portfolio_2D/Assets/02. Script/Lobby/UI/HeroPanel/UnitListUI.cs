using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Portfolio.UI;

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
            for (int i = 0; i < unitSlotList.Count; i++)
            {
                if (GameManager.CurrentUser.userUnitList.Count <= i)
                {
                    unitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotList[i].Init(GameManager.CurrentUser.userUnitList[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }

            InitUnitLIstCountText();
        }

        public void InitUnitLIstCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.userUnitList.Count} / {GameManager.CurrentUser.userData.maxUnitListCount}";
        }
    }
}