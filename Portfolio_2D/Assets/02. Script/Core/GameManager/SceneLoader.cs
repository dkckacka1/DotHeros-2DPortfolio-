using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Portfolio
{
    public static class SceneLoader 
    {
        public static List<Unit> userChoiceUnits;
        public static Map userChocieMap;

        public static string GetSceneName => SceneManager.GetActiveScene().name;
        public static void LoadLobbyScene()
        {
            SceneManager.LoadScene("Lobby");
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        public static void LoadWorldMapScene()
        {
            SceneManager.LoadScene("WorldMap");
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        public static void LoadBattleScene(List<Unit> userChoiceUnits, Map choiceMap)
        {
            SceneLoader.userChoiceUnits = userChoiceUnits;
            SceneLoader.userChocieMap = choiceMap;
            GameManager.UIManager.HideUserInfoCanvas();
            GameManager.Instance.StartCoroutine(LoadBattleSceneAsync());
        }

        private static IEnumerator LoadBattleSceneAsync()
        {
            var oper = SceneManager.LoadSceneAsync("Battle");
            oper.allowSceneActivation = false;
            while (!oper.isDone)
            {
                yield return null;
                if (oper.progress >= 0.9f)
                {
                    oper.allowSceneActivation = true;
                    break;
                }
            }
        }
    }
}