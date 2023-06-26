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

        public void CreateStage(Stage stage)
        {
            foreach (var grid in enemyGrids)
            {
                grid.CreateBattleUnit(null);
            }

            var enemyList = CreateEnemyUnit(stage.EnemyList);

            for (int i = 0; i < stage.EnemyList.Count; i++)
            {
                BattleUnit battleUnit = enemyList[i];
                if (battleUnit == null) continue;

                if (stage.EnemyLootItemList[i].canLoot)
                {
                    int itemLootCount = Random.Range(stage.EnemyLootItemList[i].lootItemMinCount, stage.EnemyLootItemList[i].lootItemMaxCount + 1);
                    if (itemLootCount != 0)
                    {
                        int itemID = stage.EnemyLootItemList[i].lootItemID;
                        battleUnit.OnDeadEvent += (sender, e) => { BattleManager.Instance.GetItem(itemID, itemLootCount); };
                    }
                }
            }
        }

        public List<BattleUnit> CreateUserUnit(List<Unit> unitList)
        {
            List<BattleUnit> playerList = new List<BattleUnit>();

            for (int i = 0; i < 5; i++)
            {
                if (unitList.Count <= i)
                {
                    continue;
                }

                var battleUnit = CreateBattleUnit(unitList[i], playerGrids[i], false);
                playerList.Add(battleUnit);

                if (battleUnit != null)
                {
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return playerList;
        }

        public List<BattleUnit> CreateEnemyUnit(List<Unit> unitList)
        {
            List<BattleUnit> enemyList = new List<BattleUnit>();

            for (int i =0; i < 5; i++)
            {
                if(unitList.Count <= i)
                {
                    continue;
                }

                var battleUnit = CreateBattleUnit(unitList[i], enemyGrids[i], true);
                enemyList.Add(battleUnit);

                if (battleUnit != null)
                {
                    BattleManager.Instance.AddUnitinUnitList(battleUnit);
                }
            }

            return enemyList;
        }

        private BattleUnit CreateBattleUnit(Unit unit, GridPosition grid, bool isEnemy)
        {
            if (unit == null)
            {
                return null;
            }

            BattleUnit battleUnitPrefab;

            if (!isEnemy)
            {
                battleUnitPrefab = playerBattleUnitPrefab;
            }
            else
            {
                battleUnitPrefab = enemyBattleUnitPrefab;
            }


            BattleUnit battleUnit;
            battleUnit = Instantiate(battleUnitPrefab, grid.transform);
            battleUnit.SetUnit(unit);
            battleUnit.name = unit.UnitName;
            grid.CreateBattleUnit(battleUnit);

            return battleUnit;
        }
    }
}