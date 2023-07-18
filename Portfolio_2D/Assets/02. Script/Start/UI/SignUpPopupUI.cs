using System.Security.Cryptography;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using UnityEditor;
using System.Globalization;
using System.Text.RegularExpressions;

/*
 * ȸ�������� �����ϴ� �˾� UI Ŭ����
 */

namespace Portfolio.Start
{
    public class SignUpPopupUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField emailInputField;           // ID�Է� �ʵ�
        [SerializeField] TMP_InputField passwordInputField;         // �н����� �Է� �ʵ�
        [SerializeField] TextMeshProUGUI passwordErrorText;         // �н����� ��� �ؽ�Ʈ
        [SerializeField] TMP_InputField passwordInputConfirmField;  // �н����� Ȯ�� �Է� �ʵ�
        [SerializeField] TextMeshProUGUI passwordConfirmErrorText;  // �н����� Ȯ�� ��� �ؽ�Ʈ
        [SerializeField] TMP_InputField nickNameInputField;         // �г��� �Է� �ʵ�
        [SerializeField] Button signUpBtn;                          // ȸ������ ��ư

        private bool isEmailError;                 // �̸��� �Է¿� ������ �ִ°�
        private bool isPasswordError;           // �н����� �Է¿� ������ �ִ°�
        private bool isPasswordConfirmError;    // �н����� Ȯ�ο� ������ �ִ°�
        private bool isNickNameError;           // �г��ӿ� ������ �մ°�

        private bool invalidEmailType = false;       // �̸��� ������ �ùٸ��� üũ
        private bool isValidFormat = false;          // �ùٸ� �������� �ƴ��� üũ

        // �˾�â�� ǥ���մϴ�.
        public void Show()
        {
            this.gameObject.SetActive(true);
            // �� �Էµ� ������ �����ݴϴ�.
            emailInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            passwordInputConfirmField.text = string.Empty;
            nickNameInputField.text = string.Empty;
            isEmailError = true;
            isPasswordError = true;
            isPasswordConfirmError = true;
            isNickNameError = true;
            signUpBtn.interactable = false;
        }

        // ȸ�������� �õ��մϴ�.
        public void BTN_OnClick_SignUp()
        {
            // ���� ���� ���̾�α׸� �����ݴϴ�.
            GameManager.UIManager.ShowConfirmation("���� ����", "���ο� ������ �����Ͻðڽ��ϱ�?", CreateUser);
        }

        // ������ �����մϴ�.
        private void CreateUser()
        {
            GameManager.NetworkManager.CreateUser(emailInputField.text, passwordInputField.text, nickNameInputField.text);

            //            // �Է��� ID�� �н�����, �г������� ���������� �����մϴ�.
            //            var newUserData = SLManager.CreateNewUser(emailInputField.text, passwordInputField.text, nickNameInputField.text);
            //            // ���� ������ �����մϴ�.
            //            SLManager.SaveUserData(newUserData);
            //            GameManager.UIManager.ShowAlert("���� ������ �Ϸ��߽��ϴ�.\n���� ���� �������� �α��� ���ּ���.");
            //            // �˾�â�� ���ݴϴ�.
            //            this.gameObject.SetActive(false);
            //#if UNITY_EDITOR
            //            // ���������� ���� ��ħ�մϴ�.
            //            AssetDatabase.Refresh();
            //#endif
        }

        // ���̵� �Է��� Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckEmail(string email)
        {
            isEmailError = !(CheckEmailAddress(email));
        }

        // �н����� �Է��� Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckPassworld(string password)
        {
            isPasswordError = false;

            if (password.Length < 8 || password.Length > 12)
            // �н����尡 8���� �̸� 12���� �ʰ����� ����
            {
                passwordErrorText.text = "�н������ 8���� �̻�, 12���� ���Ϸ� �ۼ����ּ���.";
                isPasswordError = true;
            }

            if (!isPasswordError &&
                !(IsNumberCheck(password) && IsAlphabet(password)))
            // �н����忡 ������ �ְų� ���ڸ� ���� ��� ����
            {
                passwordErrorText.text = "�н������ ������ ȥ������ �ۼ����ּ���.";
                isPasswordError = true;
            }

            // ���� ���ο� ���� ���� �ؽ�Ʈ�� ǥ���մϴ�.
            passwordErrorText.gameObject.SetActive(isPasswordError);
        }

        // �н����� Ȯ�� �ʵ带 Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckPasswordConfirm(string passwordConfirm)
        {
            isPasswordConfirmError = false;

            if (passwordConfirm != passwordInputField.text)
            // �н����� �ʵ�� �н����� Ȯ�� �ʵ尡 �������� ������ ����
            {

                passwordConfirmErrorText.text = "�н������ ��ġ���� �ʽ��ϴ�.";
                isPasswordConfirmError = true;
            }

            // ���� ���ο� ���� ���� �ؽ�Ʈ�� ǥ���մϴ�.
            passwordConfirmErrorText.gameObject.SetActive(isPasswordConfirmError);
        }

        // �г��� �ʵ带 Ȯ���մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckNickName(string nickName)
        {
            isNickNameError = false;
            if (nickName.Length < 2)
            // �г����� �α��� �̸��̶�� ����
            {
                isNickNameError = true;
            }
        }

        // ��� �ʵ带 üũ�Ͽ� ȸ������ ��ư�� Ȱ��ȭ �մϴ�.
        public void INFUTFIELD_OnValueChanged_CheckSignVaild()
        {
            signUpBtn.interactable = !isEmailError && !isPasswordError && !isPasswordConfirmError && !isNickNameError;
        }

        // ���ڿ��� ���ڰ� �ִ��� Ȯ���մϴ�.
        public bool IsNumberCheck(string str)
        {
            foreach (var charvalue in str)
            {
                if (Char.IsDigit(charvalue))
                {
                    return true;
                }
            }

            return false;
        }

        // ���ڿ� ���� ���ĺ��� �ִ��� Ȯ���մϴ�.
        public bool IsAlphabet(string str)
        {
            foreach (var charvalue in str)
            {
                if (Char.IsLetter(charvalue))
                {
                    return true;
                }
            }

            return false;
        }

        // �ùٸ� �̸��� �������� Ȯ���մϴ�.
        private bool CheckEmailAddress(string email)
        {
            // ����ִ� ��� �߸��� ����
            if (string.IsNullOrEmpty(email)) isValidFormat = false;

            email = Regex.Replace(email, @"(@)(.+)$", this.DomainMapper, RegexOptions.None);
            if (invalidEmailType) isValidFormat = false;

            // true �� ��ȯ�� ��, �ùٸ� �̸��� ������.
            isValidFormat = Regex.IsMatch(email,
                          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                          RegexOptions.IgnoreCase);

            return isValidFormat;
        }

        // �̸��� ���̵�� �������������� �з� ���ݴϴ�.
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalidEmailType = true;
            }
            return match.Groups[1].Value + domainName;
        }


    }
}