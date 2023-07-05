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
        [SerializeField] CanvasGroup loadingCanvasGroup;                // 로딩 캔버스 그룹
        [SerializeField] TextMeshProUGUI loadingText;                   // 로딩 텍스트
        [SerializeField] TextMeshProUGUI loadingProgressText;           // 로딩 진행 텍스트
        [SerializeField] Slider loadingSlider;                          // 로딩 슬라이더
        [SerializeField] float loadingFadeOutTime;                       // 로딩창이 사라지는 시간

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

        // 로딩창을 보여준다.
        public void ShowLoading()
        {
            // 로딩 캔버스 온
            loadingCanvasGroup.gameObject.SetActive(true);
            // 알파값 1로 조정
            loadingCanvasGroup.alpha = 1;
            // 로딩 슬라이더 0으로 조절
            loadingSlider.value = 0;
            loadingProgressText.text = "0%";
        }

        // 로딩한다.
        public void Loading(UnityAction loadAction, string loadingTxt, float addProgress)
        {
            loadingText.text = loadingTxt;
            loadAction?.Invoke();
            loadingSlider.value += addProgress;
        }

        // 로딩창을 숨겨준다.
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