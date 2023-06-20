using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] UserInfoUI userInfoUI;

        [Header("AlertPoup")]
        [SerializeField] AlertPoupUI alertPopup;
        [SerializeField] ConfirmationPopupUI confirmationPopup;

        public UserInfoUI UserInfoUI => userInfoUI;

        public void ShowUserInfo()
        {
            userInfoUI.Show(GameManager.CurrentUser);
        }

        public void ShowRemainTime(int time)
        {
            userInfoUI.ShowRemainTime(time);
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

        public void ShowConfirmation(string title, string alertText, UnityAction confirmEvent)
        {
            confirmationPopup.Show(title, alertText, confirmEvent);
        }
    }
}