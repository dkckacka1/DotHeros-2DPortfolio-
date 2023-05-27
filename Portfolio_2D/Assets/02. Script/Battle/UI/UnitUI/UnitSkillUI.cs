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
                Debug.Log($"{battleUnit.name}�� {battleUnit.Unit.activeSkill_1.GetData.skillName}�� ��ų ��Ÿ���� {battleUnit.activeSkill_1_CoolTime} �Դϴ�.");
                Debug.Log(battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1, battleUnit.activeSkill_1_CoolTime));
                activeSkill_1_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1, battleUnit.activeSkill_1_CoolTime);
                skillCoolTime_1_Text.gameObject.SetActive(battleUnit.activeSkill_1_CoolTime != 0);
                skillCoolTime_1_Text.text = battleUnit.activeSkill_1_CoolTime.ToString();
            }

            if (battleUnit.Unit.activeSkill_2 != null)
            {
                activeSkill_2_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_2, battleUnit.activeSkill_2_CoolTime);
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
            if (selectActiveSkill == battleUnit.Unit.basicAttackSkill)
            {
                battleUnit.BasicAttack();
                BattleManager.ManaSystem.AddMana(1);
            }
            else
            {
                BattleManager.ManaSystem.UseMana(selectActiveSkill.GetData.consumeManaValue);
                if (selectActiveSkill == battleUnit.Unit.activeSkill_1)
                {
                    battleUnit.UseActiveSkill(battleUnit.Unit.activeSkill_1, battleUnit.Unit.activeSkillLevel_1, ref battleUnit.activeSkill_1_CoolTime);

                }
                else if(selectActiveSkill == battleUnit.Unit.activeSkill_2)
                {
                    battleUnit.UseActiveSkill(battleUnit.Unit.activeSkill_2, battleUnit.Unit.activeSkillLevel_2, ref battleUnit.activeSkill_2_CoolTime);
                }
            }

            BattleManager.TurnBaseSystem.TurnEnd();
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