using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Start
{
    public class LoginPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField idInputField;
        [SerializeField] TMP_InputField passwordInputField;

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Login()
        {
            UserData loginUserData = null;

            if (SaveManager.LoadUserData(idInputField.text, out loginUserData))
            {
                Debug.Log("ID로 유저 찾기 성공");
                if (SaveManager.CheckPassword(loginUserData, passwordInputField.text))
                {
                    Debug.Log("패스워드 인증 성공");
                    StartManager.UIManager.ShowLoginConfirm(loginUserData);
                }
                else
                {
                    Debug.Log("패스워드 인증 실패");
                    GameManager.UIManager.ShowAlert("패스워드 인증에 실패했습니다.");
                }

            }
            else
            {
                Debug.Log("ID로 유저 찾기 실패");
                GameManager.UIManager.ShowAlert("해당 ID가 존재하지 않습니다.");
            }
        }
    }
}