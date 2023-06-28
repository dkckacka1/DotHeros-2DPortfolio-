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
                GameManager.UIManager.ShowAlert("중복된 ID가 존재합니다.");
            }
            else
            {
                GameManager.UIManager.ShowConfirmation("계정 생성", "새로운 계정을 생성하시겠습니까?", CreateUser);
            }
        }

        private void CreateUser()
        {
            var newUserData = SaveManager.CreateNewUser(idInputField.text, passwordInputField.text, nickNameInputField.text);
            SaveManager.SaveUserData(newUserData);
            GameManager.UIManager.ShowAlert("계정 생성을 완료했습니다.\n새로 만든 계정으로 로그인 해주세요.");
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
                passwordErrorText.text = "패스워드는 8글자 이상, 12글자 이하로 작성해주세요.";
                isPasswordError = true;
            }

            if (!isPasswordError && 
                !(IsNumberCheck(password) && IsAlphabet(password)))
            {
                passwordErrorText.text = "패스워드는 영숫자 혼합으로 작성해주세요.";
                isPasswordError = true;
            }

            passwordErrorText.gameObject.SetActive(isPasswordError);
        }

        public void CheckPasswordConfirm(string passwordConfirm)
        {
            isPasswordConfirmError = false;

            if(passwordConfirm != passwordInputField.text)
            {

                passwordConfirmErrorText.text = "패스워드와 일치하지 않습니다.";
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