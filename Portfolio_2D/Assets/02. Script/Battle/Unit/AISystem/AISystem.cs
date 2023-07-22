using Portfolio.skill;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ORDER : #20) �ڵ� ���� �ý��� ����
 * �ڵ� ���� Ȥ�� ���� ����� AI �ý���
 */

namespace Portfolio.Battle
{
    public class AISystem : MonoBehaviour
    {
        private BattleUnit battleUnit;                      // �ڱ� ���� ����
        private UnitTurnBase unitTurnBase;                  // �ڽ��� ���� �� �ý���

        public bool isAI;                                   // �ڵ� ���� ��������

        [SerializeField] private float turnEndTime = 1f;    // �ڽ��� ���� ���ƿ����� ��ų�� �ٷ� ������� �ʵ��� ���� �� �ð�
        private float timer = 0f;                           // �� Ÿ�̸�

        private ActiveSkill activeSkill_1;                  // �ڽ��� ��Ƽ�� ��ų 1
        private ActiveSkill activeSkill_2;                  // �ڽ��� ��Ƽ�� ��ų 2


        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            unitTurnBase = GetComponent<UnitTurnBase>();
        }

        // ���� ������ ��Ƽ�� ��ų ����
        public void SetActiveSkill(Unit unit)
        {
            activeSkill_1 = unit.activeSkill_1;
            activeSkill_2 = unit.activeSkill_2;
        }

        private void Update()
        {
            // �̹� ��ų ������̸� ����
            if (battleUnit.IsSkill) return;

            if (BattleManager.Instance.BattleState == eBattleState.Play && battleUnit.IsTurn && isAI)
                // ���� ���� ���̸�, �ڽ��� ���̸�, �ڵ� ���� �����Ͻ�
            {
                if (timer <= turnEndTime)
                    // �ٷ� ��ų�� ������� �ʵ��� ���� �д�.
                {
                    timer += Time.deltaTime;
                    return;
                }

                timer = 0;


                if (battleUnit.Unit == null)
                    // �ڽ��� ������ null ���̸� ����ó��
                {
                    Debug.LogError($"AI �ý��� : BattleUnit.Unit = null");
                    return;
                }

                if (TryUseActiveSkill(eActiveSkillType.Firstpriority))
                    // �ֿ켱�� ��ų�� ����� �� �ִٸ� ���
                {
                    return;
                }

                // ������ ���� ����
                IEnumerable<BattleUnit> allyList = BattleManager.Instance.GetUnitList(battleUnit => battleUnit.IsAlly(this.battleUnit));
                // ������ �� ����
                IEnumerable<BattleUnit> enemyList = BattleManager.Instance.GetUnitList(battleUnit => !battleUnit.IsAlly(this.battleUnit));

                if (CheckConditionCount(allyList, 1, IsUnitDamaged))
                // 1�� �̻��� �Ʊ��� ü���� ���ҵ� ����
                {
                    if (TryUseActiveSkill(eActiveSkillType.MultipleHeal))
                    // �������� ����� �� �ִٸ� ���
                    {
                        return;
                    }

                    if (TryUseActiveSkill(eActiveSkillType.SingleHeal))
                    // �������� ����� �� �ִٸ� ���
                    {
                        return;
                    }
                }

                if (CheckConditionCount(enemyList, 3))
                // ������ 3���̻��� ���
                {
                    if (TryUseActiveSkill(eActiveSkillType.MultipleAttack))
                    // ���� ������ ����� �� �ִٸ� ���
                    {
                        return;
                    }
                }

                if (TryUseActiveSkill(eActiveSkillType.SingleAttack))
                // ���� ������ ����� �� �ִٸ� ���
                {
                    return;
                }

                // �⺻ �������� ����
                BattleManager.ActionSystem.SetActiveSkill(battleUnit.Unit.basicAttackSkill);
                if (BattleManager.ActionSystem.SelectUnitCount != 0)
                    // ������ Ÿ���� �ִٸ�
                {
                    // �⺻ ���� ��ų ���
                    battleUnit.UseSkill(eUnitSkillType.BaseAttack);
                }
                else
                {
                    // ������ Ÿ���� ���ٸ� ���� 1 ȸ���ϰ� �� ��ŵ
                    BattleManager.ManaSystem.AddMana(1);
                    BattleManager.TurnBaseSystem.TurnEnd();
                }
            }
        }


        // ��Ƽ�� ��ų Ÿ�Կ� ���� ��ų�� ����� �� �ִ��� ���θ� �Ǵ��ϰ� ���(��Ƽ�� ��ų 2�� �켱������ ����Ѵ�.)
        private bool TryUseActiveSkill(eActiveSkillType type)
        {
            if (activeSkill_2 != null && activeSkill_2.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_2))
                    // ��Ƽ�� ��ų�� ����� �� �ִ� �������� �Ǻ�(������, ��Ÿ��)
                {
                    // ���� Ÿ���� �����Ѵ�.
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_2);
                    if (BattleManager.ActionSystem.SelectUnitCount != 0)
                        // Ÿ���� �����ϸ� ��ų ���
                    {
                        battleUnit.UseSkill(eUnitSkillType.ActiveSkill_2);
                        return true;
                    }
                }
            }

            if (activeSkill_1 != null && activeSkill_1.GetData.activeSkillType == type)
            {
                if (this.battleUnit.CanActiveSkill(activeSkill_1))
                    // ��Ƽ�� ��ų�� ����� �� �ִ� �������� �Ǻ�(������, ��Ÿ��)
                {
                    // ���� Ÿ���� �����Ѵ�.
                    BattleManager.ActionSystem.SetActiveSkill(activeSkill_1);
                    if (BattleManager.ActionSystem.SelectUnitCount != 0)
                        // Ÿ���� �����ϸ� ��ų ���
                    {
                        battleUnit.UseSkill(eUnitSkillType.ActiveSkill_1);
                        return true;
                    }
                }
            }

            // ���� ��ų�� Ȯ���Ѵ�.
            return false;
        }

        //===========================================================
        // ConditionCheck
        //===========================================================
        // Where���� ���� Count()�� count�� ���ų� ũ�� true
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

        // ������ �������� �Ծ����� üũ�Ѵ�.
        private bool IsUnitDamaged(BattleUnit unit)
        {
            return unit.CurrentHP != unit.MaxHP;
        }
    }
}