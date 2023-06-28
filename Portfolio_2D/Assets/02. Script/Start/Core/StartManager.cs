using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void GotoLobby(UserData loginUserData)
        {
            GameManager.Instance.LoadData();
            GameManager.Instance.LoadUser(loginUserData);
            SceneLoader.LoadLobbyScene();
            GameManager.UIManager.ShowUserInfo();
            GameManager.TimeChecker.CheckEnergy();
        }
    }

}