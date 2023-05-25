using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class SkillActionEventArgs : EventArgs
    {
        public BattleUnit targetUnit;
        public int skillLevel;

        public SkillActionEventArgs(BattleUnit targetUnit, int skillLevel = 1)
        {
            this.targetUnit = targetUnit;
            this.skillLevel = skillLevel;
        }
    }

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
        // TODO

        public void BasicAttack()
        {
            BattleManager.ActionSystem.IsSkillAction = true;
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(basicAttackSkill);
            //OnActionBtnEvent = basicAttackSkill.TakeAction;
            actionBtn.interactable = true;
        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.IsSkillAction = true;
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(activeSkill_1);
            //OnActionBtnEvent = activeSkill_1.TakeAction;
            actionBtn.interactable = true;
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.IsSkillAction = true;
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(activeSkill_2);
            //OnActionBtnEvent = activeSkill_2.TakeAction;
            actionBtn.interactable = true;
        }

        public void Action()
        {
            foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
            {
                OnActionBtnEvent.Invoke(this, new SkillActionEventArgs(unit, 1));
            }
            TurnEnd();
        }
    }
}