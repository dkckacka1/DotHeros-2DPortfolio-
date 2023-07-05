using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Start
{
    public class StartManager : MonoBehaviour
    {
        private static StartManager instance;
        private static StartUIManager uiManager;
        public static StartManager Instance => instance;
        public static StartUIManager UIManager => uiManager;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                uiManager = GetComponentInChildren<StartUIManager>();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void GotoLobby(UserData loginUserData)
        {
            StartCoroutine(SceneLoader.LoadLobbySceneAsync());
            StartCoroutine(LoadingStart(loginUserData));
        }

        private IEnumerator LoadingStart(UserData loginUserData)
        {
            GameManager.UIManager.ShowLoading();
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.Instance.LoadData();
            }, "데이터를 로드 하고 있습니다.", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                Debug.Log("TestTests");
                GameManager.UIManager.ShowUserInfoCanvas();
                GameManager.TimeChecker.CheckEnergy();
            }, "UI 정보와 에너지를 충전하고 있습니다..", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.Instance.LoadUser(loginUserData);
            }, "유저 정보를 불러오고 있습니다.", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
            }, "로딩 완료", 0.1f, 0.2f));
            SceneLoader.isLobbySceneLoad = true;
            GameManager.UIManager.EndLoading();
        }
    }

}