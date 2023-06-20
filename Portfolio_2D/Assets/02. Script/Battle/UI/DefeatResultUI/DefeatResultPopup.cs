using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Battle
{
    public class DefeatResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;
        [SerializeField] Button rePlayMapBtn;
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;

        public void Show()
        {
            Map currentMap = BattleManager.Instance.CurrentMap;
            mapNameText.text = currentMap.MapName;
            rePlayMapBtn.interactable = GameManager.CurrentUser.IsLeftEnergy(currentMap.ConsumEnergy);
            currentMapConsumEnergyValueText.text = currentMap.ConsumEnergy.ToString();
        }

        public void RePlayMapBtn()
        {
            SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, BattleManager.Instance.CurrentMap);
        }

        public void ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}