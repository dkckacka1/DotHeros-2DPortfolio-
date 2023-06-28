using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Start
{
    public class LoginConfirmPopupUI : MonoBehaviour
    {
        [SerializeField] Image userImage;
        [SerializeField] TextMeshProUGUI userNickNameText;
        [SerializeField] TextMeshProUGUI userLevelText;

        UserData userData;

        public void Show(UserData userData)
        {
            this.userData = userData;
            this.gameObject.SetActive(true);
            userNickNameText.text = userData.userNickName + " (" + userData.userID + ")";
            userLevelText.text = $"·¹º§ ({userData.userLevel})";
        }

        public void GotoLobby()
        {
            StartManager.Instance.GotoLobby(userData);
        }
    }
}