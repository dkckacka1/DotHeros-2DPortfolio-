using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class TurnBaseSystem : MonoBehaviour
    {
        [SerializeField] private float turnCount = 100f;
        [SerializeField] private UnitTurnBase currentTurnUnit = null;

        private List<UnitTurnBase> unitTurnBaseList = new List<UnitTurnBase>();
        private TurnType currentTurnType;

        public List<UnitTurnBase> UnitTurnBaseList { get => unitTurnBaseList; }

        private void Update()
        {
            if (BattleManager.Instance.BattleState == BattleState.PLAY && currentTurnType == TurnType.WAITTING)
            {
                foreach (UnitTurnBase unitTurnBase in unitTurnBaseList)
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
            unitTurnBase.AddUnitTurnCount(unitTurnBase.BattleUnit.Speed * Time.deltaTime);
            float SequenceUIYNormalizedPos = unitTurnBase.GetCurrentTurnCount() / turnCount;

            BattleManager.BattleUIManager.SequenceUI.SetSequenceUnitUIYPosition(unitTurnBase.UnitSequenceUI, SequenceUIYNormalizedPos);
        }

        public void StartTurn(UnitTurnBase unitbase)
        {
            currentTurnUnit = unitbase;
            if (!unitbase.BattleUnit.IsEnemy)
            {
                BattleManager.ActionSystem.IsPlayerActionTime = true;
                currentTurnType = TurnType.PLAYER;
            }
            else
            {
                currentTurnType = TurnType.ENEMY;
            }

            currentTurnUnit.TurnStart();
        }

        public void TurnEnd()
        {
            if (currentTurnUnit == null) return;

            currentTurnUnit.TurnEnd();
            BattleManager.BattleUIManager.SequenceUI.SetSequenceUnitUIYPosition(currentTurnUnit.UnitSequenceUI, 0);
            BattleManager.ActionSystem.ClearSelectedUnits();
            currentTurnUnit = null;
            currentTurnType = TurnType.WAITTING;
            BattleManager.ActionSystem.IsPlayerActionTime = false;
        }
    }

}