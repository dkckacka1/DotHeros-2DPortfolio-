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
        public static MapData userChocieMapData;

        public static void LoadBattleScene(List<Unit> userChoiceUnits, MapData userChocieMapData)
        {
            SceneLoader.userChoiceUnits = userChoiceUnits;
            SceneLoader.userChocieMapData = userChocieMapData;
            GameManager.Instance.StartCoroutine(LoadBattleSceneAsync());
        }

        private static IEnumerator LoadBattleSceneAsync()
        {
            var oper = SceneManager.LoadSceneAsync("Battle");
            oper.allowSceneActivation = false;
            while (!oper.isDone)
            {
                yield return null;
                Debug.Log(oper.progress);
                if (oper.progress >= 0.9f)
                {
                    oper.allowSceneActivation = true;
                    break;
                }
            }
        }
    }
}