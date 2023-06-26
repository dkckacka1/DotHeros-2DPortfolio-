using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Start
{
    public class StartManager : MonoBehaviour
    {
        public void GotoLobby()
        {
            GameManager.Instance.LoadData();
            GameManager.UIManager.ShowUserInfo();
            SceneLoader.LoadLobbyScene();
        }
    }

}