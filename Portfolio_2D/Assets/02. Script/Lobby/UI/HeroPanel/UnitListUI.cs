using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Portfolio.Lobby
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;
        [SerializeField] TextMeshProUGUI unitListCountText;

        private void OnEnable()
        {
            for (int i = 0; i < GameManager.CurrentUser.userUnitList.Count; i++)
            {
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