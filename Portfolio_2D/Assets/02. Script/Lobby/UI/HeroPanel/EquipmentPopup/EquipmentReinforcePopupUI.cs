using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class EquipmentReinforcePopupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI reinforceSuccessPercentText;
        [SerializeField] TextMeshProUGUI reinforceConsumeGoldText;

        [SerializeField] Button reinforceBtn;

        public void ShowReinforce(EquipmentItemData equipmentItemData)
        {
            //Debug.Log(!IsMaxReinforceCount());
            if (!IsMaxReinforceCount(equipmentItemData))
            {
                reinforceBtn.interactable = GameManager.CurrentUser.userData.gold >= Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"���� ( +{equipmentItemData.reinforceCount} )\n" +
                    $"��ȭ ���� Ȯ�� ({Constant.reinforceProbabilitys[equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount].ToString("N0");
            }
            else
            {
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"���� �ִ� ��ȭ �����Դϴ�.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }

        private bool IsMaxReinforceCount(EquipmentItemData equipmentItemData)
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}