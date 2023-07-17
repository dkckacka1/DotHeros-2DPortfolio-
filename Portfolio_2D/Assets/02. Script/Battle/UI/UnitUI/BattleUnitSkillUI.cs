using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/*
 *  �÷��̾� ������ ��ųâ�� ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitSkillUI : MonoBehaviour
    {
        private BattleUnit battleUnit;                          // ���� ����
        private BattleSkillDescUI battleSkillDescUI;            // ��ų ���� UI

        [SerializeField] Button turnEndBtn;                     // �� ���� ��ư
        [SerializeField] Button actionBtn;                      // ��ų ���� ��ư
        [SerializeField] GameObject actionObject;               // �׼� ������Ʈ
        [SerializeField] GameObject lockObject;                 // ��� ������Ʈ

        [Header("Skill_1")]
        [SerializeField] Button activeSkill_1_ActionBtn;        // ��Ƽ�� ��ų 1 ���� ��ư
        [SerializeField] Image activeSkill_1_Image;             // ��Ƽ�� ��ų 1 �̹���
        [SerializeField] TextMeshProUGUI skillCoolTime_1_Text;  // ��Ƽ�� ��ų 1 ��Ÿ�� �ؽ�Ʈ

        [Header("Skill_2")]
        [SerializeField] Button activeSkill_2_ActionBtn;        // ��Ƽ�� ��ų 2 ���� ��ư
        [SerializeField] Image activeSkill_2_Image;             // ��Ƽ�� ��ų 2 �̹���
        [SerializeField] TextMeshProUGUI skillCoolTime_2_Text;  // ��Ƽ�� ��ų 2 ��Ÿ�� �ؽ�Ʈ

        [Header("BaseAttack")]
        [SerializeField] Button BasicAttackBtn;                 // �⺻ ���� ��ų ���� ��ư
        [SerializeField] Image basicAttackSkillImage;           // �⺻ ���� ��ų �̹���

        int actionLevel = 0;                                    // ��ų ����
        private ActiveSkill selectActiveSkill;                  // ������ ��Ƽ�� ��ų
        private Button selectBtn;                                // ������ ��ư

        // ���� ��ų�� ����� �� �ִ� ����� �ִ��� Ȯ�� ����
        bool IsAction
        {
            set
            {
                // ����� �� �ִٸ� �׼� ������Ʈ�� �׼� ��ư ��ȣ�ۿ� Ȱ��ȭ
                actionObject.SetActive(value);
                actionBtn.interactable = value;

                lockObject.SetActive(!value);
            }
        }

        // ���� ���� ����
        public void SetUnit(BattleUnit battleUnit)
        {
            this.battleUnit = battleUnit;

            // �⺻ ���� ��ų �̹��� ����
            basicAttackSkillImage.sprite = battleUnit.Unit.basicAttackSkill.skillSprite;

            // ��Ƽ�� ��ų 1 ����
            if (battleUnit.Unit.activeSkill_1 == null)
            {
                activeSkill_1_ActionBtn.gameObject.SetActive(false);
            }
            else
            {
                activeSkill_1_Image.sprite = battleUnit.Unit.activeSkill_1.skillSprite;
            }


            // ��Ƽ�� ��ų 2 ����
            if (battleUnit.Unit.activeSkill_2 == null)
            {
                activeSkill_2_ActionBtn.gameObject.SetActive(false);
            }
            else
            {
                activeSkill_2_Image.sprite = battleUnit.Unit.activeSkill_2.skillSprite;
            }
        }

        // ��Ƽ�� ��ų ���� ����
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
            ResetSkillActionBtn();

        }

        // ��ų UI �ʱ�ȭ
        public void ResetSkillActionBtn()
        {
            // ������ ��ų �ʱ�ȭ
            selectActiveSkill = null;
            actionObject.SetActive(true);
            lockObject.SetActive(false);
            actionBtn.interactable = false;
        }

        // ���� ���ƿ��� �� ��ų UI�� �ʱ�ȭ
        public void ResetSkillUI(BattleUnit unit)
        {
            ResetSkillActionBtn();
            if (battleUnit.Unit.activeSkill_1 != null)
            // ��Ƽ�� ��ų 1�� null �̾ƴ� ���
            {
                // ��Ƽ�� ��ų 1�� ��Ÿ�Ӱ� �������� Ȯ���Ͽ� ��ȣ�ۿ� on/off
                activeSkill_1_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1);
                // ��Ƽ�� ��ų 1�� ��Ÿ���� Ȯ���Ͽ� ��Ÿ�� �ؽ�Ʈ ǥ��
                skillCoolTime_1_Text.gameObject.SetActive(battleUnit.ActiveSkill_1_CoolTime != 0);
                skillCoolTime_1_Text.text = battleUnit.ActiveSkill_1_CoolTime.ToString();
            }

            if (battleUnit.Unit.activeSkill_2 != null)
            // ��Ƽ�� ��ų 2�� null �̾ƴ� ���
            {
                // ��Ƽ�� ��ų 2�� ��Ÿ�Ӱ� �������� Ȯ���Ͽ� ��ȣ�ۿ� on/off
                activeSkill_2_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_2);
                // ��Ƽ�� ��ų 2�� ��Ÿ���� Ȯ���Ͽ� ��Ÿ�� �ؽ�Ʈ ǥ��
                skillCoolTime_2_Text.gameObject.SetActive(battleUnit.ActiveSkill_2_CoolTime != 0);
                skillCoolTime_2_Text.text = battleUnit.ActiveSkill_2_CoolTime.ToString();
            }
        }

        // �� �ѱ�� ��ư
        public void BTN_OnClick_TurnSkip()
        {
            // ���� 1 ȸ��
            BattleManager.ManaSystem.AddMana(1);
            // �� ����
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        // �⺻ ���� ���� ��ư
        public void BTN_OnClick_BaseAttackChoice()
        {
            // �⺻ ���� ��ų�� Ÿ�� ���� ����
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
            // Ÿ�� ������ 0���̸� �׼� ��ų ��ȣ�ۿ� �Ұ�
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0;
            // �⺻������ ��ų������ 1 ����
            actionLevel = 1;
            // ������ ��ų�� �⺻ ���� ��ų
            selectActiveSkill = battleUnit.Unit.basicAttackSkill;
        }

        // ��Ƽ�� ��ų 1 ���� ��ư
        public void BTN_OnClick_ActiveSkill_1_Choice()
        {
            // ��Ƽ�� ��ų1�� Ÿ�� ���� ����
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_1);
            // Ÿ�� ������ 0���̸� �׼� ��ų ��ȣ�ۿ� �Ұ�
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0;
            // ��Ƽ�� ��ų 1�� ��ų ���� ����
            actionLevel = battleUnit.Unit.ActiveSkillLevel_1;
            // ������ ��ų�� ��Ƽ�� ��ų 1
            selectActiveSkill = battleUnit.Unit.activeSkill_1;
        }

        // ��Ƽ�� ��ų 2 ���� ��ư
        public void BTN_OnClick_ActiveSkill_2_Choice()
        {
            // ��Ƽ�� ��ų2�� Ÿ�� ���� ����
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_2);
            // Ÿ�� ������ 0���̸� �׼� ��ų ��ȣ�ۿ� �Ұ�
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0; ;
            // ��Ƽ�� ��ų 2�� ��ų ���� ����
            actionLevel = battleUnit.Unit.ActiveSkillLevel_2;
            // ������ ��ų�� ��Ƽ�� ��ų 2
            selectActiveSkill = battleUnit.Unit.activeSkill_2;
        }

        // ��ų ���� ��ư
        public void BTN_OnClick_Action()
        {
            if (BattleManager.ActionSystem.SelectUnitCount == 0)
            // ������ ������ ������ ����
            {
                return;
            }

            if (selectActiveSkill == battleUnit.Unit.basicAttackSkill)
            // ���õ� ��ų�� �⺻ ���� ��ų�̸�
            {
                // �⺻ ���� ��ų ���
                battleUnit.UseSkill(eUnitSkillType.BaseAttack);
            }
            else
            {
                if (selectActiveSkill == battleUnit.Unit.activeSkill_1)
                // ���õ� ��ų�� ��Ƽ�� ��ų 1�̸�
                {
                    // ��Ƽ�� ��ų 1 ���
                    battleUnit.UseSkill(eUnitSkillType.ActiveSkill_1);

                }
                else if (selectActiveSkill == battleUnit.Unit.activeSkill_2)
                // ���õ� ��ų�� ��Ƽ�� ��ų 2�̸�
                {
                    // ��Ƽ�� ��ų 2 ���
                    battleUnit.UseSkill(eUnitSkillType.ActiveSkill_2);
                }
            }

            // ������ ��ư�� ������� �ǵ����ݴϴ�.
            selectBtn.transform.localScale = Vector3.one;
            selectBtn = null;
        }

        // ������ ��ư�� �����ߴ��� �����ֱ� ���� ����
        public void BTN_OnClick_BtnSelect(Button btn)
        {
            // ������ ������ ��ư�� �ڱ� �ڽ��̾��ٸ� ����
            if (selectBtn == btn) return;

            if (selectBtn != null)
            {
                // �� ������ ��ư�� ������� �ǵ����ݴϴ�.
                selectBtn.transform.localScale = Vector3.one;
            }

            // ������ ������ ��ư�� ũ�� ǥ���Ͽ� ������ �� �� �ֵ��� �մϴ�.
            selectBtn = btn;
            btn.transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.15f);
        }

        // �⺻ ���� ��ų ���� ǥ��
        public void TRIGGER_OnPointerEnter_ShowBasicAttackSkill()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.basicAttackSkill);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // ��Ƽ�� ��ų 1 ���� ǥ��
        public void TRIGGER_OnPointerEnter_ShowDescActiveSkill_1()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_1, battleUnit.Unit.ActiveSkillLevel_1);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // ��Ƽ�� ��ų 2 ���� ǥ��
        public void TRIGGER_OnPointerEnter_ShowDescActiveSkill_2()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_2, battleUnit.Unit.ActiveSkillLevel_2);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // ��ų ����â ����
        public void TRIGGER_OnPointerExit_HideSkillDesc()
        {
            this.battleSkillDescUI.gameObject.SetActive(false);
        }
    }
}