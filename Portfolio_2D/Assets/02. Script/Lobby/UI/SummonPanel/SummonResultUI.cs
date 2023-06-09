using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class SummonResultUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> resultUnitSlotList;

        public void ClearSummonResult()
        {
            foreach (var unitSlot in resultUnitSlotList)
            {
                unitSlot.gameObject.SetActive(false);
            }
        }

        public void ShowSummonResult(List<Unit> summonList)
        {
            for (int i = 0; i < 10; i++)
            {
                if (summonList.Count <= i)
                {
                    resultUnitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                resultUnitSlotList[i].Init(summonList[i]);
                resultUnitSlotList[i].gameObject.SetActive(true);
            }
        }
    } 
}
