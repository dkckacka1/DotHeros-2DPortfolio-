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
        private void Start()
        {
            GameManager.AudioManager.PlaySound("Sound_Ending");
        }

        // �κ� ������ ���ư��ϴ�.
        public void BTN_OnClick_GotoLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}