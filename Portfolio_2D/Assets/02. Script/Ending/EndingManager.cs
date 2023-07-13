using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 엔딩씬을 관리하는 매니저 클래스
 */

namespace Portfolio.Ending
{
    public class EndingManager : MonoBehaviour
    {
        public void BTN_OnClick_GotoLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}