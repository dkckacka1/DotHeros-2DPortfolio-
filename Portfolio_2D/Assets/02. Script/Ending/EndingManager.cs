using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �������� �����ϴ� �Ŵ��� Ŭ����
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