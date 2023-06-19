using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleMapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameTxt;
        [SerializeField] TextMeshProUGUI mapStageProceedTxt;

        int currentStageCount = 1;

        public void SetMapInfo(Map map)
        {
            mapNameTxt.text = map.MapName;
            mapStageProceedTxt.text = $"({currentStageCount++} / {map.StageList.Count})";
        }

        public void NextStage(Map map)
        {
            mapStageProceedTxt.text = $"({currentStageCount++} / {map.StageList.Count})";
        }
    }
}