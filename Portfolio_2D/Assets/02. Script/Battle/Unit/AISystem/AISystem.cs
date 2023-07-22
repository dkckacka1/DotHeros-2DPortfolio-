using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ORDER : #20) 자동 전투 시스템 구현
 * 자동 전투 혹은 적이 사용할 AI 시스템
 */

namespace Portfolio.Battle
{
    public class AISystem : MonoBehaviour
    {
        private BattleUnit battleUnit;                      // 자기 전투 유닛
        private UnitTurnBase unitTurnBase;                  // 자신의 유닛 턴 시스템

        public bool isAI;                                   // 자동 전투 상태인지

        [SerializeField] private float turnEndTime = 1f;    // 자신이 턴이 돌아왔을때 스킬을 바로 사용하지 않도록 텀을 둘 시간
        private float timer = 0f;                           // 텀 타이머

        private ActiveSkill activeSkill_1;                  // 자신의 액티브 스킬 1
        private ActiveSkill activeSkill_2;                  // 자신의 액티브 스킬 2


        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            unitTurnBase = GetComponent<UnitTurnBase>();
        }

        // 전투 유닛의 액티브 스킬 참조
        public void SetActiveSkill(Unit unit)
        {
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
        }

        private void Update()
        {
            // 이미 스킬 사용중이면 리턴
            if (battleUnit.IsSkill) return;

            if (BattleManager.Instance.BattleState == eBattleState.Play && battleUnit.IsTurn && isAI)
                // 현재 전투 중이며, 자신의 턴이며, 자동 전투 상태일시
            {
                if (timer <= turnEndTime)
                    // 바로 스킬을 사용하지 않도록 텀을 둔다.
                {
                    timer += Time.deltaTime;
                    return;
                }

                timer = 0;


                if (battleUnit.Unit == null)
                    // 자신의 유닛이 null 값이면 예외처리
                {
                    Debug.LogError($"AI 시스템 : BattleUnit.Unit = null");
                    return;
                }

                if (TryUseActiveSkill(eActiveSkillType.Firstpriority))
                    // 최우선도 스킬을 사용할 수 있다면 사용
                {
                    return;
                }

                // 생존한 동맹 참조
                IEnumerable<BattleUnit> allyList = BattleManager.Instance.GetUnitList(battleUnit => battleUnit.IsAlly(this.battleUnit));
                // 생존한 적 참조
                IEnumerable<BattleUnit> enemyList = BattleManager.Instance.GetUnitList(battleUnit => !battleUnit.IsAlly(this.battleUnit));

                if (CheckConditionCount(allyList, 1, IsUnitDamaged))
                // 1명 이상의 아군이 체력이 감소된 상태
                {
                    if (TryUseActiveSkill(eActiveSkillType.MultipleHeal))
                    // 광역힐을 사용할 수 있다면 사용
                    {
                        return;
                    }

                    if (TryUseActiveSkill(eActiveSkillType.SingleHeal))
                    // 단일힐을 사용할 수 있다면 사용
                    {
                        return;
                    }
                }

                if (CheckConditionCount(enemyList, 3))
                // 적군이 3명이상일 경우
                {
                    if (TryUseActiveSkill(eActiveSkillType.MultipleAttack))
                    // 광역 공격을 사용할 수 있다면 사용
                    {
                        return;
                    }
                }

                if (TryUseActiveSkill(eActiveSkillType.SingleAttack))
                // 단일 공격을 사용할 수 있다면 사용
                {
                    return;
                }

                // 기본 공격으로 세팅
                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
                if (BattleManager.ActionSystem.SelectUnitCount != 0)
                    // 공격할 타겟이 있다면
                {
                    // 기본 공격 스킬 사용
                    battleUnit.UseSkill(eUnitSkillType.BaseAttack);
                }
                else
                {
                    // 공격할 타겟이 없다면 마나 1 회복하고 턴 스킵
                    BattleManager.ManaSystem.AddMana(1);
                    BattleManager.TurnBaseSystem.TurnEnd();
                }
            }
        }


        // 액티브 스킬 타입에 따라 스킬을 사용할 수 있는지 여부를 판단하고 사용(액티브 스킬 2를 우선적으로 사용한다.)
        private bool TryUseActiveSkill(eActiveSkillType type)
        {
            if (activeSkill_2 != null && activeSkill_2.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_2))
                    // 액티브 스킬을 사용할 수 있는 상태인지 판별(마나량, 쿨타임)
                {
                    // 전투 타겟을 설정한다.
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
                    if (BattleManager.ActionSystem.SelectUnitCount != 0)
                        // 타겟이 존재하면 스킬 사용
                    {
                        battleUnit.UseSkill(eUnitSkillType.ActiveSkill_2);
                        return true;
                    }
                }
            }

            if (activeSkill_1 != null && activeSkill_1.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_1))
                    // 액티브 스킬을 사용할 수 있는 상태인지 판별(마나량, 쿨타임)
                {
                    // 전투 타겟을 설정한다.
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
                    if (BattleManager.ActionSystem.SelectUnitCount != 0)
                        // 타겟이 존재하면 스킬 사용
                    {
                        battleUnit.UseSkill(eUnitSkillType.ActiveSkill_1);
                        return true;
                    }
                }
            }

            // 다음 스킬을 확인한다.
            return false;
        }

        //===========================================================
        // ConditionCheck
        //===========================================================
        // Where절을 통한 Count()가 count와 같거나 크면 true
        private bool CheckConditionCount(IEnumerable<BattleUnit> battleUnits, int count, Func<BattleUnit, bool> WhereFunc = null)
        {
            int whereCount = 0;
            if (WhereFunc != null)
            {
                whereCount = battleUnits.Where(WhereFunc).Count();
            }
            else
            {
                whereCount = battleUnits.Count();
            }

            return whereCount >= count;
        }

        // 유닛이 데미지를 입었는지 체크한다.
        private bool IsUnitDamaged(BattleUnit unit)
        {
            return unit.CurrentHP != unit.MaxHP;
        }
    }
}