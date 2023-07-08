using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO : �ּ��� ���⼭ ���� �޾ƾ� �մϴ�.

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

        // ORDER : �񵿱� �� ȣ���� ���� �ε� ����
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
            }, "�����͸� �ε� �ϰ� �ֽ��ϴ�.", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                Debug.Log("TestTests");
                GameManager.UIManager.ShowUserInfoCanvas();
                GameManager.TimeChecker.CheckEnergy();
            }, "UI ������ �������� �����ϰ� �ֽ��ϴ�..", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.Instance.LoadUser(loginUserData);
            }, "���� ������ �ҷ����� �ֽ��ϴ�.", 0.3f, 1f));

            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
            }, "�ε� �Ϸ�", 0.1f, 0.2f));
            SceneLoader.isLobbySceneLoad = true;
            GameManager.UIManager.EndLoading();
        }
    }

}