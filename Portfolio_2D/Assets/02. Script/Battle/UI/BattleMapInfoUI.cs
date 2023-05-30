using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio
{
    public class BattleMapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameTxt;
        [SerializeField] TextMeshProUGUI mapStageProceedTxt;

        int stageCount = 1;
        int currentStageCount = 1;

        public void SetMapInfo(MapData data)
        {
            mapNameTxt.text = data.mapName;
            if (data.stage_2_ID != -1)
            {
                stageCount++;
            }
            if (data.stage_3_ID != -1)
            {
                stageCount++;
            }
            if (data.stage_4_ID != -1)
            {
                stageCount++;
            }
            if (data.stage_5_ID != -1)
            {
                stageCount++;
            }
            mapStageProceedTxt.text = $"({currentStageCount++} / {stageCount})";
        }

        public void NextStage()
        {
            mapStageProceedTxt.text = $"({currentStageCount++} / {stageCount})";
        }
    }
}