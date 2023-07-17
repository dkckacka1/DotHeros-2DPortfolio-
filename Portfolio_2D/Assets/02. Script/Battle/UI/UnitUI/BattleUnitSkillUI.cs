using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/*
 *  플레이어 유닛의 스킬창을 표시해주는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitSkillUI : MonoBehaviour
    {
        private BattleUnit battleUnit;                          // 전투 유닛
        private BattleSkillDescUI battleSkillDescUI;            // 스킬 설명 UI

        [SerializeField] Button turnEndBtn;                     // 턴 종료 버튼
        [SerializeField] Button actionBtn;                      // 스킬 수행 버튼
        [SerializeField] GameObject actionObject;               // 액션 오브젝트
        [SerializeField] GameObject lockObject;                 // 잠김 오브젝트

        [Header("Skill_1")]
        [SerializeField] Button activeSkill_1_ActionBtn;        // 액티브 스킬 1 선택 버튼
        [SerializeField] Image activeSkill_1_Image;             // 액티브 스킬 1 이미지
        [SerializeField] TextMeshProUGUI skillCoolTime_1_Text;  // 액티브 스킬 1 쿨타임 텍스트

        [Header("Skill_2")]
        [SerializeField] Button activeSkill_2_ActionBtn;        // 액티브 스킬 2 선택 버튼
        [SerializeField] Image activeSkill_2_Image;             // 액티브 스킬 2 이미지
        [SerializeField] TextMeshProUGUI skillCoolTime_2_Text;  // 액티브 스킬 2 쿨타임 텍스트

        [Header("BaseAttack")]
        [SerializeField] Button BasicAttackBtn;                 // 기본 공격 스킬 선택 버튼
        [SerializeField] Image basicAttackSkillImage;           // 기본 공격 스킬 이미지

        int actionLevel = 0;                                    // 스킬 레벨
        private ActiveSkill selectActiveSkill;                  // 선택한 액티브 스킬
        private Button selectBtn;                                // 선택한 버튼

        // 현재 스킬을 사용할 수 있는 대상이 있는지 확인 여부
        bool IsAction
        {
            set
            {
                // 사용할 수 있다면 액션 오브젝트와 액션 버튼 상호작용 활성화
                actionObject.SetActive(value);
                actionBtn.interactable = value;

                lockObject.SetActive(!value);
            }
        }

        // 전투 유닛 설정
        public void SetUnit(BattleUnit battleUnit)
        {
            this.battleUnit = battleUnit;

            // 기본 공격 스킬 이미지 세팅
            basicAttackSkillImage.sprite = battleUnit.Unit.basicAttackSkill.skillSprite;

            // 액티브 스킬 1 세팅
            if (battleUnit.Unit.activeSkill_1 == null)
            {
                activeSkill_1_ActionBtn.gameObject.SetActive(false);
            }
            else
            {
                activeSkill_1_Image.sprite = battleUnit.Unit.activeSkill_1.skillSprite;
            }


            // 액티브 스킬 2 세팅
            if (battleUnit.Unit.activeSkill_2 == null)
            {
                activeSkill_2_ActionBtn.gameObject.SetActive(false);
            }
            else
            {
                activeSkill_2_Image.sprite = battleUnit.Unit.activeSkill_2.skillSprite;
            }
        }

        // 액티브 스킬 설명 세팅
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

        // 스킬 UI 초기화
        public void ResetSkillActionBtn()
        {
            // 선택한 스킬 초기화
            selectActiveSkill = null;
            actionObject.SetActive(true);
            lockObject.SetActive(false);
            actionBtn.interactable = false;
        }

        // 턴이 돌아왔을 때 스킬 UI를 초기화
        public void ResetSkillUI(BattleUnit unit)
        {
            ResetSkillActionBtn();
            if (battleUnit.Unit.activeSkill_1 != null)
            // 액티브 스킬 1이 null 이아닐 경우
            {
                // 액티브 스킬 1의 쿨타임과 마나량을 확인하요 상호작용 on/off
                activeSkill_1_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_1);
                // 액티브 스킬 1의 쿨타임을 확인하여 쿨타임 텍스트 표시
                skillCoolTime_1_Text.gameObject.SetActive(battleUnit.ActiveSkill_1_CoolTime != 0);
                skillCoolTime_1_Text.text = battleUnit.ActiveSkill_1_CoolTime.ToString();
            }

            if (battleUnit.Unit.activeSkill_2 != null)
            // 액티브 스킬 2이 null 이아닐 경우
            {
                // 액티브 스킬 2의 쿨타임과 마나량을 확인하요 상호작용 on/off
                activeSkill_2_ActionBtn.interactable = battleUnit.CanActiveSkill(battleUnit.Unit.activeSkill_2);
                // 액티브 스킬 2의 쿨타임을 확인하여 쿨타임 텍스트 표시
                skillCoolTime_2_Text.gameObject.SetActive(battleUnit.ActiveSkill_2_CoolTime != 0);
                skillCoolTime_2_Text.text = battleUnit.ActiveSkill_2_CoolTime.ToString();
            }
        }

        // 턴 넘기기 버튼
        public void BTN_OnClick_TurnSkip()
        {
            // 마나 1 회복
            BattleManager.ManaSystem.AddMana(1);
            // 턴 종료
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        // 기본 공격 선택 버튼
        public void BTN_OnClick_BaseAttackChoice()
        {
            // 기본 공격 스킬의 타겟 유닛 설정
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
            // 타겟 유닛이 0명이면 액션 스킬 상호작용 불가
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0;
            // 기본공격의 스킬레벨은 1 고정
            actionLevel = 1;
            // 선택한 스킬은 기본 공격 스킬
            selectActiveSkill = battleUnit.Unit.basicAttackSkill;
        }

        // 액티브 스킬 1 선택 버튼
        public void BTN_OnClick_ActiveSkill_1_Choice()
        {
            // 액티브 스킬1의 타겟 유닛 설정
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_1);
            // 타겟 유닛이 0명이면 액션 스킬 상호작용 불가
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0;
            // 액티브 스킬 1의 스킬 레벨 참조
            actionLevel = battleUnit.Unit.ActiveSkillLevel_1;
            // 선택한 스킬은 액티브 스킬 1
            selectActiveSkill = battleUnit.Unit.activeSkill_1;
        }

        // 액티브 스킬 2 선택 버튼
        public void BTN_OnClick_ActiveSkill_2_Choice()
        {
            // 액티브 스킬2의 타겟 유닛 설정
            BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_2);
            // 타겟 유닛이 0명이면 액션 스킬 상호작용 불가
            IsAction = BattleManager.ActionSystem.SelectUnitCount != 0; ;
            // 액티브 스킬 2의 스킬 레벨 참조
            actionLevel = battleUnit.Unit.ActiveSkillLevel_2;
            // 선택한 스킬은 액티브 스킬 2
            selectActiveSkill = battleUnit.Unit.activeSkill_2;
        }

        // 스킬 수행 버튼
        public void BTN_OnClick_Action()
        {
            if (BattleManager.ActionSystem.SelectUnitCount == 0)
            // 선택한 유닛이 없으면 리턴
            {
                return;
            }

            if (selectActiveSkill == battleUnit.Unit.basicAttackSkill)
            // 선택된 스킬이 기본 공격 스킬이면
            {
                // 기본 공격 스킬 사용
                battleUnit.UseSkill(eUnitSkillType.BaseAttack);
            }
            else
            {
                if (selectActiveSkill == battleUnit.Unit.activeSkill_1)
                // 선택된 스킬이 액티브 스킬 1이면
                {
                    // 액티브 스킬 1 사용
                    battleUnit.UseSkill(eUnitSkillType.ActiveSkill_1);

                }
                else if (selectActiveSkill == battleUnit.Unit.activeSkill_2)
                // 선택된 스킬이 액티브 스킬 2이면
                {
                    // 액티브 스킬 2 사용
                    battleUnit.UseSkill(eUnitSkillType.ActiveSkill_2);
                }
            }

            // 선택한 버튼을 원래대로 되돌려줍니다.
            selectBtn.transform.localScale = Vector3.one;
            selectBtn = null;
        }

        // 유저가 버튼을 선택했는지 보여주기 위한 연출
        public void BTN_OnClick_BtnSelect(Button btn)
        {
            // 기존에 선택한 버튼이 자기 자신이었다면 리턴
            if (selectBtn == btn) return;

            if (selectBtn != null)
            {
                // 기 선택한 버튼을 원래대로 되돌려줍니다.
                selectBtn.transform.localScale = Vector3.one;
            }

            // 새로이 선택한 버튼을 크게 표시하여 유저가 알 수 있도록 합니다.
            selectBtn = btn;
            btn.transform.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.15f);
        }

        // 기본 공격 스킬 설명 표시
        public void TRIGGER_OnPointerEnter_ShowBasicAttackSkill()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.basicAttackSkill);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // 액티브 스킬 1 설명 표시
        public void TRIGGER_OnPointerEnter_ShowDescActiveSkill_1()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_1, battleUnit.Unit.ActiveSkillLevel_1);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // 액티브 스킬 2 설명 표시
        public void TRIGGER_OnPointerEnter_ShowDescActiveSkill_2()
        {
            battleSkillDescUI.ShowSkillDesc(battleUnit.Unit.activeSkill_2, battleUnit.Unit.ActiveSkillLevel_2);
            this.battleSkillDescUI.gameObject.SetActive(true);
        }

        // 스킬 설명창 숨김
        public void TRIGGER_OnPointerExit_HideSkillDesc()
        {
            this.battleSkillDescUI.gameObject.SetActive(false);
        }
    }
}