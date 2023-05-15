using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;

        public float Speed { get => speed; set => speed = value; }
    }
}