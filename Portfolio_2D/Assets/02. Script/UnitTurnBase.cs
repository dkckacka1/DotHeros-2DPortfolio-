using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class UnitTurnBase
    {
        public Unit unit;
        public float currentTurnCount;
        public UnitSequenceUI unitSequenceUI;


        public UnitTurnBase(Unit unit, UnitSequenceUI unitSequenceUI)
        {
            this.unit = unit;
            this.unitSequenceUI = unitSequenceUI;
            currentTurnCount = 0f;
        }

        public void AddUnitTurnCount(float count) => currentTurnCount += count;
        public void ResetUnitTurnCount() => currentTurnCount = 0;
        public float GetCurrentTurnCount() => currentTurnCount;
    }
}