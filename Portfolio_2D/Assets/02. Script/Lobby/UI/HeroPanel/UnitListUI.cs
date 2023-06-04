using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class UnitListUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> unitSlotList;

        private void OnEnable()
        {
            for (int i = 0; i < GameManager.CurrentUser.userUnitDic.Count; i++)
            {
                unitSlotList[i].Init(GameManager.CurrentUser.userUnitDic[i]);
                unitSlotList[i].gameObject.SetActive(true);
            }
        }
    }
}