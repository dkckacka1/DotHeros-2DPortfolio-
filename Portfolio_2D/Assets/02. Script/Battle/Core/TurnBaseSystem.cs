using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class TurnBaseSystem : MonoBehaviour
    {
        [SerializeField] private float turnCount = 100f;

        [SerializeField] private UnitTurnBase currentTurnUnit = null;

        private TurnType currentTurnType;

        private void Update()
        {
            if (BattleManager.Instance.BattleState == BattleState.PLAY && currentTurnType == TurnType.WAITTING)
            {
                foreach (UnitTurnBase unitTurnBase in BattleManager.Instance.GetUnitList())
                {
                    if (unitTurnBase.currentTurnCount >= turnCount)
                    {
                        StartTurn(unitTurnBase);

                        break;
                    }

                    ProceedTurn(unitTurnBase);
                }
            }
        }

        private void ProceedTurn(UnitTurnBase unitTurnBase)
        {
            unitTurnBase.AddUnitTurnCount(unitTurnBase.unit.Speed * Time.deltaTime);
            float SequenceUIYNormalizedPos = unitTurnBase.GetCurrentTurnCount() / turnCount;
            BattleManager.BattleUIManager.SequenceUI.SetSequenceUnitUIYPosition(unitTurnBase.unitSequenceUI, SequenceUIYNormalizedPos);
        }

        public void StartTurn(UnitTurnBase unitbase)
        {
            currentTurnUnit = unitbase;
            switch (unitbase.unit.UnitType)
            {
                case UnitType.Player:
                    BattleManager.ActionSystem.IsPlayerActionTime = true;
                    currentTurnType = TurnType.PLAYER;
                    break;
                case UnitType.Enemy:
                    currentTurnType = TurnType.ENEMY;
                    break;
            }

            currentTurnUnit.TurnStart();
        }

        public void TurnEnd()
        {
            if (currentTurnUnit == null) return;

            currentTurnUnit.TurnEnd();
            BattleManager.BattleUIManager.SequenceUI.SetSequenceUnitUIYPosition(currentTurnUnit.unitSequenceUI, 0);
            BattleManager.ActionSystem.ClearSelectedUnits();
            currentTurnUnit = null;
            currentTurnType = TurnType.WAITTING;
            BattleManager.ActionSystem.IsPlayerActionTime = false;

        }
    }

}