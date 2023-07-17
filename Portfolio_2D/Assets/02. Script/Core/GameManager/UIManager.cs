using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 *  ���� ��ü UI�� �����ϴ� UI�Ŵ��� Ŭ����
 */

namespace Portfolio.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UserInfoUI userInfoUI;                             // ���� ������ ǥ���ϴ� UI
        [SerializeField] UserPortraitChangePopupUI portraitChangePopupUI;   // ���� �̹����� �����ϴ� �˾�â

        [Header("LoadingCanvas")]
        [SerializeField] CanvasGroup loadingCanvasGroup;                // �ε� ĵ���� �׷�
        [SerializeField] GameObject sceneLoadingObj;                    // ���� �ε��Ҷ� ������ �ε� ������Ʈ
        [SerializeField] GameObject networkLoadingObj;                  // ��Ʈ��ũ�� ����� �� ������ �ε� ������Ʈ
        [SerializeField] TextMeshProUGUI loadingText;                   // �ε� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI loadingProgressText;           // �ε� ���� �ؽ�Ʈ
        [SerializeField] Slider loadingSlider;                          // �ε� �����̴�
        [SerializeField] float loadingFadeOutTime;                      // �ε�â�� ������� �ð�

        [Header("ConfigurePopup")]
        [SerializeField] ConfigurePopupUI configurePopupUI;             // ȯ�漳�� �˾�â

        [Header("AlertPopup")]
        [SerializeField] AlertPoupUI alertPopup;                        // ��� �˾�â
        [SerializeField] ConfirmationPopupUI confirmationPopup;         // Ȯ�� �˾�â

        public UserInfoUI UserInfoUI => userInfoUI;

   
        // ���� ������ ���� ����ڿ� �°� ������Ʈ �Ѵ�.
        public void ShowUserInfo()
        {
            userInfoUI.Show(GameManager.CurrentUser);
        }

        // ������ ȸ�� �ð��� ������Ʈ �Ѵ�.
        public void ShowRemainTime(int time)
        {
            userInfoUI.ShowRemainTime(time);
        }

        // ���� ������ �����ش�.
        public void ShowUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(true);
        }

        // ���� ������ �����ش�.
        public void HideUserInfoCanvas()
        {
            userInfoUI.transform.parent.gameObject.SetActive(false);
        }

        // ȯ�漳�� �˾�â�� ǥ���Ѵ�.
        public void BTN_OnClick_ShowConfigure()
        {
            configurePopupUI.Show();
        }

        // ORDER : ���� ���Ǵ� UI �˾�â�� ��Ȱ��
        // ��� �˾�â�� ǥ���Ѵ�.
        public void ShowAlert(string text)
        {
            alertPopup.Show(text);
        }

        // Ȯ�� �˾�â�� ǥ���Ѵ�.
        public void ShowConfirmation(string title, string alertText, UnityAction confirmEvent)
        {
            confirmationPopup.Show(title, alertText, confirmEvent);
        }

        // ���� �̹��� �˾�â�� ǥ���Ѵ�.
        public void BTN_OnClick_ShowPortraitChangePopupUI()
        {
            // ���� �κ���̾ƴϸ� ����
            if (SceneLoader.GetSceneName != "Lobby") return;

            portraitChangePopupUI.Show();
        }

        // �ε�â�� �����ش�.
        public void ShowSceneLoading()
        {
            // �˸´� ������Ʈ�� �����ݴϴ�.
            sceneLoadingObj.SetActive(true);
            networkLoadingObj.SetActive(false);
            // �ε� ĵ���� ��
            loadingCanvasGroup.gameObject.SetActive(true);

            // ���İ� 1�� ����
            loadingCanvasGroup.alpha = 1;
            // �ε� �����̴� 0���� ����
            loadingSlider.value = 0;
            loadingProgressText.text = "0%";
        }

        public void ShowNetworkLoading()
        {
            // �˸´� ������Ʈ�� �����ݴϴ�.
            sceneLoadingObj.SetActive(false);
            networkLoadingObj.SetActive(true);

            // �ε� ĵ���� ��
            loadingCanvasGroup.gameObject.SetActive(true);
            // ���İ� 1�� ����
            loadingCanvasGroup.alpha = 1;
        }

        // �ε��Ѵ�.
        public IEnumerator Loading(UnityAction loadAction, string loadingTxt, float addProgress, float waitTime)
        {
            // �ε� �ؽ�Ʈ �Է�
            loadingText.text = loadingTxt;
            // ������ �ε��� ������
            loadAction?.Invoke();

            // �ε� �����̴� ����
            loadingSlider.value += addProgress;
            loadingProgressText.text = (loadingSlider.value / loadingSlider.maxValue * 100).ToString() + "%";

            // ��� ����Ұ���
            yield return new WaitForSeconds(waitTime);
        }

        // �ε�â�� �����ش�.
        public void EndLoading()
        {
            StartCoroutine(FadeOutLoading());
        }

        // �ε�â�� ���̵� �ƿ��Ѵ�.
        private IEnumerator FadeOutLoading()
        {
            while(true)
            {
                // ������ �ð��� �°� ���İ��� �ٿ��ش�.
                loadingCanvasGroup.alpha -= Time.deltaTime / loadingFadeOutTime;
                if (loadingCanvasGroup.alpha <= 0.01f)
                {
                    break;
                }
                yield return null;
            }

            // ���� ������ ���������� �Ǹ� ĵ������ �����.
            loadingCanvasGroup.gameObject.SetActive(false);
        }
    }
}