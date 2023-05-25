using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class ChangeCurrnetHPEventArgs : EventArgs
    {
        public float currentHP;

        public ChangeCurrnetHPEventArgs(float currentHP) => this.currentHP = currentHP;
    }

    public abstract class BattleUnit : MonoBehaviour
    {
        [SerializeField] UnitType unitType;

        [Header("UnitTurnSystem")]
        private bool isTrun;

        [Header("UnitAttribute")]
        [SerializeField] private float maxHP = 100;
        [SerializeField] private float currentHP = 100;
        [SerializeField] private float attackPoint = 10f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float defencePoint = 0f;
        [SerializeField] private float criticalPoint = 0f;
        [SerializeField] private float criticalDamage = 0f;
        [SerializeField] private float effectHit = 0f;
        [SerializeField] private float effectResistance = 0f;

        private UnitUI unitUI;
        // TODO
        //private List<SkillStack> skillStackList = new List<SkillStack>();

        //===========================================================
        // Event
        //===========================================================
        public event EventHandler OnStartBattleEvent; // 전투 시작시 호출될 이벤트
        public event EventHandler OnStartCurrentTurnEvent; // 자신의 턴이 왔다면 호출될 이벤트
        public event EventHandler OnEndCurrentTurnEvent; // 자신의 턴이 종료될때 호출될 이벤트
        public event EventHandler OnChangedCurrentHPEvent; // 자신의 체력이 변화될때 호출될 이벤트
        public event EventHandler OnAttackEvent; // 자신이 공격(기본공격, 스킬공격)시 호출될 이벤트
        public event EventHandler OnTakeAttackEvent; // 자신이 공격받았을때 호출될 이벤트
        public event EventHandler OnDeadEvent; // 자신이 죽었을때 호출될 이벤트

        //===========================================================
        // Property
        //===========================================================
        public UnitType UnitType { get => unitType; set => unitType = value; }
        public float MaxHP { get => maxHP; set => maxHP = value; }
        public float AttackPoint { get => attackPoint; set => attackPoint = value; }
        public float Speed { get => speed; set => speed = value; }
        public float DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float CriticalPoint { get => criticalPoint; set => criticalPoint = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float EffectTarget { get => effectHit; set => effectHit = value; }
        public float EffectResistance { get => effectResistance; set => effectResistance = value; }
        public float CurrentHP
        {
            get => currentHP;

            set
            {
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
                OnChangedCurrentHPEvent?.Invoke(this, new ChangeCurrnetHPEventArgs(currentHP));

                if (currentHP <= 0)
                {
                    Dead();
                }
            }
        }

        //===========================================================
        // UnityEvent
        //===========================================================
        private void Awake()
        {
            unitUI = GetComponent<UnitUI>();
            unitUI.SetUnit(this);

            OnChangedCurrentHPEvent += unitUI.BattleUnit_OnCurrentHPChangedEvent;
        }

        //===========================================================
        // CreateUnit
        //===========================================================

        public virtual void SetUnit(Unit unit, UnitSkillUI skillUI)
        {
            this.maxHP = unit.Data.maxHP;
            this.currentHP = unit.Data.maxHP;
            this.attackPoint = unit.Data.attackPoint;
            this.speed = unit.Data.speed;
            this.defencePoint = unit.Data.defencePoint;
            this.criticalPoint = unit.Data.criticalPoint;
            this.criticalDamage = unit.Data.criticalDamage;
            this.effectHit = unit.Data.effectHit;
            this.effectResistance = unit.Data.effectResistance;

            //unit.passiveSkill_1?.SetCurrentTurnUnit(this);
            //unit.passiveSkill_2?.SetCurrentTurnUnit(this);

            //unit.passiveSkill_1?.TakeAction(this, new SkillActionEventArgs(this, unit.passiveSkillLevel_1));
            //unit.passiveSkill_2?.TakeAction(this, new SkillActionEventArgs(this, unit.passiveSkillLevel_2));

            //skillUI.SetSkill(unit.basicAttackSkill, unit.activeSkill_1, unit.activeSkill_2, unit.activeSkillLevel_1, unit.activeSkillLevel_2);
        }

        //===========================================================
        // TurnSystem & ActionSystem
        //===========================================================
        public virtual void UnitTurnBase_OnTurnStartEvent(object sender, EventArgs e)
        {
            unitUI.SetCurrentTurnUI(true);
            ProcessStackSkill();

            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public virtual void UnitTurnBase_OnTurnEndEvent(object sender, EventArgs e)
        {
            unitUI.SetCurrentTurnUI(false);

            OnEndCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Select()
        {
            unitUI.SetTargetedUI(true);
        }

        public void UnSelect()
        {
            unitUI.SetTargetedUI(false);
        }

        //===========================================================
        // BattleMethod
        //===========================================================
        public void BasicAttack(BattleUnit targetUnit)
        {
            targetUnit.TakeDamage(attackPoint);
        }

        public void TakeDamage(float DamagePoint)
        {
            CurrentHP -= DamagePoint;
        }

        private void Dead()
        {
        }

        private void ProcessStackSkill()
        {
            //foreach (var skillStack in skillStackList)
            //{
            //    skillStack.ProcessStack();
            //}
        }

        public void TakeStackSkill(int skillID, int stackCount, EventHandler OnSkillAction)
        {
            //SkillStack skill = skillStackList.Find((skill) => skill.SkillID == skillID);

            //if (skill == null)
            //{
            //    skill = new SkillStack(skillID, stackCount, OnSkillAction, OnStackEndAction);
            //}
            //else
            //{
            //    skill.StackCount = stackCount;
            //}
        }

        private void OnStackEndAction(object sender, EventArgs e)
        {

        }
    }

}