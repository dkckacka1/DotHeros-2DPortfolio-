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
 * 회원가입을 수행하는 팝업 UI 클래스
 */

namespace Portfolio.Start
{
    public class SignUpPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;               // ID입력 필드
        [SerializeField] TMP_InputField passwordInputField;         // 패스워드 입력 필드
        [SerializeField] TextMeshProUGUI passwordErrorText;         // 패스워드 경고 텍스트
        [SerializeField] TMP_InputField passwordInputConfirmField;  // 패스워드 확인 입력 필드
        [SerializeField] TextMeshProUGUI passwordConfirmErrorText;  // 패스워드 확인 경고 텍스트
        [SerializeField] TMP_InputField nickNameInputField;         // 닉네임 입력 필드
        [SerializeField] Button signUpBtn;                          // 회원가입 버튼

        private bool isIDError;                 // ID 입력에 에러가 있는가
        private bool isPasswordError;           // 패스워드 입력에 에러가 있는가
        private bool isPasswordConfirmError;    // 패스워드 확인에 에러가 있는가
        private bool isNickNameError;           // 닉네임에 에러가 잇는가
        
        // 팝업창을 표시합니다.
        public void Show()
        {
            this.gameObject.SetActive(true);
            // 기 입력된 사항을 없애줍니다.
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

        // 회원가입을 시도합니다.
        public void BTN_OnClick_SignUp()
        {
            if (SLManager.ContainUserData(idInputField.text))
                // 입력한 ID로 유저데이터가 존재한다면
            {
                GameManager.UIManager.ShowAlert("중복된 ID가 존재합니다.");
            }
            else
            {
                // 확인 다이얼로그 팝업창을 표시합니다.
                GameManager.UIManager.ShowConfirmation("계정 생성", "새로운 계정을 생성하시겠습니까?", CreateUser);
            }
        }

        // 계정을 생성합니다.
        private void CreateUser()
        {
            // 입력한 ID와 패스워드, 닉네임으로 유저정보를 생성합니다.
            var newUserData = SLManager.CreateNewUser(idInputField.text, passwordInputField.text, nickNameInputField.text);
            // 유저 정보를 저장합니다.
            SLManager.SaveUserData(newUserData);
            GameManager.UIManager.ShowAlert("계정 생성을 완료했습니다.\n새로 만든 계정으로 로그인 해주세요.");
            // 팝업창을 꺼줍니다.
            this.gameObject.SetActive(false);
            // 에셋폴더를 새로 고침합니다.
            AssetDatabase.Refresh();
        }

        // 아이디 입력을 확인합니다.
        public void INFUTFIELD_OnValueChanged_CheckID(string id)
        {
            isIDError = false;

            if(id.Length < 6)
                // ID가 6글자 미만이라면 에러
            {
                isIDError = true;
            }
        }

        // 패스워드 입력을 확인합니다.
        public void INFUTFIELD_OnValueChanged_CheckPassworld(string password)
        {
            isPasswordError = false;

            if (password.Length < 8 || password.Length > 12)
                // 패스워드가 8글자 미만 12글자 초과면은 에러
            {
                passwordErrorText.text = "패스워드는 8글자 이상, 12글자 이하로 작성해주세요.";
                isPasswordError = true;
            }

            if (!isPasswordError && 
                !(IsNumberCheck(password) && IsAlphabet(password)))
                // 패스워드에 영문만 있거나 숫자만 있을 경우 에러
            {
                passwordErrorText.text = "패스워드는 영숫자 혼합으로 작성해주세요.";
                isPasswordError = true;
            }

            // 에러 여부에 따라 에러 텍스트를 표기합니다.
            passwordErrorText.gameObject.SetActive(isPasswordError);
        }

        // 패스워드 확인 필드를 확인합니다.
        public void INFUTFIELD_OnValueChanged_CheckPasswordConfirm(string passwordConfirm)
        {
            isPasswordConfirmError = false;

            if(passwordConfirm != passwordInputField.text)
                // 패스워드 필드와 패스워드 확인 필드가 동일하지 않으면 에러
            {

                passwordConfirmErrorText.text = "패스워드와 일치하지 않습니다.";
                isPasswordConfirmError = true;
            }

            // 에러 여부에 따라 에러 텍스트를 표기합니다.
            passwordConfirmErrorText.gameObject.SetActive(isPasswordConfirmError);
        }

        // 닉네임 필드를 확인합니다.
        public void INFUTFIELD_OnValueChanged_CheckNickName(string nickName)
        {
            isNickNameError = false;
            if (nickName.Length < 2)
                // 닉네임이 두글자 미만이라면 에러
            {
                isNickNameError = true;
            }
        }

        // 모든 필드를 체크하여 회원가입 버튼을 활성화 합니다.
        public void INFUTFIELD_OnValueChanged_CheckSignVaild()
        {
            signUpBtn.interactable = !isIDError && !isPasswordError && !isPasswordConfirmError && !isNickNameError;
        }

        // 문자열에 숫자가 있는지 확인합니다.
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

        // 문자열 내에 알파벳이 있는지 확인합니다.
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