using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���� UI�� �����ϴ� �Ŵ��� Ŭ����
 */

namespace Portfolio.Start
{
    public class StartUIManager : MonoBehaviour
    {
        [SerializeField] LoginPopupUI loginPopupUI;                 // �α��� �˾� UI
        [SerializeField] SignUpPopupUI signUpPopupUI;               // ȸ������ �˾� UI
        [SerializeField] LoginConfirmPopupUI loginConfirmPopupUI;   // �α��� Ȯ�� �˾� Ui

        // �α��� �˾�â�� ǥ���մϴ�.
        public void BTN_OnClick_ShowLoginPopup()
        {
            loginPopupUI.Show();
        }

        // ȸ������ �˾�â�� ǥ���մϴ�.
        public void BTN_OnClick_ShowSignUpPopup()
        {
            signUpPopupUI.Show();
        }

        // �α��� Ȯ�� �˾�â�� ǥ���մϴ�.
        public void ShowLoginConfirm(UserData userData)
        {
            loginConfirmPopupUI.Show(userData);
        }
    }
}