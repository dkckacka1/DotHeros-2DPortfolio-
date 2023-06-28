using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Start
{
    public class LoginPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;
        [SerializeField] TMP_InputField passwordInputField;

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Login()
        {
            UserData loginUserData = null;

            if (SaveManager.LoadUserData(idInputField.text, out loginUserData))
            {
                Debug.Log("ID�� ���� ã�� ����");
                if (SaveManager.CheckPassword(loginUserData, passwordInputField.text))
                {
                    Debug.Log("�н����� ���� ����");
                    StartManager.UIManager.ShowLoginConfirm(loginUserData);
                }
                else
                {
                    Debug.Log("�н����� ���� ����");
                    GameManager.UIManager.ShowAlert("�н����� ������ �����߽��ϴ�.");
                }

            }
            else
            {
                Debug.Log("ID�� ���� ã�� ����");
                GameManager.UIManager.ShowAlert("�ش� ID�� �������� �ʽ��ϴ�.");
            }
        }
    }
}