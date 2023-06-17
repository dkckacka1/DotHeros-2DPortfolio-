using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Portfolio.Lobby.Hero
{
    public class EquipmentReinforcePopupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI reinforceSuccessPercentText;
        [SerializeField] TextMeshProUGUI reinforceConsumeGoldText;

        [SerializeField] Button reinforceBtn;

        public void Init()
        {
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowReinforce;
        }
        public void ShowReinforce(object sender, EventArgs eventArgs)
        {
            var equipmentItemData = HeroPanelUI.SelectEquipmentItem;
            if (equipmentItemData == null) return;

            //Debug.Log(!IsMaxReinforceCount());
            if (!IsMaxReinforceCount(equipmentItemData))
            {
                reinforceBtn.interactable = GameManager.CurrentUser.userData.gold >= Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"현재 ( +{equipmentItemData.reinforceCount} )\n" +
                    $"강화 성공 확률 ({Constant.reinforceProbabilitys[equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount].ToString("N0");
            }
            else
            {
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"현재 최대 강화 상태입니다.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }

        public void ShowReinforce(object sender, EquipmentItemData equipmentItemData)
        {
            if (equipmentItemData == null) return;

            //Debug.Log(!IsMaxReinforceCount());
            if (!IsMaxReinforceCount(equipmentItemData))
            {
                reinforceBtn.interactable = GameManager.CurrentUser.userData.gold >= Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"현재 ( +{equipmentItemData.reinforceCount} )\n" +
                    $"강화 성공 확률 ({Constant.reinforceProbabilitys[equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount].ToString("N0");
            }
            else
            {
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"현재 최대 강화 상태입니다.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }



        private bool IsMaxReinforceCount(EquipmentItemData equipmentItemData)
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}