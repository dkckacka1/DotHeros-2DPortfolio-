using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static LobbyManager instance;
        private static LobbyUIManager uiManager;
        public static LobbyManager Instance { get => instance; }
        public static LobbyUIManager UIManager { get => uiManager; }

        public Unit userSelectedUnit;


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
    }
}