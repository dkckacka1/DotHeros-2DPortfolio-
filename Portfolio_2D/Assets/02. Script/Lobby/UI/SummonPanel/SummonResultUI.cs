using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 소환 결과창을 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonResultUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> resultUnitSlotList; // 소환 결과창의 유닛 슬롯 리스트

        // 소환 결과창을 비워줍니다.
        public void ClearSummonResult()
        {
            // 모든 유닛 슬롯을 비활성화 합니다.
            foreach (var unitSlot in resultUnitSlotList)
            {
                unitSlot.gameObject.SetActive(false);
            }
        }

        // 소환 결과를 보여줍니다.
        public void ShowSummonResult(List<Unit> summonList)
        {
            
            // 소환 횟수만큼 유닛 슬롯을 활성화 합니다.
            for (int i = 0; i < 10; i++)
            {
                if (summonList.Count <= i)
                {
                    resultUnitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // 소환한 유닛의 정보를 표시합니다.
                resultUnitSlotList[i].ShowUnit(summonList[i], false, true);
                resultUnitSlotList[i].gameObject.SetActive(true);
            }
        }
    }
}
