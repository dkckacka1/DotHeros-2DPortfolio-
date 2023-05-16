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
                        AllUnitTurnSpriteActive();
                        currentTurnUnit.onSelectedTurnEvent?.Invoke();
                        isUnitTurn = true;
                        Debug.Log(unitTurnBase.unit.unitType);
                        switch (unitTurnBase.unit.unitType)
                        {
                            case UnitType.Player:
                                BattleManager.Instance.SwitchBattleState(BattleState.PLAYERTURN);
                                break;
                            case UnitType.Enemy:
                                BattleManager.Instance.SwitchBattleState(BattleState.ENEMYTURN);
                                break;
                        }

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

        private void AllUnitTurnSpriteActive()
        {
            foreach (UnitTurnBase unit in BattleManager.Instance.GetUnitList())
            {
                unit.unit.SetCurrentTurnSprite(currentTurnUnit?.unit == unit.unit);
            }
        }

        public void TurnEnd()
        {
            if (currentTurnUnit == null) return;

            currentTurnUnit.ResetUnitTurnCount();
            BattleManager.Instance.BattleUI.SequenceUI.SetSequenceUnitUIXPosition(currentTurnUnit.unitSequenceUI, 0);
            currentTurnUnit = null;
            AllUnitTurnSpriteActive();
            isUnitTurn = false;
            BattleManager.Instance.SwitchBattleState(BattleState.WATTINGTURN);
        }
    }

}