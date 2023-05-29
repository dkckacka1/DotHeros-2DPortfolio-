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
        public bool isAI;

        private float timer = 0f;
        private float turnEndTime = 1f;

        private ActiveSkill activeSkill_1;
        private ActiveSkill activeSkill_2;

        private BattleUnit battleUnit;

        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
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
                }

                IEnumerable<BattleUnit> allyList = BattleManager.Instance.GetUnitList(battleUnit => battleUnit.IsAlly(this.battleUnit));
                IEnumerable<BattleUnit> enemyList = BattleManager.Instance.GetUnitList(battleUnit => !battleUnit.IsAlly(this.battleUnit));
                
                if (allyList.Where(isUnitDamaged).Count() >= 1)
                {

                }




                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
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
                if (this.battleUnit.CanActiveSkillCool(activeSkill_2))
                {
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
                    battleUnit.UseActiveSkill(activeSkill_2);
                    return true;
                }
            }

            if (activeSkill_1.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkillCool(activeSkill_1))
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
    }
}