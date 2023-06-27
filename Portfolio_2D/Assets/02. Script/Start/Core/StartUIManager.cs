using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Start
{
    public class StartUIManager : MonoBehaviour
    {
        [SerializeField] LoginPopupUI loginPopupUI;
        [SerializeField] SignUpPopupUI signUpPopupUI;

        public void ShowLoginPopup()
        {
            loginPopupUI.Show();
        }

        public void ShowSignUpPopup()
        {
            signUpPopupUI.Show();
        }
    }
}