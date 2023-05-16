using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private Unit_Playable PlayerUnit;
        [SerializeField] private Unit_Enemy EnemyUnit;
        [SerializeField] private UnitSequenceUI unitSequenceUI;
        [SerializeField] private RectTransform unitSequenceUIParent;

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

                var newUnit = Instantiate(PlayerUnit, gridPosition.transform);
                gridPosition.unit = newUnit;
                var newUnitSequenceUI = Instantiate(unitSequenceUI, unitSequenceUIParent);
                unitBase = new UnitTurnBase(newUnit, newUnitSequenceUI);
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

                var newUnit = Instantiate(EnemyUnit, gridPosition.transform);
                gridPosition.unit = newUnit;
                var newUnitSequenceUI = Instantiate(unitSequenceUI, unitSequenceUIParent);
                unitBase = new UnitTurnBase(newUnit, newUnitSequenceUI);
                break;
            }

            return unitBase;
        }
    }
}