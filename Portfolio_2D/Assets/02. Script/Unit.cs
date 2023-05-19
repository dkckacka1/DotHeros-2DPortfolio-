using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class Unit : MonoBehaviour
    {
        public UnitType unitType;

        [SerializeField] private int maxHP = 100;
        [SerializeField] private int currentHP = 100;
        [SerializeField] private float speed = 100f;

        public float Speed { get => speed; set => speed = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }
        public int CurrentHP 
        { 
            get => currentHP;

            set
            {
                value = Mathf.Clamp(value, 0, MaxHP);
                currentHP = value;
            }
        }

        public UnitUI unitUI;

        private void Awake()
        {
            unitUI = GetComponent<UnitUI>();
        }

        public void TurnEnd()
        {
        }

        public void OnSelectedTurnEvent()
        {
            switch (unitType)
            {
                case UnitType.Player:
                    Debug.Log("아군 턴");
                    break;
                case UnitType.Enemy:
                    Debug.Log("적군 턴");
                    break;
            }
        }
    }
}