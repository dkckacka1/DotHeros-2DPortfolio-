using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*
 * ��� ��ȭ �˾� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class EquipmentReinforcePopupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI reinforceSuccessPercentText;   // ��ȭ ���� Ȯ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI reinforceConsumeGoldText;      // ��ȭ�� �Ҹ�Ǵ� ��� �ؽ�Ʈ

        [SerializeField] Button reinforceBtn;   // ��� ��ȭ ��ư

        private void OnEnable()
        {
            // ��� �����Ͱ� ����Ǹ� UI�� ������Ʈ�մϴ�.
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowReinforce;
            ShowReinforce(this, EventArgs.Empty);
        }

        private void OnDisable()
        {
            // â�� ������ ���� ����
            LobbyManager.UIManager.equipmentItemDataChangeEvent -= ShowReinforce;
        }

        // ��� ��ȭâ�� ǥ���Ѵ�.
        public void ShowReinforce(object sender, EventArgs eventArgs)
        {
            // ��ȭ�� ������
            var equipmentItemData = HeroPanelUI.SelectEquipmentItem;
            if (equipmentItemData == null) return;

            if (!IsMaxReinforceCount(equipmentItemData))
                // �ִ� ��ȭ ��ġ�� �ƴ϶��
            {
                // ��ȭ ��ġ�� ���� Ȯ���� ��� �Һ��� ǥ�����ش�.
                reinforceBtn.interactable = GameManager.CurrentUser.Gold >= Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"���� ( +{equipmentItemData.reinforceCount} )\n" +
                    $"��ȭ ���� Ȯ�� ({Constant.reinforceProbabilitys[equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount].ToString("N0");
            }
            else
                // ��ȭ ��ġ�� �ִ���
            {
                // ��ȭ ��ư ��ȣ�ۿ��� ��Ȱ��ȭ ���ش�.
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"���� �ִ� ��ȭ �����Դϴ�.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }

        // �ִ� ��ȭ ��ġ���� Ȯ���Ѵ�.
        private bool IsMaxReinforceCount(EquipmentItemData equipmentItemData)
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}