using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 로그인을 하는 팝업 UI 클래스
 */

namespace Portfolio.Start
{
    public class LoginPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;       // 아이디 입력 필드
        [SerializeField] TMP_InputField passwordInputField; // 패스워드 입력 필드

        // 팝업창을 보여줍니다.
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        // 로그인을 시도합니다.
        public void BTN_OnClick_TryLogin()
        {
            UserData loginUserData = null;

            if (SLManager.LoadUserData(idInputField.text, out loginUserData))
            // 입력한 ID로 저장되어있는 유저정보를 찾아봅니다.
            {
                // 유저 정보가 있으면 비밀번호를 비교합니다.
                if (SLManager.CheckPassword(loginUserData, passwordInputField.text))
                    // 비밀번호가 맞다면
                {
                    // 로그인 확인 팝업 표시
                    StartManager.UIManager.ShowLoginConfirm(loginUserData);
                }
                else
                {
                    // 경고창 표시
                    GameManager.UIManager.ShowAlert("패스워드 인증에 실패했습니다.");
                }
            }
            else
            // 유저정보가 없다면
            {
                // 경고창 표시
                GameManager.UIManager.ShowAlert("해당 ID가 존재하지 않습니다.");
            }
        }
    }
}