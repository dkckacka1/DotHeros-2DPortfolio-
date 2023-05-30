using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleUnitUI : MonoBehaviour
    {
        [SerializeField] private Canvas unitUICanvas;

        [Header("턴 UI")]
        [SerializeField] private GameObject currentTurnUIObject;
        [SerializeField] private GameObject targetedUIObject;
        private BattleUnitSequenceUI unitSequenceUI;

        [Header("HP바 UI")]
        [SerializeField] private BattleUnitHPUI unitHPUI;

        [Header("상태이상 UI")]
        [SerializeField] private RectTransform conditionLayout;
        [SerializeField] private BattleUnitConditionUI conditionUIPrefab;

        [Header("스킬 UI")]
        private BattleUnitSkillUI skillUI;


        public BattleUnitSequenceUI UnitSequenceUI { get => unitSequenceUI; }

        private void Awake()
        {
            unitUICanvas.worldCamera = Camera.main;
        }

        public void Win()
        {
            
        }

        public void Defeat()
        {

        }

        public void Dead()
        {
            unitSequenceUI.gameObject.SetActive(false);
        }

        public void SetCurrentTurnUI(bool isTurn)
        {
            currentTurnUIObject.SetActive(isTurn);
        }
        public void SetTargetedUI(bool isTarget)
        {
            targetedUIObject.SetActive(isTarget);
        }
        public void SetUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
        }

        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        public BattleUnitConditionUI CreateConditionUI(int count)
        {
            var ui = Instantiate(conditionUIPrefab, conditionLayout);
            ui.SetCount(count);
            return ui;
        }

        public void CreateSequenceUI(BattleUnit battleUnit)
        {
            unitSequenceUI = BattleManager.BattleUIManager.CreateUnitSequenceUI();
            unitSequenceUI.SetNameText(battleUnit.Unit.Data.unitName);
        }

        public void CreateSkillUI(BattleUnit battleUnit)
        {
            this.skillUI = BattleManager.BattleUIManager.CreateUnitSkillUI();
            this.skillUI.SetUnit(battleUnit);
        }

        public void ShowSkillUI()
        {
            skillUI?.ShowSkillUI();
        }

        public void HideSkillUI()
        {
            skillUI?.HideSkillUI();
        }

        public void ResetSkillUI(BattleUnit unit)
        {
            skillUI?.ResetSkillUI(unit);
        }
    }

}