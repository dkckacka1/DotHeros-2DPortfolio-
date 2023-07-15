using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 맵 정보를 표시할 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleMapInfoUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameTxt;        // 맵 이름 표시 텍스트
        [SerializeField] TextMeshProUGUI mapStageProceedTxt;// 현재 맵 진행도를 표시할 텍스트

        int currentStageCount = 1;                          // 현재 맵의 스테이지

        // 맵 정보를 표시해준다.
        public void SetMapInfo(Map map)
        {
            mapNameTxt.text = map.MapName;
            mapStageProceedTxt.text = $"({currentStageCount} / {map.StageList.Count})";
        }

        // 다음 스테이지에 넘어갈 시 UI 업데이트 해준다.
        public void NextStage(Map map)
        {
            mapStageProceedTxt.text = $"({currentStageCount++} / {map.StageList.Count})";
        }
    }
}