using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Stage
    {
        public struct LootingItem
        {
            public bool canLoot;
            public int lootItemID;
            public int lootItemMinCount;
            public int lootItemMaxCount;

            public LootingItem(bool canLoot = false)
            {
                this.canLoot = canLoot;
                lootItemID = -1;
                lootItemMinCount = -1;
                lootItemMaxCount = -1;
            }

            public LootingItem(int lootItemID, int lootItemMinCount, int lootItemMaxCount, bool canLoot = true)
            {
                this.canLoot = canLoot;
                this.lootItemID = lootItemID;
                this.lootItemMinCount = lootItemMinCount;
                this.lootItemMaxCount = lootItemMaxCount;
            }
        }

        private StageData data;
        public StageData Data => data;

        private List<Unit> enemyUnitList = new List<Unit>();
        private List<LootingItem> enemyLootItemList = new List<LootingItem>();
        public List<Unit> EnemyList => enemyUnitList;
        public List<LootingItem> EnemyLootItemList => enemyLootItemList;

        public Stage(StageData stageData)
        {
            data = stageData;

            if (data.EnemyUnit_1_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_1_ID, out UnitData unitData1))
            {
                Unit enemyUnit = new Unit(unitData1, data.EnemyUnit_1_Grade, data.EnemyUnit_1_Level);
                EnemyList.Add(enemyUnit);
                if (data.EnemyUnit_1_LootItemID != -1)
                {
                    enemyLootItemList.Add(new LootingItem(data.EnemyUnit_1_LootItemID, data.EnemyUnit_1_LootItemMinCount, data.EnemyUnit_1_LootItemMaxCount));
                }
                else
                {
                    enemyLootItemList.Add(new LootingItem());
                }
            }


            if (data.EnemyUnit_2_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_2_ID, out UnitData unitData2))
            {
                Unit enemyUnit = new Unit(unitData2, data.EnemyUnit_2_Grade, data.EnemyUnit_2_Level);
                EnemyList.Add(enemyUnit);
                if (data.EnemyUnit_2_LootItemID != -1)
                {
                    enemyLootItemList.Add(new LootingItem(data.EnemyUnit_2_LootItemID, data.EnemyUnit_2_LootItemMinCount, data.EnemyUnit_2_LootItemMaxCount));
                }
                else
                {
                    enemyLootItemList.Add(new LootingItem());
                }
            }

            if (data.EnemyUnit_3_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_3_ID, out UnitData unitData3))
            {
                Unit enemyUnit = new Unit(unitData3, data.EnemyUnit_3_Grade, data.EnemyUnit_3_Level);
                EnemyList.Add(enemyUnit);
                if (data.EnemyUnit_3_LootItemID != -1)
                {
                    enemyLootItemList.Add(new LootingItem(data.EnemyUnit_3_LootItemID, data.EnemyUnit_3_LootItemMinCount, data.EnemyUnit_3_LootItemMaxCount));
                }
                else
                {
                    enemyLootItemList.Add(new LootingItem());
                }
            }

            if (data.EnemyUnit_4_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_4_ID, out UnitData unitData4))
            {
                Unit enemyUnit = new Unit(unitData4, data.EnemyUnit_4_Grade, data.EnemyUnit_4_Level);
                EnemyList.Add(enemyUnit);
                if (data.EnemyUnit_4_LootItemID != -1)
                {
                    enemyLootItemList.Add(new LootingItem(data.EnemyUnit_4_LootItemID, data.EnemyUnit_4_LootItemMinCount, data.EnemyUnit_4_LootItemMaxCount));
                }
                else
                {
                    enemyLootItemList.Add(new LootingItem());
                }
            }

            if (data.EnemyUnit_5_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_5_ID, out UnitData unitData5))
            {
                Unit enemyUnit = new Unit(unitData5, data.EnemyUnit_5_Grade, data.EnemyUnit_5_Level);
                EnemyList.Add(enemyUnit);
                if (data.EnemyUnit_5_LootItemID != -1)
                {
                    enemyLootItemList.Add(new LootingItem(data.EnemyUnit_5_LootItemID, data.EnemyUnit_5_LootItemMinCount, data.EnemyUnit_5_LootItemMaxCount));
                }
                else
                {
                    enemyLootItemList.Add(new LootingItem());
                }
            }
        }
    }
}