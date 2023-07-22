using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� �й� �˾� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class DefeatResultPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mapNameText;                       // �� �̸� �ؽ�Ʈ
        [SerializeField] Button rePlayMapBtn;                               // ���÷��� ��ư
        [SerializeField] TextMeshProUGUI currentMapConsumEnergyValueText;   // ���� �� ������ �Һ� �ؽ�Ʈ

        // �й� �˾�â�� ǥ���Ѵ�.
        public void Show()
        {
            // ���� �� ����
            Map currentMap = BattleManager.Instance.CurrentMap;
            // ���̸� ǥ��
            mapNameText.text = currentMap.MapName;
            // ������ ���� ���������� Ȯ���ؼ� �����ϸ� ���÷��� ��ư�� ��ȣ�ۿ� �Ұ����ϰ� ����
            rePlayMapBtn.interactable = GameManager.CurrentUser.IsLeftEnergy(currentMap.ConsumEnergy);
            // ���� ���� �Һ� ǥ��
            currentMapConsumEnergyValueText.text = currentMap.ConsumEnergy.ToString();
        }

        // ���� �� ���÷���
        public void BTN_OnClick_RePlayMapBtn()
        {
            SceneLoader.LoadBattleScene(BattleManager.Instance.userChoiceUnits, BattleManager.Instance.CurrentMap);
        }

        // �κ�� ���ư���
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}