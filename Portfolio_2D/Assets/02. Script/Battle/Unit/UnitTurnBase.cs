using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class UnitTurnBase
    {
        public BattleUnit unit;
        public float currentTurnCount;
        public GridPosition unitGridPosition;
        public UnitSequenceUI unitSequenceUI;
        public UnitSkillUI unitSkillUI;

        public event EventHandler OnTurnStartEvent;
        public event EventHandler OnTurnEndEvent;

        public UnitTurnBase(BattleUnit unit, GridPosition gridPosition, UnitSequenceUI unitSequenceUI, UnitSkillUI unitSkillUI = null)
        {
            this.unit = unit;
            this.unitGridPosition = gridPosition;
            this.unitSequenceUI = unitSequenceUI;

            OnTurnStartEvent += unit.UnitTurnBase_OnTurnStartEvent;
            OnTurnEndEvent += unit.UnitTurnBase_OnTurnEndEvent;

            if (unitSkillUI != null)
            {
                this.unitSkillUI = unitSkillUI;
                unitSkillUI.SetUnit(this.unit);

                OnTurnStartEvent += unitSkillUI.UnitTurnBase_OnTurnStartEvent;
                OnTurnEndEvent += unitSkillUI.UnitTurnBase_OnTurnEndEvent;
            }

            currentTurnCount = 0f;
        }

        public void TurnStart()
        {

            OnTurnStartEvent.Invoke(this, EventArgs.Empty);
        }

        public void TurnEnd()
        {
            ResetUnitTurnCount();

            OnTurnEndEvent.Invoke(this, EventArgs.Empty);
        }

        public void AddUnitTurnCount(float count) => currentTurnCount += count;
        public void ResetUnitTurnCount() => currentTurnCount = 0;
        public float GetCurrentTurnCount() => currentTurnCount;
    }
}