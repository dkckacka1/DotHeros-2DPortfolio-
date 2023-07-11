using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/*
 * 장비 강화 팝업 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class EquipmentReinforcePopupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI reinforceSuccessPercentText;   // 강화 성공 확률 텍스트
        [SerializeField] TextMeshProUGUI reinforceConsumeGoldText;      // 강화시 소모되는 골드 텍스트

        [SerializeField] Button reinforceBtn;   // 장비 강화 버튼

        private void OnEnable()
        {
            // 장비 데이터가 변경되면 UI를 업데이트합니다.
            LobbyManager.UIManager.equipmentItemDataChangeEvent += ShowReinforce;
            ShowReinforce(this, EventArgs.Empty);
        }

        private void OnDisable()
        {
            // 창이 꺼지면 구독 해제
            LobbyManager.UIManager.equipmentItemDataChangeEvent -= ShowReinforce;
        }

        // 장비 강화창을 표시한다.
        public void ShowReinforce(object sender, EventArgs eventArgs)
        {
            // 강화할 데이터
            var equipmentItemData = HeroPanelUI.SelectEquipmentItem;
            if (equipmentItemData == null) return;

            if (!IsMaxReinforceCount(equipmentItemData))
                // 최대 강화 수치가 아니라면
            {
                // 강화 수치에 따른 확률과 골드 소비량을 표시해준다.
                reinforceBtn.interactable = GameManager.CurrentUser.Gold >= Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount];
                reinforceSuccessPercentText.text = $"현재 ( +{equipmentItemData.reinforceCount} )\n" +
                    $"강화 성공 확률 ({Constant.reinforceProbabilitys[equipmentItemData.reinforceCount] * 100}%)";
                reinforceConsumeGoldText.text = Constant.reinforceConsumeGoldValues[equipmentItemData.reinforceCount].ToString("N0");
            }
            else
                // 강화 수치가 최대라면
            {
                // 강화 버튼 상호작용을 비활성화 해준다.
                reinforceBtn.interactable = false;
                reinforceSuccessPercentText.text = $"현재 최대 강화 상태입니다.";
                reinforceConsumeGoldText.text = "[-]";
            }
        }

        // 최대 강화 수치인지 확인한다.
        private bool IsMaxReinforceCount(EquipmentItemData equipmentItemData)
        {
            return equipmentItemData.reinforceCount == Constant.MAX_REINFORCE_COUNT;
        }
    }
}