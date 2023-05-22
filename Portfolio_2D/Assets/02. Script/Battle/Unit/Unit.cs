using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class ChangeCurrnetHPEventArgs : EventArgs
    {
        public float currentHP;

        public ChangeCurrnetHPEventArgs(float currentHP) => this.currentHP = currentHP;
    }

    public class Unit : MonoBehaviour
    {
        public UnitType unitType;

        [Header("UnitAttribute")]
        [SerializeField] private float maxHP = 100;
        [SerializeField] private float currentHP = 100;
        [SerializeField] private float attackPoint = 10f;
        [SerializeField] private float speed = 100f;
        [SerializeField] private float defencePoint = 0f;
        [SerializeField] private float criticalPoint = 0f;
        [SerializeField] private float criticalDamage = 0f;
        [SerializeField] private float effectTarget = 0f;
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
        public float MaxHP { get => maxHP; set => maxHP = value; }
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

        public float AttackPoint { get => attackPoint; set => attackPoint = value; }
        public float Speed { get => speed; set => speed = value; }
        public float DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float CriticalPoint { get => criticalPoint; set => criticalPoint = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float EffectTarget { get => effectTarget; set => effectTarget = value; }
        public float EffectResistance { get => effectResistance; set => effectResistance = value; }

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
        public void BasicAttack(Unit targetUnit)
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