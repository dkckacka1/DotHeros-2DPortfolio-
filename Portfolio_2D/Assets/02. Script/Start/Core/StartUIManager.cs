using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 시작 씬의 UI를 관리하는 매니저 클래스
 */

namespace Portfolio.Start
{
    public class StartUIManager : MonoBehaviour
    {
        [SerializeField] LoginPopupUI loginPopupUI;                 // 로그인 팝업 UI
        [SerializeField] SignUpPopupUI signUpPopupUI;               // 회원가입 팝업 UI
        [SerializeField] LoginConfirmPopupUI loginConfirmPopupUI;   // 로그인 확인 팝업 Ui

        // 로그인 팝업창을 표시합니다.
        public void BTN_OnClick_ShowLoginPopup()
        {
            loginPopupUI.Show();
        }

        // 회원가입 팝업창을 표시합니다.
        public void BTN_OnClick_ShowSignUpPopup()
        {
            signUpPopupUI.Show();
        }

        // 로그인 확인 팝업창을 표시합니다.
        public void ShowLoginConfirm(UserData userData)
        {
            loginConfirmPopupUI.Show(userData);
        }
    }
}