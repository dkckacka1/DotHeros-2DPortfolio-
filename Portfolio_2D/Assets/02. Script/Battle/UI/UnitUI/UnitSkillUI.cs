using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{


    public class UnitSkillUI : MonoBehaviour
    {
        private BattleUnit unit;

        [SerializeField] Button turnEndBtn;
        [SerializeField] Button BasicAttackBtn;
        [SerializeField] Button activeSkill_1_ActionBtn;
        [SerializeField] Button activeSkill_2_ActionBtn;
        [SerializeField] Button actionBtn;

        private ActiveSkill basicAttackSkill;
        private ActiveSkill activeSkill_1;
        private ActiveSkill activeSkill_2;
        private int activeSkill_1_Level = 1;
        private int activeSkill_2_Level = 1;

        private int actionLevel = 1;
        private event EventHandler<SkillActionEventArgs> OnActionBtnEvent;

        public void SetUnit(BattleUnit battleUnit)
        {
            this.unit = battleUnit;

        }

        public void SetSkill(Unit unit)
        {
            basicAttackSkill = unit.basicAttackSkill;
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
            activeSkill_1_Level = unit.activeSkillLevel_1;
            activeSkill_2_Level = unit.activeSkillLevel_2;

            if (activeSkill_1 != null)
            {
            }
            else
            {
                activeSkill_1_ActionBtn.gameObject.SetActive(false);
            }


            if (activeSkill_2 != null)
            {
            }
            else
            {
                activeSkill_2_ActionBtn.gameObject.SetActive(false);
            }
        }

        public void ShowSkillUI() => this.gameObject.SetActive(true);
        public void HideSkillUI() => this.gameObject.SetActive(false);

        //===========================================================
        // ButtonPlugin
        //===========================================================

        public void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            OnActionBtnEvent = null;
            actionBtn.interactable = false;
            ShowSkillUI();
        }

        public void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            HideSkillUI();
        }

        public void TurnEnd()
        {
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void BasicAttack()
        {
            BattleManager.ActionSystem.SetActiveSkill(basicAttackSkill);
            actionBtn.interactable = true;
            actionLevel = 1;
            OnActionBtnEvent = basicAttackSkill.Action;

        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
            actionBtn.interactable = true;
            actionLevel = activeSkill_1_Level;
            OnActionBtnEvent = activeSkill_1.Action;
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
            actionBtn.interactable = true;
            actionLevel = activeSkill_2_Level;
            OnActionBtnEvent = activeSkill_2.Action;
        }

        public void Action()
        {
            foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
            {
                OnActionBtnEvent.Invoke(this, new SkillActionEventArgs(actionLevel, this.unit, unit));
            }
            TurnEnd();
        }
    }
}