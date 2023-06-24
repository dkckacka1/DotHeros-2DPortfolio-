using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Battle
{
    public class AISystem : MonoBehaviour
    {
        private BattleUnit battleUnit;
        private UnitTurnBase unitTurnBase;

        public bool isAI;

        private float timer = 0f;
        [SerializeField] private float turnEndTime = 1f;

        private ActiveSkill activeSkill_1;
        private ActiveSkill activeSkill_2;


        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            unitTurnBase = GetComponent<UnitTurnBase>();
        }

        private void Update()
        {
            if (battleUnit.isUseSkill) return;

            if (BattleManager.Instance.BattleState == BattleState.PLAY && battleUnit.IsTurn && isAI)
            {
                if (timer <= turnEndTime)
                {
                    timer += Time.deltaTime;
                    return;
                }

                timer = 0;

                if (battleUnit.Unit == null)
                {
                    Debug.Log(battleUnit.Unit);
                    return;
                }

                if (TryUseActiveSkill(ActiveSkillType.Firstpriority))
                {
                    //BattleManager.TurnBaseSystem.TurnEnd();
                    return;
                }

                IEnumerable<BattleUnit> allyList = BattleManager.Instance.GetUnitList(battleUnit => battleUnit.IsAlly(this.battleUnit));
                IEnumerable<BattleUnit> enemyList = BattleManager.Instance.GetUnitList(battleUnit => !battleUnit.IsAlly(this.battleUnit));

                //Debug.Log($"{this.gameObject.name} 의 아군은 {allyList.Count()}명");
                //Debug.Log($"{this.gameObject.name} 의 적군은 {enemyList.Count()}명");

                if (CheckConditionCount(allyList, 1, isUnitDamaged))
                    // 1명 이상의 아군이 체력이 감소된 상태
                {
                    if (TryUseActiveSkill(ActiveSkillType.MultipleHeal))
                        // 광역힐을 사용할 수 있다면 사용
                    {
                        //BattleManager.TurnBaseSystem.TurnEnd();
                        return;
                    }

                    if (TryUseActiveSkill(ActiveSkillType.SingleHeal, GetLowHealthUnit))
                        // 단일힐을 사용할 수 있다면 사용
                    {
                        //BattleManager.TurnBaseSystem.TurnEnd();
                        return;
                    }
                }

                if (CheckConditionCount(enemyList, 3))
                    // 적군이 3명이상일 경우
                {
                    if (TryUseActiveSkill(ActiveSkillType.MultipleAttack))
                    // 광역 공격을 사용할 수 있다면 사용
                    {
                        //BattleManager.TurnBaseSystem.TurnEnd();
                        return;
                    }
                }
                
                if (TryUseActiveSkill(ActiveSkillType.Singleattack, GetLowHealthUnit))
                    // 단일 공격을 사용할 수 있다면 사용
                {
                    //BattleManager.TurnBaseSystem.TurnEnd();
                    return;
                }



                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill, GetLowHealthUnit);
                battleUnit.UseSkill(UnitSkillType.BaseAttack);
                //BattleManager.TurnBaseSystem.TurnEnd();
            }
        }

        public void SetActiveSkill(Unit unit)
        {
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
        }

        private bool TryUseActiveSkill(ActiveSkillType type, Func<BattleUnit, int> orderby = null)
        {
            if (activeSkill_2.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_2))
                {
                    if (activeSkill_2.GetData.isAutoTarget || orderby == null)
                    {
                        BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
                    }
                    else
                    {
                        BattleManager.ActionSystem.SetActiveSkill(activeSkill_2, orderby);
                    }

                    battleUnit.UseSkill(UnitSkillType.ActiveSkill_2);
                    return true;
                }
            }

            if (activeSkill_1.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_1))
                {
                    if (activeSkill_1.GetData.isAutoTarget || orderby == null)
                    {
                        BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
                    }
                    else
                    {
                        BattleManager.ActionSystem.SetActiveSkill(activeSkill_1, orderby);
                    }
                    battleUnit.UseSkill(UnitSkillType.ActiveSkill_1);
                    return true;
                }
            }

            return false;
        }

        //===========================================================
        // ConditionCheck
        //===========================================================

        private bool CheckConditionCount(IEnumerable<BattleUnit> battleUnits, int count, Func<BattleUnit, bool> WhereFunc = null)
            // Where절을 통한 Count()가 count와 같거나 크면 true
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

        private bool isUnitDamaged(BattleUnit unit)
        {
            return unit.CurrentHP != unit.MaxHP;
        }

        //===========================================================
        // PriorityCheck
        //===========================================================
        private int GetLowHealthUnit(BattleUnit arg)
        {
            return (int)(arg.CurrentHP / arg.MaxHP * 100); // 소수점을 int형으로 맞추기 위해 * 100을 함
        }
    }
}