using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestModule : Module, IStackable, IStatChangeable
    {
        public int StackCount => throw new System.NotImplementedException();

        public bool IsStackBuff => throw new System.NotImplementedException();

        public bool IsStackOverlap => throw new System.NotImplementedException();

        public bool IsStackSum => throw new System.NotImplementedException();

        public EquipmentOptionStat EquipmentOptionStat => throw new System.NotImplementedException();

        public float changeValue => throw new System.NotImplementedException();

        public override void Action(BattleUnit unit)
        {
        }
    }
}