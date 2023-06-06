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

        public void Reinforce()
        {
            GameManager.CurrentUser.userData.gold -= Constant.reinforceConsumeGoldValues[this.equipmentItemData.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.reinforceProbabilitys[this.equipmentItemData.reinforceCount])
            {
                GameManager.ItemCreator.ReinforceEquipment(equipmentItemData);
                heroPanelUI.ReShow();
            }

            LobbyManager.UIManager.ShowUserResource();
            GameManager.Instance.SaveUser();
            
        }

        private void ShowReinforce(EquipmentItemData equipmentItemData)
        {
            //Debug.Log(!IsMaxReinforceCount());
            if (!IsMaxReinforceCount())
            {
                reinforceBtn.interactable = GameManager.CurrentUser.userData.gold >= Constant.reinforceConsumeGoldValues[this.equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"���� ( +{this.equipmentItemData.reinforceCount} )\n" +
                    $"��ȭ ���� Ȯ�� ({Constant.reinforceProbabilitys[this.equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[this.equipmentItemData.reinforceCount].ToString("N0");
            }
            else
            {
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"���� �ִ� ��ȭ �����Դϴ�.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }

        private bool IsMaxReinforceCount()
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}