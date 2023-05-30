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

        int playerNum = 1;
        int enemyNum = 1;

        public void CreateStage(StageData stageData)
        {
            foreach (var grid in enemyGrids)
            {
                grid.unit = null;
            }

            if (TryGetUnitinStageUnitID(stageData.EnemyUnit_1_ID, out Unit unit1))
            {
                TryCreateBattleUnit(unit1, true, out BattleUnit battleUnit);
                BattleManager.Instance.AddUnitinUnitList(battleUnit);
            }

            if (TryGetUnitinStageUnitID(stageData.EnemyUnit_2_ID, out Unit unit2))
            {
                TryCreateBattleUnit(unit2, true, out BattleUnit battleUnit);
                BattleManager.Instance.AddUnitinUnitList(battleUnit);
            }

            if (TryGetUnitinStageUnitID(stageData.EnemyUnit_3_ID, out Unit unit3))
            {
                TryCreateBattleUnit(unit3, true, out BattleUnit battleUnit);
                BattleManager.Instance.AddUnitinUnitList(battleUnit);
            }

            if (TryGetUnitinStageUnitID(stageData.EnemyUnit_4_ID, out Unit unit4))
            {
                TryCreateBattleUnit(unit4, true, out BattleUnit battleUnit);
                BattleManager.Instance.AddUnitinUnitList(battleUnit);
            }

            if (TryGetUnitinStageUnitID(stageData.EnemyUnit_5_ID, out Unit unit5))
            {
                TryCreateBattleUnit(unit5, true, out BattleUnit battleUnit);
                BattleManager.Instance.AddUnitinUnitList(battleUnit);
            }
        }

        private bool TryGetUnitinStageUnitID(int unitID, out Unit unit)
        {
            if (unitID == -1)
            {
                unit = null;
                return false;
            }

            if (GameManager.Instance.TryGetUnit(unitID, out unit))
            {
                return true;
            }

            return false;
        }

        public bool TryCreateBattleUnit(Unit unit, bool isEnemy, out BattleUnit battleUnit)
        {
            List<GridPosition> gridList = null;
            BattleUnit battleUnitPrefab = null;
            int num = 0;

            if (!isEnemy)
            {
                gridList = playerGrids;
                battleUnitPrefab = playerBattleUnitPrefab;
                num = playerNum++;
            }
            else
            {
                gridList = enemyGrids;
                battleUnitPrefab = enemyBattleUnitPrefab;
                num = enemyNum++;
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
                battleUnit.name = unit.Data.unitName + "_" + num;
                return true;
            }

            battleUnit = null;
            return false;
        }

    }
}