using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
 * �� �����͸� ���� ���� �� Ŭ����
 */

namespace Portfolio
{
    public class Map
    {
        private MapData mapData;

        private List<Stage> stageList = new List<Stage>();  // ���� ���� �������� ����Ʈ
        public LootItemTable lootItemTable; // ���� ���� ���þ����� ���̺�
        public List<Stage> StageList => stageList;
        public int MapID => mapData.ID;
        public string MapName => mapData.mapName;
        public float MapExperience => mapData.experienceValue;
        public int MapUserExperience => mapData.consumEnergy * 10;        // ȹ�� ���� ����ġ�� �Һ� ���������� * 10
        public int ConsumEnergy => mapData.consumEnergy;
        public bool IsExternMap => mapData.isExternalMap;
        public bool IsHeigestMapID => !IsExternMap && GameManager.CurrentUser.ClearHighestMapID == mapData.ID; // �� ���� ������ ���� ���� �� ���� �´��� Ȯ��
        public bool IsNextMapVaild
        {
            get
            {
                if (IsExternMap)
                    // ���� ���̸� �������� ������ false ����
                {
                    return false;
                }
                else
                {
                    // ���ӸŴ����� ���� ���� ���� �ִ��� Ȯ���Ѵ�.
                    return GameManager.Instance.CheckContainsMap(mapData.ID + 1);
                }
            }
        }

        public int GetGoldValue => mapData.getGoldValue;
        // �� �����͸� ���� �����.
        public Map(MapData mapData)
        {
            this.mapData = mapData;

            // ���þ����� ��ũ�����̺� ������Ʈ ��������
            lootItemTable = Resources.Load<LootItemTable>(Constant.ScriptableObjectResourcesPath + "\\" + Constant.LootingTableResourcesPath + "\\" + $"LootingTable_{mapData.ID}");


            // �� �������� �������� ID�� ���ؼ� ���������� �����Ѵ�.
            if (mapData.stage_1_ID != -1 && GameManager.Instance.TryGetData(mapData.stage_1_ID, out StageData stageData1))
            {
                stageList.Add(new Stage(stageData1));
            }

            if (mapData.stage_2_ID != -1 && GameManager.Instance.TryGetData(mapData.stage_2_ID, out StageData stageData2))
            {
                stageList.Add(new Stage(stageData2));
            }

            if (mapData.stage_3_ID != -1 && GameManager.Instance.TryGetData(mapData.stage_3_ID, out StageData stageData3))
            {
                stageList.Add(new Stage(stageData3));
            }

            if (mapData.stage_4_ID != -1 && GameManager.Instance.TryGetData(mapData.stage_4_ID, out StageData stageData4))
            {
                stageList.Add(new Stage(stageData4));
            }

            if (mapData.stage_5_ID != -1 && GameManager.Instance.TryGetData(mapData.stage_5_ID, out StageData stageData5))
            {
                stageList.Add(new Stage(stageData5));
            }
        }

        // ���� �ʿ��� ������ �� �ִ� ���� ����Ʈ�� �����Ѵ�.
        public List<Unit> GetMapUnitList()
        {
            List<Unit> mapEnemyList = new List<Unit>();

            foreach (var stage in stageList)
            {
                mapEnemyList.AddRange(stage.EnemyList);
            }

            List<int> unitIDList = new List<int>();
            // ���� ID�� �̹� �����ϸ� ����Ʈ�� ���� �ʵ��� �Ͽ� �ߺ��� ���Ѵ�.
            var enemyListDis = mapEnemyList.Where((unit) => 
            {

                if (unitIDList.Contains(unit.UnitID))
                {
                    return false;
                }
                else
                {
                    unitIDList.Add(unit.UnitID);
                    return true;
                }
            }).ToList();

            return enemyListDis;
        }
    }
}