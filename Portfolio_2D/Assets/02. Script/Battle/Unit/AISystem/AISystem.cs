using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio
{
    public class AISystem : MonoBehaviour
    {
        private BattleUnit battleUnit;
        private UnitTurnBase unitTurnBase;

        public bool isAI;

        private float timer = 0f;
        private float turnEndTime = 1f;

        private ActiveSkill activeSkill_1;
        private ActiveSkill activeSkill_2;


        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            unitTurnBase = GetComponent<UnitTurnBase>();
        }

        private void Update()
        {
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

                if (TryActiveSkill(ActiveSkillType.Firstpriority))
                {
                    BattleManager.TurnBaseSystem.TurnEnd();
                    return;
                }

                IEnumerable<BattleUnit> allyList = BattleManager.Instance.GetUnitList(battleUnit => battleUnit.IsAlly(this.battleUnit));
                IEnumerable<BattleUnit> enemyList = BattleManager.Instance.GetUnitList(battleUnit => !battleUnit.IsAlly(this.battleUnit));
                
                if (allyList.Where(isUnitDamaged).Count() >= 1)
                {
                    Debug.Log($"데미지를 입은 아군은 {allyList.Where(isUnitDamaged).Count()}명입니다.");
                }




                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill, GetLowHealthUnit);
                battleUnit.BasicAttack();
                BattleManager.TurnBaseSystem.TurnEnd();
            }
        }



        public void SetActiveSkill(Unit unit)
        {
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
        }

        private bool TryActiveSkill(ActiveSkillType type)
        {
            if (activeSkill_2.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_2))
                {
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
                    battleUnit.UseActiveSkill(activeSkill_2);
                    return true;
                }
            }

            if (activeSkill_1.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_1))
                {
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
                    battleUnit.UseActiveSkill(activeSkill_1);
                    return true;
                }
            }

            return false;
        }

        //===========================================================
        // ConditionCheck
        //===========================================================

        private bool isUnitDamaged(BattleUnit unit)
        {
            return (unit.CurrentHP / unit.MaxHP) <= 0.8f;
        }

        //===========================================================
        // PriorityCheck
        //===========================================================
        private int GetLowHealthUnit(BattleUnit arg)
        {
            return (int)arg.CurrentHP;
        }
    }
}