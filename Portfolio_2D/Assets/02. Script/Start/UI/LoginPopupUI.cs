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

            GameManager.NetworkManager.Login(emailInputField.text, passwordInputField.text,
                () =>
                {
                    // TODO : �����ͺ��̽����� ���� ���� �������� �������
                    Debug.Log("�α��� ����");
                    var userDataJson = GameManager.NetworkManager.LoadUserData();
                    Debug.Log(userDataJson);
                },
                () => 
                {
                    Debug.Log("�α��� ����");
                });

            //if (SLManager.LoadUserData(eamilInputField.text, out loginUserData))
            //// �Է��� ID�� ����Ǿ��ִ� ���������� ã�ƺ��ϴ�.
            //{
            //    // ���� ������ ������ ��й�ȣ�� ���մϴ�.
            //    if (SLManager.CheckPassword(loginUserData, passwordInputField.text))
            //        // ��й�ȣ�� �´ٸ�
            //    {
            //        // �α��� Ȯ�� �˾� ǥ��
            //        StartManager.UIManager.ShowLoginConfirm(loginUserData);
            //    }
            //    else
            //    {
            //        // ���â ǥ��
            //        GameManager.UIManager.ShowAlert("�н����� ������ �����߽��ϴ�.");
            //    }
            //}
            //else
            //// ���������� ���ٸ�
            //{
            //    // ���â ǥ��
            //    GameManager.UIManager.ShowAlert("�ش� ID�� �������� �ʽ��ϴ�.");
            //}
        }
    }
}