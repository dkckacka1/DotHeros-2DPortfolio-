using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Battle
{
    public class BattleFactory : MonoBehaviour
    {
        [Header("Unit")]
        [SerializeField] private BattleUnit playerBattleUnitPrefab;
        [SerializeField] private BattleUnit enemyBattleUnitPrefab;

        [Header("Grid")]
        [SerializeField] List<GridPosition> playerGrids;
        [SerializeField] List<GridPosition> enemyGrids;

        int playerNum = -1;
        int enemyNum = -1;

        public void CreateStage(Stage stage)
        {
            foreach (var grid in enemyGrids)
            {
                grid.unit = null;
            }

            for (int i = 0; i < stage.EnemyList.Count; i++)
            {
                TryCreateBattleUnit(stage.EnemyList[i], true, out BattleUnit battleUnit);
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

            if (gridList == null || battleUnitPrefab == null || unit == null)
            {
                battleUnit = null;
                return false;
            }

            // 유저에게서 받은 포메이션으로 유닛 생성 바꿔야함

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