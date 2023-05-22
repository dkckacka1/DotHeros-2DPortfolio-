using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class UnitTurnBase
    {
        public Unit unit;
        public float currentTurnCount;
        public GridPosition unitGridPosition;
        public UnitSequenceUI unitSequenceUI;
        public UnitSkillUI unitSkillUI;

        public UnitTurnBase(Unit unit, GridPosition gridPosition, UnitSequenceUI unitSequenceUI, UnitSkillUI unitSkillUI = null)
        {
            this.unit = unit;
            this.unitGridPosition = gridPosition;
            this.unitSequenceUI = unitSequenceUI;

            this.unitSkillUI = unitSkillUI;
            unitSkillUI?.SetUnit(this.unit);

            currentTurnCount = 0f;
        }

        public void TurnStart()
        {
            unit.StartCurrentTurn();

            unitSkillUI?.ShowSkillUI();
        }

        public void TurnEnd()
        {
            ResetUnitTurnCount();
            unit.EndCurrentTurn();

            unitSkillUI?.HideSkillUI();
        }

        public void AddUnitTurnCount(float count) => currentTurnCount += count;
        public void ResetUnitTurnCount() => currentTurnCount = 0;
        public float GetCurrentTurnCount() => currentTurnCount;
    }
}