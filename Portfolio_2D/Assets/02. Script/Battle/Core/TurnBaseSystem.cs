using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class TurnBaseSystem : MonoBehaviour
    {
        public static TurnBaseSystem Instance { get; private set; }

        [SerializeField] private float turnCount = 100f;

        [SerializeField] private UnitTurnBase currentTurnUnit = null;

        private TurnType currentTurnType;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

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
            BattleManager.Instance.BattleUI.SequenceUI.SetSequenceUnitUIYPosition(unitTurnBase.unitSequenceUI, SequenceUIYNormalizedPos);
        }

        public void StartTurn(UnitTurnBase unitbase)
        {
            currentTurnUnit = unitbase;
            switch (unitbase.unit.unitType)
            {
                case UnitType.Player:
                    ActionSystem.Instance.IsPlayerActionTime = true;
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
            BattleManager.Instance.BattleUI.SequenceUI.SetSequenceUnitUIYPosition(currentTurnUnit.unitSequenceUI, 0);
            ActionSystem.Instance.ClearSelectedUnits();
            currentTurnUnit = null;
            currentTurnType = TurnType.WAITTING;
            ActionSystem.Instance.IsPlayerActionTime = false;

        }
    }

}