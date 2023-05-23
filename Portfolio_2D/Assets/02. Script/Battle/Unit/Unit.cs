using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public abstract class Unit 
    {
        private UnitData unitData;

        public UnitData UnitData { get => unitData; set => unitData = value; }
    }
}