using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * ���� �ε��ϴ� �Լ��� ��Ƴ��� static Ŭ����
 */

namespace Portfolio
{
    public static class SceneLoader 
    {
        public static List<Unit> userChoiceUnits;   // ��Ʋ�� �� �� ������ ������ ���ֵ��� ������ ���� ����Ʈ
        public static Map userChocieMap;            // ������ ������ ��

        public static bool isLobbySceneLoad;        // �κ���� �ҷ��� �غ� ����� Ȯ��
        public static string GetSceneName => SceneManager.GetActiveScene().name; // ���� Ȱ��ȭ�� ���̸� �����´�

        // �κ���� �ҷ��´�.
        public static void LoadLobbyScene()
        {
            SceneManager.LoadScene("Lobby");
            // ���� ������ �����ش�.
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        // ����ʾ��� �ҷ��´�.
        public static void LoadWorldMapScene()
        {
            SceneManager.LoadScene("WorldMap");
            // ���� ������ �����ش�.
            GameManager.UIManager.ShowUserInfoCanvas();
        }

        // �������� �ҷ��´�.
        public static void LoadBattleScene(List<Unit> userChoiceUnits, Map userChocieMap)
        {
            // ������ ������ ���ָ���Ʈ�� �����Ѵ�.
            SceneLoader.userChoiceUnits = userChoiceUnits;
            // ������ ������ ���� �����Ѵ�.
            SceneLoader.userChocieMap = userChocieMap;
            // ���� ������ �����.
            GameManager.UIManager.HideUserInfoCanvas();
            // �������� �񵿱� �ε��Ѵ�.
            GameManager.Instance.StartCoroutine(LoadBattleSceneAsync());
        }

        // �������� �񵿱� �ε��Ѵ�.
        private static IEnumerator LoadBattleSceneAsync()
        {
            var oper = SceneManager.LoadSceneAsync("Battle");
            oper.allowSceneActivation = false;
            while (!oper.isDone)
            {
                yield return null;
                if (oper.progress >= 0.9f)
                    // 90% �̻� �ε� �Ǿ��� ��
                {
                    // ���� Ȱ��ȭ �Ѵ�.
                    oper.allowSceneActivation = true;
                    break;
                }
            }
        }

        // �κ� ���� �񵿱� �ε��Ѵ�.
        public static IEnumerator LoadLobbySceneAsync()
        {
            AsyncOperation oper = SceneManager.LoadSceneAsync("Lobby");
            oper.allowSceneActivation = false;
            isLobbySceneLoad = false;
            while (!oper.isDone)
            {
                yield return null;
                if (oper.progress >= 0.9f && isLobbySceneLoad)
                    // 90% �̻� �ε� �Ǿ���, �ε��� ��������
                {
                    // ���� Ȱ��ȭ �Ѵ�.
                    oper.allowSceneActivation = true;
                    break;
                }
            }

            isLobbySceneLoad = false;
        }
    }
}