using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �κ� ������ ���Ǵ� �Ŵ��� Ŭ����
 */

namespace Portfolio.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static LobbyManager instance;                           // �κ� �Ŵ��� �̱���
        private static LobbyUIManager uiManager;                        // �κ� UI�Ŵ���
        public static LobbyManager Instance { get => instance; }
        public static LobbyUIManager UIManager { get => uiManager; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                uiManager = GetComponentInChildren<LobbyUIManager>();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            // �κ� ������ ����մϴ�.
            GameManager.AudioManager.PlayMusic("Music_LobbyScene");
        }
    }
}