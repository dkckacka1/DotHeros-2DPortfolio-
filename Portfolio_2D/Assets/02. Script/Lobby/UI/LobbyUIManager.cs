using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        [SerializeField] GameObject heroPanel;

        public void ShowCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        public void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}