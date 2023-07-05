using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 씬을 로드하는 함수를 모아놓은 static 클래스
 */

namespace Portfolio
{
    public static class SceneLoader 
    {
        public static List<Unit> userChoiceUnits;   // 배틀씬 들어갈 때 유저가 선택한 유닛들을 저장할 유닛 리스트
        public static Map userChocieMap;            // 유저가 선택한 맵

        public static bool isLobbySceneLoad;        // 로비씬을 불러올 준비가 됬는지 확인
        public static string GetSceneName => SceneManager.GetActiveScene().name; // 현재 활성화된 씬이름 가져온다

        // 로비씬을 불러온다.
        public static void LoadLobbyScene()
        {
            SceneManager.LoadScene("Lobby");
            // 유저 인포를 보여준다.
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        // 월드맵씬을 불러온다.
        public static void LoadWorldMapScene()
        {
            SceneManager.LoadScene("WorldMap");
            // 유저 인포를 보여준다.
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        // 전투씬을 불러온다.
        public static void LoadBattleScene(List<Unit> userChoiceUnits, Map userChocieMap)
        {
            // 유저가 선택한 유닛리스트를 참조한다.
            SceneLoader.userChoiceUnits = userChoiceUnits;
            // 유저가 선택한 맵을 참조한다.
            SceneLoader.userChocieMap = userChocieMap;
            // 유저 정보를 숨긴다.
            GameManager.UIManager.HideUserInfoCanvas();
            // 전투씬을 비동기 로드한다.
            GameManager.Instance.StartCoroutine(LoadBattleSceneAsync());
        }

        // 전투씬을 비동기 로드한다.
        private static IEnumerator LoadBattleSceneAsync()
        {
            var oper = SceneManager.LoadSceneAsync("Battle");
            oper.allowSceneActivation = false;
            while (!oper.isDone)
            {
                yield return null;
                if (oper.progress >= 0.9f)
                    // 90% 이상 로드 되었을 때
                {
                    // 씬을 활성화 한다.
                    oper.allowSceneActivation = true;
                    break;
                }
            }
        }

        // 로비 씬을 비동기 로드한다.
        public static IEnumerator LoadLobbySceneAsync()
        {
            AsyncOperation oper = SceneManager.LoadSceneAsync("Lobby");
            oper.allowSceneActivation = false;
            isLobbySceneLoad = false;
            while (!oper.isDone)
            {
                yield return null;
                if (oper.progress >= 0.9f && isLobbySceneLoad)
                    // 90% 이상 로드 되었고, 로딩이 끝났으면
                {
                    // 씬을 활성화 한다.
                    oper.allowSceneActivation = true;
                    break;
                }
            }

            isLobbySceneLoad = false;
        }
    }
}