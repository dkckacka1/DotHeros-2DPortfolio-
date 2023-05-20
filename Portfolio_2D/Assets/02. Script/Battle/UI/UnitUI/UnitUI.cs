using System;
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


        [SerializeField] private UnitHPUI unitHPUI;

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }
            
        public void SetCurrentTurnUI(bool isTurn) => currentTurnUIObject.SetActive(isTurn);

        public void SetTargetedUI(bool isTarget) => targetedUIObject.SetActive(isTarget);

        public void SetUnit(Unit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
        }

        public void Unit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }
    }

}