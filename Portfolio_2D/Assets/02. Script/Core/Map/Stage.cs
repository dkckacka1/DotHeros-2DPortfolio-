using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스테이지 Data를 토대로 만든 스테이지 클래스
 */

namespace Portfolio
{
    public class Stage
    {
        // 루팅 정보
        public struct LootingItem
        {       
            public bool canLoot;            // 루팅 가능한가
            public int lootItemID;          // 루팅할 아이템 ID
            public int lootItemMinCount;    // 루팅아이템의 최소 개수
            public int lootItemMaxCount;    // 루팅아이템의 최대 개수

        }

        private StageData data;

        private List<Unit> enemyUnitList = new List<Unit>();                    // 현재 스테이지에서 나올 적 리스트
        public List<Unit> EnemyList => enemyUnitList;

        public Stage(StageData stageData)
        {
            data = stageData;

            // 유닛 ID를 통해서 스테이지 적 리스트에 유닛 정보를 넣어준다.
            if (data.EnemyUnit_1_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_1_ID, out UnitData unitData1))
            {
                Unit enemyUnit = new Unit(unitData1, data.EnemyUnit_1_Grade, data.EnemyUnit_1_Level);
                EnemyList.Add(enemyUnit);
            }


            if (data.EnemyUnit_2_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_2_ID, out UnitData unitData2))
            {
                Unit enemyUnit = new Unit(unitData2, data.EnemyUnit_2_Grade, data.EnemyUnit_2_Level);
                EnemyList.Add(enemyUnit);
            }

            if (data.EnemyUnit_3_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_3_ID, out UnitData unitData3))
            {
                Unit enemyUnit = new Unit(unitData3, data.EnemyUnit_3_Grade, data.EnemyUnit_3_Level);
                EnemyList.Add(enemyUnit);
            }

            if (data.EnemyUnit_4_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_4_ID, out UnitData unitData4))
            {
                Unit enemyUnit = new Unit(unitData4, data.EnemyUnit_4_Grade, data.EnemyUnit_4_Level);
                EnemyList.Add(enemyUnit);
            }

            if (data.EnemyUnit_5_ID != -1 && GameManager.Instance.TryGetData(data.EnemyUnit_5_ID, out UnitData unitData5))
            {
                Unit enemyUnit = new Unit(unitData5, data.EnemyUnit_5_Grade, data.EnemyUnit_5_Level);
                EnemyList.Add(enemyUnit);
            }
        }
    }
}