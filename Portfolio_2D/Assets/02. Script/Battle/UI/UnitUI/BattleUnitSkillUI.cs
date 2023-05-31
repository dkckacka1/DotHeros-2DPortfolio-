using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Battle
{
    public class BattleUnitSkillUI : MonoBehaviour
    {
        private BattleUnit battleUnit;
        private BattleSkillDescUI battleSkillDescUI;

        [SerializeField] Button turnEndBtn;
        [SerializeField] Button BasicAttackBtn;
        [SerializeField] Button activeSkill_1_ActionBtn;
        [SerializeField] Button activeSkill_2_ActionBtn;
        [SerializeField] Button actionBtn;
        [SerializeField] TextMeshProUGUI skillCoolTime_1_Text;
        [SerializeField] TextMeshProUGUI skillCoolTime_2_Text;

        int actionLevel = 0;

        private ActiveSkill selectActiveSkill;

        //private event EventHandler<SkillActionEventArgs> OnActionBtnEvent;

        public void SetUnit(BattleUnit battleUnit)
        {
            this.battleUnit = battleUnit;

            if (battleUnit.Unit.activeSkill_1 == null)
            {
                activeSkill_1_ActionBtn.gameObject.SetActive(false);
            }


            if (battleUnit.Unit.activeSkill_2 == null)
            {
                activeSkill_2_ActionBtn.gameObject.SetActive(false);
            }
        }

        public void SetBattleSkillDescUI(BattleSkillDescUI battleSkillDescUI)
        {
            this.battleSkillDescUI = battleSkillDescUI;
        }

        public void ShowSkillUI() => this.gameObject.SetActive(true);
        public void HideSkillUI() => this.gameObject.SetActive(false);

        //===========================================================
        // ButtonPlugin
        //===========================================================

        private void Start()
        {
            InitSkillUI();

        }

        public void InitSkillUI()
        {
            selectActiveSkill = null;
            actionBtn.interactable = false;
        }

        public void ResetSkillUI(BattleUnit unit)
        {
            InitSkillUI();
            if (battleUnit.Unit.activeSkill_1 != null)
            {
                //Debug.Log($"{battleUnit.name}의 {battleUnit.Unit.activeSkill_1.GetData.skillName}의 스킬 쿨타임은 {battleUnit.activeSkill_1_CoolTime} 입니다.");
                //Debug.Log(battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1, battleUnit.activeSkill_1_CoolTime));
                activeSkill_1_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1);
                skillCoolTime_1_Text.gameObject.SetActive(battleUnit.activeSkill_1_CoolTime != 0);
                skillCoolTime_1_Text.text = battleUnit.activeSkill_1_CoolTime.ToString();
            }

            if (battleUnit.Unit.activeSkill_2 != null)
            {
                activeSkill_2_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_2);
                skillCoolTime_2_Text.gameObject.SetActive(battleUnit.activeSkill_2_CoolTime != 0);
                skillCoolTime_2_Text.text = battleUnit.activeSkill_2_CoolTime.ToString();
            }
        }

        public void TurnEnd()
        {
            BattleManager.ManaSystem.AddMana(1);
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void BasicAttack()
        {
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
            actionBtn.interactable = true;
            actionLevel = 1;
            selectActiveSkill = battleUnit.Unit.basicAttackSkill;

        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_1);
            actionBtn.interactable = true;
            actionLevel = battleUnit.Unit.activeSkillLevel_1;
            selectActiveSkill = battleUnit.Unit.activeSkill_1;
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_2);
            actionBtn.interactable = true;
            actionLevel = battleUnit.Unit.activeSkillLevel_2;
            selectActiveSkill = battleUnit.Unit.activeSkill_2;
        }

        public void Action()
        {
            if (BattleManager.ActionSystem.SelectedUnits.Count == 0)
                // 선택한 유닛이 없으면 리턴
            {
                return;
            }

            if (selectActiveSkill == battleUnit.Unit.basicAttackSkill)
            {
                battleUnit.BasicAttack();
            }
            else
            {
                if (selectActiveSkill == battleUnit.Unit.activeSkill_1)
                {
                    battleUnit.UseActiveSkill(battleUnit.Unit.activeSkill_1);

                }
                else if(selectActiveSkill == battleUnit.Unit.activeSkill_2)
                {
                    battleUnit.UseActiveSkill(battleUnit.Unit.activeSkill_2);
                }
            }

            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void ShowBasicAttackSkill()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.basicAttackSkill);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        public void ShowDescActiveSkill_1()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_1, battleUnit.Unit.activeSkillLevel_1);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        public void ShowDescActiveSkill_2()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_2, battleUnit.Unit.activeSkillLevel_2);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        public void HideSkillDesc()
        {
            this.battleSkillDescUI.gameObject.SetActive(false);
        }
    }
}