using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��ȯ ���â�� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonResultUI : MonoBehaviour
    {
        [SerializeField] List<UnitSlotUI> resultUnitSlotList; // ��ȯ ���â�� ���� ���� ����Ʈ

        // ��ȯ ���â�� ����ݴϴ�.
        public void ClearSummonResult()
        {
            // ��� ���� ������ ��Ȱ��ȭ �մϴ�.
            foreach (var unitSlot in resultUnitSlotList)
            {
                unitSlot.gameObject.SetActive(false);
            }
        }

        // ��ȯ ����� �����ݴϴ�.
        public void ShowSummonResult(List<Unit> summonList)
        {
            
            // ��ȯ Ƚ����ŭ ���� ������ Ȱ��ȭ �մϴ�.
            for (int i = 0; i < 10; i++)
            {
                if (summonList.Count <= i)
                {
                    resultUnitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // ��ȯ�� ������ ������ ǥ���մϴ�.
                resultUnitSlotList[i].ShowUnit(summonList[i], false, true);
                resultUnitSlotList[i].gameObject.SetActive(true);
            }
        }
    }
}
