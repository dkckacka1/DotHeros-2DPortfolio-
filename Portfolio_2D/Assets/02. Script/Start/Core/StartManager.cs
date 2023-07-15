using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스타트씬의 매니저 클래스
 */

namespace Portfolio.Start
{
    public class StartManager : MonoBehaviour
    {
        private static StartManager instance;
        private static StartUIManager uiManager;
        public static StartManager Instance => instance;
        public static StartUIManager UIManager => uiManager;


        // 싱글턴 선언
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

        private void Start()
        {
            // 시작 씬 음악을 재생합니다.
            GameManager.AudioManager.PlayMusic("Music_StartScene");
        }

        // ORDER : 비동기 씬 호출을 통한 로딩 구현
        // 로비씬으로 들어가기전 로딩창을 보여주고 데이터를 로드한다.
        public void GotoLobby(UserData loginUserData)
        {
            // 로비씬을 비동기 로드합니다.
            StartCoroutine(SceneLoader.LoadLobbySceneAsync());
            // 로딩신을 보여줍니다.
            StartCoroutine(LoadingStart(loginUserData));
        }

        // 로딩을 시작합니다.
        private IEnumerator LoadingStart(UserData loginUserData)
        {
            // 로딩창을 보여줍니다.
            GameManager.UIManager.ShowLoading();
            // 데이터를 로드합니다. 리소스와 데이터를 불러와 Dictonary에 바인딩합니다.
            // 완료시 로딩 슬라이더를 30% 채우고 1초대기합니다.
            if (!GameManager.Instance.isLoaded)
                // 이미 데이터와 리소스가 로드된 상태가 아니라면
            {
                yield return StartCoroutine(GameManager.UIManager.Loading(() =>
                {
                    GameManager.Instance.LoadData();
                    GameManager.Instance.isLoaded = true;
                }, "데이터를 로드 하고 있습니다.", 0.3f, 1f));
            }
            else
                // 이미 데이터와 리소스가 로드된 상태라면 어떠한 작업도 하지 않는다.
            {
                yield return StartCoroutine(GameManager.UIManager.Loading(() =>
                {
                }, "데이터를 로드 하고 있습니다.", 0.3f, 0.5f));
            }

            // 유저 정보를 UI를 표시하고 시간 체크를 시작합니다.
            // 완료시 로딩 슬라이더를 30% 채우고 1초대기합니다.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.UIManager.ShowUserInfoCanvas();
                GameManager.TimeChecker.CheckEnergy();
            }, "UI 정보와 에너지를 충전하고 있습니다..", 0.3f, 1f));
            // 유저 데이터로 유저를 생성합니다.
            // 완료시 로딩 슬라이더를 30% 채우고 1초대기합니다.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.Instance.LoadUser(loginUserData);
                GameManager.UIManager.ShowUserInfo();
            }, "유저 정보를 불러오고 있습니다.", 0.3f, 1f));

            // 로딩을 완료합니다.
            // 완료시 로딩 슬라이더를 10% 채우고 0.2초대기합니다.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
            }, "로딩 완료", 0.1f, 0.2f));

            // 모든 작업이 완료되면 로비씬을 불러오고 로딩창을 숨겨줍니다.
            SceneLoader.isLobbySceneLoad = true;
            GameManager.UIManager.EndLoading();
        }
    }

}