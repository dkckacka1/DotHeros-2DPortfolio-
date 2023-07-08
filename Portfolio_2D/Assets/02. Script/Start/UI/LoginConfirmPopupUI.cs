using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * �α��� Ȯ�� �˾� UI Ŭ����
 */

namespace Portfolio.Start
{
    public class LoginConfirmPopupUI : MonoBehaviour
    {
        [SerializeField] Image userImage;                   // ������ ��Ʈ����Ʈ �̹���
        [SerializeField] TextMeshProUGUI userNickNameText;  // ������ �г��� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI userLevelText;     // ������ ���� �ؽ�Ʈ

        UserData userData; // ���� ������

        // ���� �����͸� �����ݴϴ�.
        public void Show(UserData userData)
        {
            this.userData = userData;

            // �˾��� ǥ���մϴ�.
            this.gameObject.SetActive(true);
            // ���� ������ �����ݴϴ�
            userImage.sprite = Resources.Load<Sprite>("Sprite/CharacterPortrait/" + userData.userPortraitName);
            userNickNameText.text = userData.userNickName + " (" + userData.userID + ")";
            userLevelText.text = $"���� ({userData.userLevel})";
        }

        // Ȯ���� ���������ͷ� �κ���� �ε��մϴ�.
        public void BTN_OnClick_GotoLobby()
        {
            StartManager.Instance.GotoLobby(userData);
        }
    }
}