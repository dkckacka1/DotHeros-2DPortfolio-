using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class EquipmentReinforcePopupUI : MonoBehaviour
    {
        EquipmentItemData equipmentItemData;

        [SerializeField] HeroPanelUI heroPanelUI;

        [SerializeField] TextMeshProUGUI reinforceSuccessPercentText;
        [SerializeField] TextMeshProUGUI reinforceConsumeGoldText;

        [SerializeField] Button reinforceBtn;

        public void Init(EquipmentItemData equipmentItemData)
        {
            this.equipmentItemData = equipmentItemData;
            ShowReinforce(equipmentItemData);

        }

        public void ReShow()
        {
            if (this.equipmentItemData == null) return;

            ShowReinforce(this.equipmentItemData);
        }

        private void ShowReinforce(EquipmentItemData equipmentItemData)
        {
            Debug.Log(!IsMaxReinforceCount());
            if (!IsMaxReinforceCount())
            {
                reinforceBtn.interactable = true;
                reinforceSuccessPercentText.text = $"현재 ( +{this.equipmentItemData.reinforceCount} )\n" +
                    $"강화 성공 확률 ({Constant.reinforceProbabilitys[this.equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[this.equipmentItemData.reinforceCount].ToString();
            }
            else
            {
                reinforceBtn.interactable = false;
            }
        }

        private bool IsMaxReinforceCount()
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}