using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 로그인 확인 팝업 UI 클래스
 */

namespace Portfolio.Start
{
    public class LoginConfirmPopupUI : MonoBehaviour
    {
        [SerializeField] Image userImage;                   // 유저의 포트레이트 이미지
        [SerializeField] TextMeshProUGUI userNickNameText;  // 유저의 닉네임 텍스트
        [SerializeField] TextMeshProUGUI userLevelText;     // 유저의 레벨 텍스트

        UserData userData; // 유저 데이터

        // 유저 데이터를 보여줍니다.
        public void Show(UserData userData)
        {
            this.userData = userData;

            // 팝업을 표시합니다.
            this.gameObject.SetActive(true);
            // 유저 정보를 보여줍니다
            userImage.sprite = Resources.Load<Sprite>("Sprite/CharacterPortrait/" + userData.userPortraitName);
            userNickNameText.text = userData.userNickName + " (" + userData.userID + ")";
            userLevelText.text = $"레벨 ({userData.userLevel})";
        }

        // 확인한 유저데이터로 로비씬을 로드합니다.
        public void BTN_OnClick_GotoLobby()
        {
            StartManager.Instance.GotoLobby(userData);
        }
    }
}