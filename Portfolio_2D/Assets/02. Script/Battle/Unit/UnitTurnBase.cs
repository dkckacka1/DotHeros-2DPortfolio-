using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio.Battle
{
    public class UnitTurnBase : MonoBehaviour
    {
        private BattleUnit battleUnit;
        private BattleUnitUI battleUnitUI;

        public float currentTurnCount;

        public BattleUnit BattleUnit { get => battleUnit; }
        public BattleUnitUI BattleUnitUI { get => battleUnitUI; }
        public BattleUnitSequenceUI UnitSequenceUI { get => battleUnitUI.UnitSequenceUI; }

        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            battleUnitUI = GetComponent<BattleUnitUI>();
        }

        private void Start()
        {
            currentTurnCount = 0f;
            BattleManager.TurnBaseSystem.AddUnitTurnBase(this);
        }

        public void Dead()
        {
            BattleManager.TurnBaseSystem.UnitTurnBaseList.Remove(this);
        }

        public void TurnStart()
        {
            battleUnit.StartUnitTurn();
        }

        public void TurnEnd()
        {
            battleUnit.EndUnitTurn();

            ResetUnitTurnCount();
        }

        public void AddUnitTurnCount(float count) => currentTurnCount += count;
        public void ResetUnitTurnCount() => currentTurnCount = 0;
        public float GetCurrentTurnCount() => currentTurnCount;
    }
}