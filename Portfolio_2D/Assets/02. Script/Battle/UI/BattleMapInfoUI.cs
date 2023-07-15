using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * �� ������ ǥ���� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleMapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameTxt;        // �� �̸� ǥ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI mapStageProceedTxt;// ���� �� ���൵�� ǥ���� �ؽ�Ʈ

        int currentStageCount = 1;                          // ���� ���� ��������

        // �� ������ ǥ�����ش�.
        public void SetMapInfo(Map map)
        {
            mapNameTxt.text = map.MapName;
            mapStageProceedTxt.text = $"({currentStageCount} / {map.StageList.Count})";
        }

        // ���� ���������� �Ѿ �� UI ������Ʈ ���ش�.
        public void NextStage(Map map)
        {
            mapStageProceedTxt.text = $"({currentStageCount++} / {map.StageList.Count})";
        }
    }
}