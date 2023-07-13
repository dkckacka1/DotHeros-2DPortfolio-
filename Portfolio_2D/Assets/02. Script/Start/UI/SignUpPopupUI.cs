using System.Security.Cryptography;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using UnityEditor;

/*
 * ȸ�������� �����ϴ� �˾� UI Ŭ����
 */

namespace Portfolio.Start
{
    public class SignUpPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;               // ID�Է� �ʵ�
        [SerializeField] TMP_InputField passwordInputField;         // �н����� �Է� �ʵ�
        [SerializeField] TextMeshProUGUI passwordErrorText;         // �н����� ��� �ؽ�Ʈ
        [SerializeField] TMP_InputField passwordInputConfirmField;  // �н����� Ȯ�� �Է� �ʵ�
        [SerializeField] TextMeshProUGUI passwordConfirmErrorText;  // �н����� Ȯ�� ��� �ؽ�Ʈ
        [SerializeField] TMP_InputField nickNameInputField;         // �г��� �Է� �ʵ�
        [SerializeField] Button signUpBtn;                          // ȸ������ ��ư

        private bool isIDError;                 // ID �Է¿� ������ �ִ°�
        private bool isPasswordError;           // �н����� �Է¿� ������ �ִ°�
        private bool isPasswordConfirmError;    // �н����� Ȯ�ο� ������ �ִ°�
        private bool isNickNameError;           // �г��ӿ� ������ �մ°�
        
        // �˾�â�� ǥ���մϴ�.
        public void Show()
        {
            this.gameObject.SetActive(true);
            // �� �Էµ� ������ �����ݴϴ�.
            idInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            passwordInputConfirmField.text = string.Empty;
            nickNameInputField.text = string.Empty;
            isIDError = true;
            isPasswordError = true;
            isPasswordConfirmError = true;
            isNickNameError = true;
            signUpBtn.interactable = false;
        }

        // ȸ�������� �õ��մϴ�.
        public void BTN_OnClick_SignUp()
        {
            if (SLManager.ContainUserData(idInputField.text))
                // �Է��� ID�� ���������Ͱ� �����Ѵٸ�
            {
                GameManager.UIManager.ShowAlert("�ߺ��� ID�� �����մϴ�.");
            }
            else
            {
                // Ȯ�� ���̾�α� �˾�â�� ǥ���մϴ�.
                GameManager.UIManager.ShowConfirmation("���� ����", "���ο� ������ �����Ͻðڽ��ϱ�?", CreateUser);
            }
        }

        // ������ �����մϴ�.
        private void CreateUser()
        {
            // �Է��� ID�� �н�����, �г������� ���������� �����մϴ�.
            var newUserData = SLManager.CreateNewUser(idInputField.text, passwordInputField.text, nickNameInputField.text);
            // ���� ������ �����մϴ�.
            SLManager.SaveUserData(newUserData);
            GameManager.UIManager.ShowAlert("���� ������ �Ϸ��߽��ϴ�.\n���� ���� �������� �α��� ���ּ���.");
            // �˾�â�� ���ݴϴ�.
            this.gameObject.SetActive(false);
            // ���������� ���� ��ħ�մϴ�.
            AssetDatabase.Refresh();
        }

        // ���̵� �Է��� Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckID(string id)
        {
            isIDError = false;

            if(id.Length < 6)
                // ID�� 6���� �̸��̶�� ����
            {
                isIDError = true;
            }
        }

        // �н����� �Է��� Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckPassworld(string password)
        {
            isPasswordError = false;

            if (password.Length < 8 || password.Length > 12)
                // �н����尡 8���� �̸� 12���� �ʰ����� ����
            {
                passwordErrorText.text = "�н������ 8���� �̻�, 12���� ���Ϸ� �ۼ����ּ���.";
                isPasswordError = true;
            }

            if (!isPasswordError && 
                !(IsNumberCheck(password) && IsAlphabet(password)))
                // �н����忡 ������ �ְų� ���ڸ� ���� ��� ����
            {
                passwordErrorText.text = "�н������ ������ ȥ������ �ۼ����ּ���.";
                isPasswordError = true;
            }

            // ���� ���ο� ���� ���� �ؽ�Ʈ�� ǥ���մϴ�.
            passwordErrorText.gameObject.SetActive(isPasswordError);
        }

        // �н����� Ȯ�� �ʵ带 Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckPasswordConfirm(string passwordConfirm)
        {
            isPasswordConfirmError = false;

            if(passwordConfirm != passwordInputField.text)
                // �н����� �ʵ�� �н����� Ȯ�� �ʵ尡 �������� ������ ����
            {

                passwordConfirmErrorText.text = "�н������ ��ġ���� �ʽ��ϴ�.";
                isPasswordConfirmError = true;
            }

            // ���� ���ο� ���� ���� �ؽ�Ʈ�� ǥ���մϴ�.
            passwordConfirmErrorText.gameObject.SetActive(isPasswordConfirmError);
        }

        // �г��� �ʵ带 Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckNickName(string nickName)
        {
            isNickNameError = false;
            if (nickName.Length < 2)
                // �г����� �α��� �̸��̶�� ����
            {
                isNickNameError = true;
            }
        }

        // ��� �ʵ带 üũ�Ͽ� ȸ������ ��ư�� Ȱ��ȭ �մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckSignVaild()
        {
            signUpBtn.interactable = !isIDError && !isPasswordError && !isPasswordConfirmError && !isNickNameError;
        }

        // ���ڿ��� ���ڰ� �ִ��� Ȯ���մϴ�.
        public bool IsNumberCheck(string str)
        {
            foreach (var charvalue in str)
            {
                if(Char.IsDigit(charvalue))
                {
                    return true;
                }
            }

            return false;
        }

        // ���ڿ� ���� ���ĺ��� �ִ��� Ȯ���մϴ�.
        public bool IsAlphabet(string str)
        {
            foreach (var charvalue in str)
            {
                if (Char.IsLetter(charvalue))
                {
                    return true;
                }
            }

            return false;
        }




    }
}