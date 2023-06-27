using System.Security.Cryptography;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;


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
            passwordInputField.text = string.Empty;
            passwordInputConfirmField.text = string.Empty;
            signUpBtn.interactable = false;
        }

        public void SignUp()
        {
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

        public void SignCheck()
        {
            signUpBtn.interactable = !(!isIDError && !isPasswordError && !isPasswordConfirmError && !isNickNameError);
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

        static string ComputeSHA256(string s)
        {
            string hash = String.Empty;

            // SHA256 �ؽ� ��ü �ʱ�ȭ
            using (SHA256 sha256 = SHA256.Create())
            {
                // �־��� ���ڿ��� �ؽø� ����մϴ�.
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // ����Ʈ �迭�� ���ڿ� �������� ��ȯ
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }
    }
}