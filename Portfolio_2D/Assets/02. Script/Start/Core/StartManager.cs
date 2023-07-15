using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��ŸƮ���� �Ŵ��� Ŭ����
 */

namespace Portfolio.Start
{
    public class StartManager : MonoBehaviour
    {
        private static StartManager instance;
        private static StartUIManager uiManager;
        public static StartManager Instance => instance;
        public static StartUIManager UIManager => uiManager;


        // �̱��� ����
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
            // ���� �� ������ ����մϴ�.
            GameManager.AudioManager.PlayMusic("Music_StartScene");
        }

        // ORDER : �񵿱� �� ȣ���� ���� �ε� ����
        // �κ������ ������ �ε�â�� �����ְ� �����͸� �ε��Ѵ�.
        public void GotoLobby(UserData loginUserData)
        {
            // �κ���� �񵿱� �ε��մϴ�.
            StartCoroutine(SceneLoader.LoadLobbySceneAsync());
            // �ε����� �����ݴϴ�.
            StartCoroutine(LoadingStart(loginUserData));
        }

        // �ε��� �����մϴ�.
        private IEnumerator LoadingStart(UserData loginUserData)
        {
            // �ε�â�� �����ݴϴ�.
            GameManager.UIManager.ShowLoading();
            // �����͸� �ε��մϴ�. ���ҽ��� �����͸� �ҷ��� Dictonary�� ���ε��մϴ�.
            // �Ϸ�� �ε� �����̴��� 30% ä��� 1�ʴ���մϴ�.
            if (!GameManager.Instance.isLoaded)
                // �̹� �����Ϳ� ���ҽ��� �ε�� ���°� �ƴ϶��
            {
                yield return StartCoroutine(GameManager.UIManager.Loading(() =>
                {
                    GameManager.Instance.LoadData();
                    GameManager.Instance.isLoaded = true;
                }, "�����͸� �ε� �ϰ� �ֽ��ϴ�.", 0.3f, 1f));
            }
            else
                // �̹� �����Ϳ� ���ҽ��� �ε�� ���¶�� ��� �۾��� ���� �ʴ´�.
            {
                yield return StartCoroutine(GameManager.UIManager.Loading(() =>
                {
                }, "�����͸� �ε� �ϰ� �ֽ��ϴ�.", 0.3f, 0.5f));
            }

            // ���� ������ UI�� ǥ���ϰ� �ð� üũ�� �����մϴ�.
            // �Ϸ�� �ε� �����̴��� 30% ä��� 1�ʴ���մϴ�.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.UIManager.ShowUserInfoCanvas();
                GameManager.TimeChecker.CheckEnergy();
            }, "UI ������ �������� �����ϰ� �ֽ��ϴ�..", 0.3f, 1f));
            // ���� �����ͷ� ������ �����մϴ�.
            // �Ϸ�� �ε� �����̴��� 30% ä��� 1�ʴ���մϴ�.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
                GameManager.Instance.LoadUser(loginUserData);
                GameManager.UIManager.ShowUserInfo();
            }, "���� ������ �ҷ����� �ֽ��ϴ�.", 0.3f, 1f));

            // �ε��� �Ϸ��մϴ�.
            // �Ϸ�� �ε� �����̴��� 10% ä��� 0.2�ʴ���մϴ�.
            yield return StartCoroutine(GameManager.UIManager.Loading(() =>
            {
            }, "�ε� �Ϸ�", 0.1f, 0.2f));

            // ��� �۾��� �Ϸ�Ǹ� �κ���� �ҷ����� �ε�â�� �����ݴϴ�.
            SceneLoader.isLobbySceneLoad = true;
            GameManager.UIManager.EndLoading();
        }
    }

}