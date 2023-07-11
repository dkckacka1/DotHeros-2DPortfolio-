using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �������� Data�� ���� ���� �������� Ŭ����
 */

namespace Portfolio
{
    public class Stage
    {
        // ���� ����
        public struct LootingItem
        {       
            public bool canLoot;            // ���� �����Ѱ�
            public int lootItemID;          // ������ ������ ID
            public int lootItemMinCount;    // ���þ������� �ּ� ����
            public int lootItemMaxCount;    // ���þ������� �ִ� ����

        }

        private StageData data;

        private List<Unit> enemyUnitList = new List<Unit>();                    // ���� ������������ ���� �� ����Ʈ
        public List<Unit> EnemyList => enemyUnitList;

        public Stage(StageData stageData)
        {
            data = stageData;

            // ���� ID�� ���ؼ� �������� �� ����Ʈ�� ���� ������ �־��ش�.
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