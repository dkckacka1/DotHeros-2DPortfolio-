using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;

        [Header("턴 UI")]
        [SerializeField] private GameObject currentTurnUIObject;
        [SerializeField] private GameObject targetedUIObject;

        [Header("HP바 UI")]
        [SerializeField] private UnitHPUI unitHPUI;

        [Header("상태이상 UI")]
        [SerializeField] private RectTransform conditionLayout;
        [SerializeField] private UnitConditionUI conditionUIPrefab;



        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }
            
        public void SetCurrentTurnUI(bool isTurn) => currentTurnUIObject.SetActive(isTurn);

        public void SetTargetedUI(bool isTarget) => targetedUIObject.SetActive(isTarget);

        public void SetUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
        }

        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        public UnitConditionUI CreateConditionUI(int count)
        {
            var ui = Instantiate(conditionUIPrefab, conditionLayout);
            ui.SetCount(count);
            return ui;
        }
    }

}