using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;

        [SerializeField] private GameObject currentTurnUIObject;
        [SerializeField] private GameObject targetedUIObject;

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }
            
        public void SetCurrentTurnUI(bool isTurn) => currentTurnUIObject.SetActive(isTurn);

        public void SetTargetedUI(bool isTarget) => targetedUIObject.SetActive(isTarget);
    }

}