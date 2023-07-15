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
        private void Start()
        {
            GameManager.AudioManager.PlaySound("Sound_Ending");
        }

        // 로비 씬으로 돌아갑니다.
        public void BTN_OnClick_GotoLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}