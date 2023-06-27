using System.Security.Cryptography;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using System.Text.RegularExpressions;

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

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void SignUp()
        {
            string id = idInputField.text;
            string password = passwordInputField.text;
            string nickname = nickNameInputField.text;

            Debug.ClearDeveloperConsole();
            Debug.Log($"{id} : {password} : {nickname}");
            string idHash = ComputeSHA256(id);
            string passwordHash = ComputeSHA256(password);
            string hashTag = ComputeSHA256(id + password);
            Debug.Log($"{idHash} : {passwordHash}");
            Debug.Log($"{hashTag}");
        }

        public void CheckPassworld(string password)
        {
            bool isError = false;

            if (password.Length < 8 || password.Length > 12)
            {
                passwordErrorText.text = "�н������ 8���� �̻�, 12���� ���Ϸ� �ۼ����ּ���.";
                isError = true;
            }

            if (!isError && Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
            {
                passwordErrorText.text = "�н������ ������ ȥ������ �ۼ����ּ���.";
                isError = true;
            }

            passwordErrorText.gameObject.SetActive(isError);
        }

        public bool isNumberCheck(string str)
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