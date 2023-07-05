using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Portfolio.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UserInfoUI userInfoUI;
        [SerializeField] UserPortraitChangePopupUI portraitChangePopupUI;

        [Header("LoadingCanvas")]
        [SerializeField] CanvasGroup loadingCanvasGroup;                // �ε� ĵ���� �׷�
        [SerializeField] TextMeshProUGUI loadingText;                   // �ε� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI loadingProgressText;           // �ε� ���� �ؽ�Ʈ
        [SerializeField] Slider loadingSlider;                          // �ε� �����̴�
        [SerializeField] float loadingFadeOutTime;                       // �ε�â�� ������� �ð�

        [Header("ConfigurePopup")]
        [SerializeField] ConfigurePopupUI configurePopupUI;

        [Header("AlertPopup")]
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

        public void ShowConfigure()
        {
            configurePopupUI.Show();
        }

        public void ShowAlert(string text)
        {
            alertPopup.Show(text);
        }

        public void ShowConfirmation(string title, string alertText, UnityAction confirmEvent)
        {
            confirmationPopup.Show(title, alertText, confirmEvent);
        }

        public void ShowPortraitChangePopupUI()
        {
            Debug.Log(SceneLoader.GetSceneName);
            if (SceneLoader.GetSceneName != "Lobby") return;

            portraitChangePopupUI.Show();
        }

        // �ε�â�� �����ش�.
        public void ShowLoading()
        {
            // �ε� ĵ���� ��
            loadingCanvasGroup.gameObject.SetActive(true);
            // ���İ� 1�� ����
            loadingCanvasGroup.alpha = 1;
            // �ε� �����̴� 0���� ����
            loadingSlider.value = 0;
            loadingProgressText.text = "0%";
        }

        // �ε��Ѵ�.
        public void Loading(UnityAction loadAction, string loadingTxt, float addProgress)
        {
            loadingText.text = loadingTxt;
            loadAction?.Invoke();
            loadingSlider.value += addProgress;
        }

        // �ε�â�� �����ش�.
        public void EndLoading()
        {
            StartCoroutine(FadeOutLoading());
        }
        private IEnumerator FadeOutLoading()
        {
            while(true)
            {
                loadingCanvasGroup.alpha -= Time.deltaTime / loadingFadeOutTime;
                if (loadingCanvasGroup.alpha <= 0.01f)
                {
                    break;
                }
                yield return null;
            }

            loadingCanvasGroup.gameObject.SetActive(false);
        }
    }
}