using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class AISystem : MonoBehaviour
    {
        public bool isAI;

        private float timer = 0f;
        private float turnEndTime = 1f;

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

                if (battleUnit.Unit.activeSkill_2.GetData.activeSkillType == ActiveSkillType.Firstpriority)
                {
                    if (battleUnit.canActiveSkillCool(battleUnit.Unit.activeSkill_2))
                    {
                        BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.activeSkill_2);
                        battleUnit.UseActiveSkill(battleUnit.Unit.activeSkill_2, battleUnit.Unit.activeSkillLevel_2, ref battleUnit.activeSkill_2_CoolTime);
                        BattleManager.TurnBaseSystem.TurnEnd();
                        return;
                    }
                }

                if (battleUnit.Unit.activeSkill_1.GetData.activeSkillType == ActiveSkillType.Firstpriority)
                {
                    if (battleUnit.canActiveSkillCool(battleUnit.Unit.activeSkill_1))
                    {

                    }
                }

                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
                battleUnit.BasicAttack();
                BattleManager.TurnBaseSystem.TurnEnd();
            }
        }
    }
}