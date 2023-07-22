using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  전투 패배 팝업 UI 클래스
 */

namespace Portfolio.Battle
{
    public class DefeatResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;                       // 맵 이름 텍스트
        [SerializeField] Button rePlayMapBtn;                               // 리플레이 버튼
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;   // 현재 맵 에너지 소비량 텍스트

        // 패배 팝업창을 표시한다.
        public void Show()
        {
            // 현재 맵 참조
            Map currentMap = BattleManager.Instance.CurrentMap;
            // 맵이름 표시
            mapNameText.text = currentMap.MapName;
            // 유저의 남은 에너지량을 확인해서 부족하면 리플레이 버튼을 상호작용 불가능하게 변경
            rePlayMapBtn.interactable = GameManager.CurrentUser.IsLeftEnergy(currentMap.ConsumEnergy);
            // 현재 맵의 소비량 표기
            currentMapConsumEnergyValueText.text = currentMap.ConsumEnergy.ToString();
        }

        // 현재 맵 리플레이
        public void BTN_OnClick_RePlayMapBtn()
        {
            SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, BattleManager.Instance.CurrentMap);
        }

        // 로비로 돌아가기
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}