using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private BattleUnit playerBattleUnitPrefab;
        [SerializeField] private BattleUnit enemyBattleUnitPrefab;

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;
        [SerializeField] List<GridPosition> enemyGrids;

        public bool TryCreateBattleUnit(Unit unit, bool isEnemy, out BattleUnit battleUnit)
        {
            List<GridPosition> gridList = null;
            BattleUnit battleUnitPrefab = null;

            if (!isEnemy)
            {
                gridList = playerGrids;
                battleUnitPrefab = playerBattleUnitPrefab;
            }
            else
            {
                gridList = enemyGrids;
                battleUnitPrefab = enemyBattleUnitPrefab;
            }

            if (gridList == null || battleUnitPrefab == null)
            {
                battleUnit = null;
                return false;
            }

            foreach (var grid in gridList)
            {
                if (grid.unit != null)
                {
                    continue;
                }

                battleUnit = Instantiate(battleUnitPrefab, grid.transform);
                battleUnit.SetUnit(unit);
                grid.unit = battleUnit;
                return true;
            }

            battleUnit = null;
            return false;
        }

        public UnitTurnBase CreatePlayableUnitBase()
        {
            UnitTurnBase unitBase = null;

            foreach (var gridPosition in playerGrids)
            {
                if (gridPosition.unit != null)
                {
                    continue;
                }

                var newUnit = Instantiate(playerBattleUnitPrefab, gridPosition.transform);
                gridPosition.unit = newUnit;
                break;
            }

            return unitBase;
        }

        public UnitTurnBase CreatePlayableUnitBase(Unit unit)
        {
            UnitTurnBase unitBase = null;

            foreach (var gridPosition in playerGrids)
            {
                if (gridPosition.unit != null)
                {
                    continue;
                }

                var newBattleUnit = Instantiate(playerBattleUnitPrefab, gridPosition.transform);
                gridPosition.unit = newBattleUnit;
                newBattleUnit.SetUnit(unit);
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

                var newUnit = Instantiate(enemyBattleUnitPrefab, gridPosition.transform);
                gridPosition.unit = newUnit;
                break;
            }

            return unitBase;
        }
    }
}