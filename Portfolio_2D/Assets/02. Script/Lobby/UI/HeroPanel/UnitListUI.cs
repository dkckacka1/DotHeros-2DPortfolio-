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
            for (int i = 0; i < GameManager.CurrentUser.userUnitDic.Count; i++)
            {
                unitSlotList[i].Init(GameManager.CurrentUser.userUnitDic[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }

            InitUnitLIstCountText();
        }

        public void InitUnitLIstCountText()
        {
            unitListCountText.text = $"{GameManager.CurrentUser.userUnitDic.Count} / {GameManager.CurrentUser.userData.maxUnitListCount}";
        }
    }
}