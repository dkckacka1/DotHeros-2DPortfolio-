using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio
{


    public class UnitSkillUI : MonoBehaviour
    {
        private BattleUnit unit;
        private BattleSkillDescUI battleSkillDescUI;

        [SerializeField] Button turnEndBtn;
        [SerializeField] Button BasicAttackBtn;
        [SerializeField] Button activeSkill_1_ActionBtn;
        [SerializeField] Button activeSkill_2_ActionBtn;
        [SerializeField] Button actionBtn;
        [SerializeField] TextMeshProUGUI skillCoolTime_1_Text;
        [SerializeField] TextMeshProUGUI skillCoolTime_2_Text;

        private ActiveSkill basicAttackSkill;
        private ActiveSkill activeSkill_1;
        private ActiveSkill activeSkill_2;
        private int activeSkill_1_Level = 1;
        private int activeSkill_2_Level = 1;

        private int activeSkillCoolTime_1 = 0;
        private int activeSkillCoolTime_2 = 0;

        private int actionLevel = 1;
        private ActiveSkill selectActiveSkill;

        public int ActiveSkillCoolTime_1 
        { 
            get => activeSkillCoolTime_1;
            set
            {
                activeSkillCoolTime_1 = value;
                skillCoolTime_1_Text.gameObject.SetActive(activeSkillCoolTime_1 != 0);
                skillCoolTime_1_Text.text = activeSkillCoolTime_1.ToString();
            }
        }

        public int ActiveSkillCoolTime_2 
        { 
            get => activeSkillCoolTime_2;
            set 
            { 
                activeSkillCoolTime_2 = value;
                skillCoolTime_2_Text.gameObject.SetActive(activeSkillCoolTime_2 != 0);
                skillCoolTime_2_Text.text = activeSkillCoolTime_2.ToString();
            }
        }

        //private event EventHandler<SkillActionEventArgs> OnActionBtnEvent;

        public void SetUnit(BattleUnit battleUnit)
        {
            this.unit = battleUnit;
        }

        public void SetBattleSkillDescUI(BattleSkillDescUI battleSkillDescUI)
        {
            this.battleSkillDescUI = battleSkillDescUI;
        }

        public void SetSkill(Unit unit)
        {
            basicAttackSkill = unit.basicAttackSkill;
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
            activeSkill_1_Level = unit.activeSkillLevel_1;
            activeSkill_2_Level = unit.activeSkillLevel_2;

            if (activeSkill_1 == null)
            {
                activeSkill_1_ActionBtn.gameObject.SetActive(false);
            }


            if (activeSkill_2 == null)
            {
                activeSkill_2_ActionBtn.gameObject.SetActive(false);
            }
        }

        public void ShowSkillUI() => this.gameObject.SetActive(true);
        public void HideSkillUI() => this.gameObject.SetActive(false);

        //===========================================================
        // ButtonPlugin
        //===========================================================

        private void Start()
        {
            ResetSkillUI();
        }

        public void ResetSkillUI()
        {
            selectActiveSkill = null;
            actionBtn.interactable = false;
        }

        public void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            SetActiveBtn(activeSkill_1_ActionBtn, activeSkill_1, ActiveSkillCoolTime_1);
            SetActiveBtn(activeSkill_2_ActionBtn, activeSkill_2, ActiveSkillCoolTime_2);
        }

        public void TurnEnd()
        {
            ActiveSkillCoolTime_1 = (ActiveSkillCoolTime_1 > 0) ? ActiveSkillCoolTime_1-- : ActiveSkillCoolTime_1;
            ActiveSkillCoolTime_2 = (ActiveSkillCoolTime_2 > 0) ? ActiveSkillCoolTime_2-- : ActiveSkillCoolTime_2;
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void BasicAttack()
        {
            BattleManager.ActionSystem.SetActiveSkill(basicAttackSkill);
            actionBtn.interactable = true;
            actionLevel = 1;
            selectActiveSkill = basicAttackSkill;

        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
            actionBtn.interactable = true;
            actionLevel = activeSkill_1_Level;
            selectActiveSkill = activeSkill_1;
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
            actionBtn.interactable = true;
            actionLevel = activeSkill_2_Level;
            selectActiveSkill = activeSkill_2;
        }

        public void Action()
        {
            if (selectActiveSkill == basicAttackSkill)
            {
                BattleManager.ManaSystem.AddMana(1);
                ActiveSkillCoolTime_1 = (ActiveSkillCoolTime_1 > 0) ? ActiveSkillCoolTime_1-- : ActiveSkillCoolTime_1;
                ActiveSkillCoolTime_2 = (ActiveSkillCoolTime_2 > 0) ? ActiveSkillCoolTime_2-- : ActiveSkillCoolTime_2;
            }
            else
            {
                BattleManager.ManaSystem.UseMana(selectActiveSkill.GetData.consumeManaValue);
                if (selectActiveSkill == activeSkill_1)
                {
                    ActiveSkillCoolTime_1 = selectActiveSkill.GetData.skillCoolTime;
                    ActiveSkillCoolTime_2 = (ActiveSkillCoolTime_2 > 0) ? ActiveSkillCoolTime_2-- : ActiveSkillCoolTime_2;
                }
                else if(selectActiveSkill == activeSkill_2)
                {
                    ActiveSkillCoolTime_2 = selectActiveSkill.GetData.skillCoolTime;
                    ActiveSkillCoolTime_1 = (ActiveSkillCoolTime_1 > 0) ? ActiveSkillCoolTime_1-- : ActiveSkillCoolTime_1;
                }
            }

            foreach (var unit in BattleManager.ActionSystem.SelectedUnits)
            {
                selectActiveSkill.Action(this, new SkillActionEventArgs(actionLevel, this.unit, unit));
            }

            TurnEnd();
        }

        private void SetActiveBtn(Button activeBtn, ActiveSkill skill, int skillCoolTime)
        {
            if (skill == null) return;

            activeBtn.interactable = BattleManager.ManaSystem.canUseMana(skill.GetData.consumeManaValue) || skillCoolTime == 0;
        }

        public void ShowSkillDesc()
        {
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        public void HideSkillDesc()
        {
            this.battleSkillDescUI.gameObject.SetActive(false);
        }
    }
}