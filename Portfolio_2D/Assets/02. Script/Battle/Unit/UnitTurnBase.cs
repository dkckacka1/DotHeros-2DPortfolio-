using System;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class UnitTurnBase : MonoBehaviour
    {
        private BattleUnit battleUnit;
        private BattleUnitUI battleUnitUI;

        public float currentTurnCount;

        public BattleUnit BattleUnit { get => battleUnit; }
        public BattleUnitUI BattleUnitUI { get => battleUnitUI; }
        public UnitSequenceUI UnitSequenceUI { get => battleUnitUI.UnitSequenceUI; }

        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
            battleUnitUI = GetComponent<BattleUnitUI>();
        }

        private void Start()
        {
            BattleManager.TurnBaseSystem.UnitTurnBaseList.Add(this);
            currentTurnCount = 0f;
        }

        private void OnDisable()
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