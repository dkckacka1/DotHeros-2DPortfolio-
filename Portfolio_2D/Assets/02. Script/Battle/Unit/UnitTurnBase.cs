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

        public UnitTurnBase(PlayerBattleUnit unit, GridPosition gridPosition, UnitSequenceUI unitSequenceUI, UnitSkillUI unitSkillUI)
        {
            SetDefaultBattleUnit(unit, gridPosition, unitSequenceUI);

            if (unit is PlayerBattleUnit)
            {
                if (unitSkillUI != null)
                {
                    (unit as PlayerBattleUnit).SetUI(unitSkillUI);
                    this.unitSkillUI = unitSkillUI;
                    unitSkillUI.SetUnit(this.unit);
                }
            }
        }



        public UnitTurnBase(EnemyBattleUnit unit, GridPosition gridPosition, UnitSequenceUI unitSequenceUI, UnitSkillUI unitSkillUI = null)
        {
            SetDefaultBattleUnit(unit, gridPosition, unitSequenceUI);

            currentTurnCount = 0f;
        }

        private void SetDefaultBattleUnit(BattleUnit unit, GridPosition gridPosition, UnitSequenceUI unitSequenceUI)
        {
            this.unit = unit;
            this.unitGridPosition = gridPosition;
            this.unitSequenceUI = unitSequenceUI;

            unitSequenceUI.SetNameText(unit.name);

            OnTurnStartEvent += unit.UnitTurnBase_OnTurnStartEvent;
            OnTurnEndEvent += unit.UnitTurnBase_OnTurnEndEvent;
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