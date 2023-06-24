using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Battle
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
        [SerializeField] private List<BattleUnitConditionUI> conditionUIList = new List<BattleUnitConditionUI>();

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
            conditionLayout.gameObject.SetActive(false);
            unitSequenceUI.gameObject.SetActive(false);
            unitHPUI.gameObject.SetActive(false);
        }

        public void SetCurrentTurnUI(bool isTurn)
        {
            currentTurnUIObject.SetActive(isTurn);
        }
        public void SetTargetedUI(bool isTarget)
        {
            targetedUIObject.SetActive(isTarget);
        }
        public void SetBattleUnit(BattleUnit unit)
        {
            unitHPUI.SetHP(unit.MaxHP);
            CreateSequenceUI(unit);
            if (!unit.IsEnemy)
            {
                CreateSkillUI(unit);
            }
        }

        public void BattleUnit_OnCurrentHPChangedEvent(object sender, EventArgs e)
        {
            ChangeCurrnetHPEventArgs args = (ChangeCurrnetHPEventArgs)e;

            unitHPUI.ChangeHP(args.currentHP);
        }

        public BattleUnitConditionUI CreateConditionUI(int count, Condition condition)
        {
            // TODO
            var conditionUI = conditionUIList.Where(ui => !ui.gameObject.activeInHierarchy).First();
            conditionUI.ShowCondition(condition);
            conditionUI.isActive = true;
            conditionUI.SetCount(count);
            conditionUI.gameObject.SetActive(true);
            return conditionUI;
        }

        public void CreateSequenceUI(BattleUnit battleUnit)
        {
            unitSequenceUI = BattleManager.BattleUIManager.CreateUnitSequenceUI();
            unitSequenceUI.ShowUnit(battleUnit);
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