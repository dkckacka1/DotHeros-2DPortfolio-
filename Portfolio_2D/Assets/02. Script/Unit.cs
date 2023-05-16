using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public abstract class Unit : MonoBehaviour
    {
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

        public abstract void OnSelectedTurnEvent();
    }
}