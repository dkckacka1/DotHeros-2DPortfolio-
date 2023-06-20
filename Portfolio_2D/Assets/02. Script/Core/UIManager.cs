using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] UserInfoUI userInfoUI;

        [Header("AlertPoup")]
        [SerializeField] AlertPoupUI alertPopup;

        public UserInfoUI UserInfoUI => userInfoUI;

        public void ShowUserInfo()
        {
            userInfoUI.Show(GameManager.CurrentUser);
        }

        public void ShowUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(true);
        }

        public void HideUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(false);
        }

        public void ShowAlert(string text)
        {
            alertPopup.Show(text);

        }
    }
}