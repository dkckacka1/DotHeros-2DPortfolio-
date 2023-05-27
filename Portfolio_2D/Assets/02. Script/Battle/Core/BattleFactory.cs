using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private PlayerBattleUnit playerUnit;
        [SerializeField] private EnemyBattleUnit enemyUnit;

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

        public UnitTurnBase CreatePlayableUnitBase(Unit unit)
        {
            UnitTurnBase unitBase = null;

            foreach (var gridPosition in playerGrids)
            {
                if (gridPosition.unit != null)
                {
                    continue;
                }

                var newBattleUnit = Instantiate(playerUnit, gridPosition.transform);
                gridPosition.unit = newBattleUnit;
                newBattleUnit.SetUnit(unit);
                unitBase = new UnitTurnBase(newBattleUnit, gridPosition, BattleManager.BattleUIManager.CreateUnitSequenceUI(), BattleManager.BattleUIManager.CreateUnitSkillUI());
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