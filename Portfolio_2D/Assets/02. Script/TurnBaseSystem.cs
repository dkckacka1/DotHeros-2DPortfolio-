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

        private bool isUnitTurn;

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
            if (!isUnitTurn)
            {
                foreach (UnitTurnBase unitTurnBase in BattleManager.Instance.GetUnitList())
                {
                    if (unitTurnBase.currentTurnCount >= turnCount)
                    {
                        currentTurnUnit = unitTurnBase;
                        currentTurnUnit.onSelectedTurnEvent?.Invoke();
                        isUnitTurn = true;
                        break;
                    }

                    ProceedTurn(unitTurnBase);
                }
            }
        }

        private void ProceedTurn(UnitTurnBase unitTurnBase)
        {
            unitTurnBase.AddUnitTurnCount(unitTurnBase.unit.Speed * Time.deltaTime);
            float SequenceUIXNormalizedPos = unitTurnBase.GetCurrentTurnCount() / turnCount;
            BattleManager.Instance.BattleUI.SequenceUI.SetSequenceUnitUIXPosition(unitTurnBase.unitSequenceUI, SequenceUIXNormalizedPos);
        }

        public void TurnEnd()
        {
            if (currentTurnUnit == null) return;

            currentTurnUnit.ResetUnitTurnCount();
            BattleManager.Instance.BattleUI.SequenceUI.SetSequenceUnitUIXPosition(currentTurnUnit.unitSequenceUI, 0);
            currentTurnUnit = null;
            isUnitTurn = false;
        }
    }

}