using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 로비 씬에서 사용되는 매니저 클래스
 */

namespace Portfolio.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static LobbyManager instance;                           // 로비 매니저 싱글턴
        private static LobbyUIManager uiManager;                        // 로비 UI매니저
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
            // 로비 음악을 재생합니다.
            GameManager.AudioManager.PlayMusic("Music_LobbyScene");
        }
    }
}