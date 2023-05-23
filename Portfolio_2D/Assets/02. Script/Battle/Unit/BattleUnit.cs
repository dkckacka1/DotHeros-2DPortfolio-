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

    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] UnitType unitType;

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

        [Header("UnitTurnSystem")]
        private bool isTrun;

        private UnitUI unitUI;

        //===========================================================
        // Event
        //===========================================================
        public event EventHandler OnStartCurrentTurnEvent; // 자신의 턴이 왔다면 호출될 이벤트
        public event EventHandler OnEndCurrentTurnEvent; // 자신의 턴이 종료될때 호출될 이벤트
        public event EventHandler OnChangedCurrentHPEvent; // 자신의 체력이 변화될때 호출될 이벤트

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

            OnChangedCurrentHPEvent += unitUI.Unit_OnCurrentHPChangedEvent;
        }

        //===========================================================
        // CreateUnit
        //===========================================================

        public virtual void SetUnit(Unit unit)
        {
            this.maxHP = unit.UnitData.maxHP;
            this.currentHP = unit.UnitData.maxHP;
            this.attackPoint = unit.UnitData.attackPoint;
            this.speed = unit.UnitData.speed;
            this.defencePoint = unit.UnitData.defencePoint;
            this.criticalPoint = unit.UnitData.criticalPoint;
            this.criticalDamage = unit.UnitData.criticalDamage;
            this.effectHit = unit.UnitData.effectHit;
            this.effectResistance = unit.UnitData.effectResistance;
        }

        //===========================================================
        // TurnSystem & ActionSystem
        //===========================================================
        public void StartCurrentTurn()
        {
            unitUI.SetCurrentTurnUI(true);

            OnStartCurrentTurnEvent?.Invoke(this, EventArgs.Empty);
        }

        public void EndCurrentTurn()
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
    }

}