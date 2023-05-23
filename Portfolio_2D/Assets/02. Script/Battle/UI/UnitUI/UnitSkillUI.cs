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

        public SkillActionEventArgs(BattleUnit targetUnit)
        {
            this.targetUnit = targetUnit;
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

        private Skill basicAttackSkill;
        private Skill activeSkill_1;
        private Skill activeSkill_2;

        private event EventHandler<BattleUnit> OnActionBtnEvent;

        public void SetUnit(BattleUnit unit)
        {
            this.unit = unit;
        }

        public void SetSkill(Skill basicAttackSkill, Skill activeSkill_1, Skill activeSkill_2)
        {
            Debug.Log("스킬 세트");
            this.basicAttackSkill = basicAttackSkill;
            basicAttackSkill?.SetCurrentTurnUnit(unit);
            this.activeSkill_1 = activeSkill_1;
            activeSkill_1?.SetCurrentTurnUnit(unit);
            this.activeSkill_2 = activeSkill_2;
            activeSkill_2?.SetCurrentTurnUnit(unit);
        }

        public void ShowSkillUI() => this.gameObject.SetActive(true);
        public void HideSkillUI() => this.gameObject.SetActive(false);

        //===========================================================
        // ButtonPlugin
        //===========================================================
        public void TurnEnd()
        {
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void BasicAttack()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(basicAttackSkill);
            OnActionBtnEvent = basicAttackSkill.TakeAction;
        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(activeSkill_1);
            OnActionBtnEvent = activeSkill_1.TakeAction;
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
            BattleManager.ActionSystem.SetHowToTarget(activeSkill_2);
            OnActionBtnEvent = activeSkill_2.TakeAction;
        }

        public void Action()
        {
            foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
            {
                OnActionBtnEvent.Invoke(this, unit);
            }
            TurnEnd();
        }
    }
}