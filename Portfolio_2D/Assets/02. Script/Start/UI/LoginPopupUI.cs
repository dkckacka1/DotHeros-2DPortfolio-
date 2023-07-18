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
        [SerializeField] TMP_InputField emailInputField;       // 이메일 입력 필드
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

            // 네트워크 매니저의 로그인 프로세스를 수행합니다.
            GameManager.NetworkManager.LoginProcess(emailInputField.text, passwordInputField.text,
                (json) =>
                {
                    // 성공 이벤트입니다.
                    // 성공시 가져온 json파일로 유저데이터로 가져옵니다.
                    loginUserData = SLManager.LoadUserData(json);
                    StartManager.UIManager.ShowLoginConfirm(loginUserData);
                },
                (errorMessage) => 
                {
                    // 실패시 경고 팝업창을 표시합니다.
                    GameManager.UIManager.ShowAlert(errorMessage);
                });
        }
    }
}