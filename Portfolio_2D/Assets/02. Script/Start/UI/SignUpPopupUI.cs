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
                passwordErrorText.text = "패스워드는 8글자 이상, 12글자 이하로 작성해주세요.";
                isError = true;
            }

            if (!isError && Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
            {
                passwordErrorText.text = "패스워드는 영숫자 혼합으로 작성해주세요.";
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

            // SHA256 해시 객체 초기화
            using (SHA256 sha256 = SHA256.Create())
            {
                // 주어진 문자열의 해시를 계산합니다.
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));

                // 바이트 배열을 문자열 형식으로 변환
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }
    }
}