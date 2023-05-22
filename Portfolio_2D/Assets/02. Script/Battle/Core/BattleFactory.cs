using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private PlayerUnit playerUnit;
        [SerializeField] private EnemyUnit enemyUnit;

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;
        [SerializeField] List<GridPosition> enemyGrids;

        public UnitTurnBase CreatePlayableUnitBase()
        {
            UnitTurnBase unitBase = null;

            foreach (var gridPosition in playerGrids)
            {
                if (gridPosition.unit != null)
                {
                    continue;
                }

                var newUnit = Instantiate(playerUnit, gridPosition.transform);
                gridPosition.unit = newUnit;
                unitBase = new UnitTurnBase(newUnit, gridPosition, BattleManager.BattleUIManager.CreateUnitSequenceUI(), BattleManager.BattleUIManager.CreateUnitSkillUI());
                break;
            }

            return unitBase;
        }

        public UnitTurnBase CreateEnemyUnitBase()
        {
            UnitTurnBase unitBase = null;

            foreach (var gridPosition in enemyGrids)
            {
                if (gridPosition.unit != null)
                {
                    continue;
                }

                var newUnit = Instantiate(enemyUnit, gridPosition.transform);
                gridPosition.unit = newUnit;
                unitBase = new UnitTurnBase(newUnit, gridPosition, BattleManager.BattleUIManager.CreateUnitSequenceUI());
                break;
            }

            return unitBase;
        }
    }
}