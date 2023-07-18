using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * �α����� �ϴ� �˾� UI Ŭ����
 */

namespace Portfolio.Start
{
    public class LoginPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField emailInputField;       // �̸��� �Է� �ʵ�
        [SerializeField] TMP_InputField passwordInputField; // �н����� �Է� �ʵ�

        // �˾�â�� �����ݴϴ�.
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        // �α����� �õ��մϴ�.
        public void BTN_OnClick_TryLogin()
        {
            UserData loginUserData = null;

            // ��Ʈ��ũ �Ŵ����� �α��� ���μ����� �����մϴ�.
            GameManager.NetworkManager.LoginProcess(emailInputField.text, passwordInputField.text,
                (json) =>
                {
                    // ���� �̺�Ʈ�Դϴ�.
                    // ������ ������ json���Ϸ� ���������ͷ� �����ɴϴ�.
                    loginUserData = SLManager.LoadUserData(json);
                    StartManager.UIManager.ShowLoginConfirm(loginUserData);
                },
                (errorMessage) => 
                {
                    // ���н� ��� �˾�â�� ǥ���մϴ�.
                    GameManager.UIManager.ShowAlert(errorMessage);
                });
        }
    }
}