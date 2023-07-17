using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 *  게임 전체 UI를 관리하는 UI매니저 클래스
 */

namespace Portfolio.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UserInfoUI userInfoUI;                             // 유저 정보를 표시하는 UI
        [SerializeField] UserPortraitChangePopupUI portraitChangePopupUI;   // 유저 이미지를 변경하는 팝업창

        [Header("LoadingCanvas")]
        [SerializeField] CanvasGroup loadingCanvasGroup;                // 로딩 캔버스 그룹
        [SerializeField] GameObject sceneLoadingObj;                    // 씬을 로드할때 보여줄 로딩 오브젝트
        [SerializeField] GameObject networkLoadingObj;                  // 네트워크와 통신할 때 보여줄 로딩 오브젝트
        [SerializeField] TextMeshProUGUI loadingText;                   // 로딩 텍스트
        [SerializeField] TextMeshProUGUI loadingProgressText;           // 로딩 진행 텍스트
        [SerializeField] Slider loadingSlider;                          // 로딩 슬라이더
        [SerializeField] float loadingFadeOutTime;                      // 로딩창이 사라지는 시간

        [Header("ConfigurePopup")]
        [SerializeField] ConfigurePopupUI configurePopupUI;             // 환경설정 팝업창

        [Header("AlertPopup")]
        [SerializeField] AlertPoupUI alertPopup;                        // 경고 팝업창
        [SerializeField] ConfirmationPopupUI confirmationPopup;         // 확인 팝업창

        public UserInfoUI UserInfoUI => userInfoUI;

   
        // 유저 인포를 현재 사용자에 맞게 업데이트 한다.
        public void ShowUserInfo()
        {
            userInfoUI.Show(GameManager.CurrentUser);
        }

        // 에너지 회복 시간을 업데이트 한다.
        public void ShowRemainTime(int time)
        {
            userInfoUI.ShowRemainTime(time);
        }

        // 유저 인포를 보여준다.
        public void ShowUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(true);
        }

        // 유저 인포를 숨겨준다.
        public void HideUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(false);
        }

        // 환경설정 팝업창을 표시한다.
        public void BTN_OnClick_ShowConfigure()
        {
            configurePopupUI.Show();
        }

        // ORDER : 자주 사용되는 UI 팝업창을 재활용
        // 경고 팝업창을 표시한다.
        public void ShowAlert(string text)
        {
            alertPopup.Show(text);
        }

        // 확인 팝업창을 표시한다.
        public void ShowConfirmation(string title, string alertText, UnityAction confirmEvent)
        {
            confirmationPopup.Show(title, alertText, confirmEvent);
        }

        // 유저 이미지 팝업창을 표시한다.
        public void BTN_OnClick_ShowPortraitChangePopupUI()
        {
            // 현재 로비씬이아니면 리턴
            if (SceneLoader.GetSceneName != "Lobby") return;

            portraitChangePopupUI.Show();
        }

        // 로딩창을 보여준다.
        public void ShowSceneLoading()
        {
            // 알맞는 오브젝트를 보여줍니다.
            sceneLoadingObj.SetActive(true);
            networkLoadingObj.SetActive(false);
            // 로딩 캔버스 온
            loadingCanvasGroup.gameObject.SetActive(true);

            // 알파값 1로 조정
            loadingCanvasGroup.alpha = 1;
            // 로딩 슬라이더 0으로 조절
            loadingSlider.value = 0;
            loadingProgressText.text = "0%";
        }

        public void ShowNetworkLoading()
        {
            // 알맞는 오브젝트를 보여줍니다.
            sceneLoadingObj.SetActive(false);
            networkLoadingObj.SetActive(true);

            // 로딩 캔버스 온
            loadingCanvasGroup.gameObject.SetActive(true);
            // 알파값 1로 조정
            loadingCanvasGroup.alpha = 1;
        }

        // 로딩한다.
        public IEnumerator Loading(UnityAction loadAction, string loadingTxt, float addProgress, float waitTime)
        {
            // 로딩 텍스트 입력
            loadingText.text = loadingTxt;
            // 무엇을 로딩할 것인지
            loadAction?.Invoke();

            // 로딩 슬라이더 변경
            loadingSlider.value += addProgress;
            loadingProgressText.text = (loadingSlider.value / loadingSlider.maxValue * 100).ToString() + "%";

            // 잠시 대기할건지
            yield return new WaitForSeconds(waitTime);
        }

        // 로딩창을 숨겨준다.
        public void EndLoading()
        {
            StartCoroutine(FadeOutLoading());
        }

        // 로딩창을 페이드 아웃한다.
        private IEnumerator FadeOutLoading()
        {
            while(true)
            {
                // 설정한 시간에 맞게 알파값을 줄여준다.
                loadingCanvasGroup.alpha -= Time.deltaTime / loadingFadeOutTime;
                if (loadingCanvasGroup.alpha <= 0.01f)
                {
                    break;
                }
                yield return null;
            }

            // 거의 보이지 않을정도가 되면 캔버스를 숨긴다.
            loadingCanvasGroup.gameObject.SetActive(false);
        }
    }
}