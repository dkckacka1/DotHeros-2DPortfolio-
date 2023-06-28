using System.Security.Cryptography;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using UnityEditor;

namespace Portfolio.Start
{
    public class SignUpPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;
        [SerializeField] TMP_InputField passwordInputField;
        [SerializeField] TextMeshProUGUI passwordErrorText;
        [SerializeField] TMP_InputField passwordInputConfirmField;
        [SerializeField] TextMeshProUGUI passwordConfirmErrorText;
        [SerializeField] TMP_InputField nickNameInputField;
        [SerializeField] Button signUpBtn;

        private bool isIDError;
        private bool isPasswordError;
        private bool isPasswordConfirmError;
        private bool isNickNameError;
        public void Show()
        {
            this.gameObject.SetActive(true);
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

        public void SignUp()
        {
            if (SaveManager.ContainUserData(idInputField.text))
            {
                GameManager.UIManager.ShowAlert("�ߺ��� ID�� �����մϴ�.");
            }
            else
            {
                GameManager.UIManager.ShowConfirmation("���� ����", "���ο� ������ �����Ͻðڽ��ϱ�?", CreateUser);
            }
        }

        private void CreateUser()
        {
            var newUserData = SaveManager.CreateNewUser(idInputField.text, passwordInputField.text, nickNameInputField.text);
            SaveManager.SaveUserData(newUserData);
            GameManager.UIManager.ShowAlert("���� ������ �Ϸ��߽��ϴ�.\n���� ���� �������� �α��� ���ּ���.");
            this.gameObject.SetActive(false);
            AssetDatabase.Refresh();
        }

        public void CheckID(string id)
        {
            isIDError = false;

            if(id.Length < 6)
            {
                isIDError = true;
            }
        }

        public void CheckPassworld(string password)
        {
            isPasswordError = false;

            if (password.Length < 8 || password.Length > 12)
            {
                passwordErrorText.text = "�н������ 8���� �̻�, 12���� ���Ϸ� �ۼ����ּ���.";
                isPasswordError = true;
            }

            if (!isPasswordError && 
                !(IsNumberCheck(password) && IsAlphabet(password)))
            {
                passwordErrorText.text = "�н������ ������ ȥ������ �ۼ����ּ���.";
                isPasswordError = true;
            }

            passwordErrorText.gameObject.SetActive(isPasswordError);
        }

        public void CheckPasswordConfirm(string passwordConfirm)
        {
            isPasswordConfirmError = false;

            if(passwordConfirm != passwordInputField.text)
            {

                passwordConfirmErrorText.text = "�н������ ��ġ���� �ʽ��ϴ�.";
                isPasswordConfirmError = true;
            }

            passwordConfirmErrorText.gameObject.SetActive(isPasswordConfirmError);
        }

        public void CheckNickName(string nickName)
        {
            isNickNameError = false;
            if (nickName.Length < 2)
            {
                isNickNameError = true;
            }
        }

        public void CheckSignVaild()
        {
            signUpBtn.interactable = !isIDError && !isPasswordError && !isPasswordConfirmError && !isNickNameError;
        }

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