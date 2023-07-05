using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Portfolio
{
    public class Map
    {
        private MapData mapData;

        private List<Stage> stageList = new List<Stage>();
        public List<Stage> StageList => stageList;
        public int MapID => mapData.ID;
        public string MapName => mapData.mapName;
        public float MapExperience => mapData.experienceValue;
        public int ConsumEnergy => mapData.consumEnergy;
        public bool IsExternMap => mapData.isExternalMap;
        public bool IsHeigestMapID => !IsExternMap && GameManager.CurrentUser.ClearHighestMapID == mapData.ID;
        public bool IsNextMapVaild
        {
            get
            {
                if (IsExternMap)
                {
                    return false;
                }
                else
                {
                    return GameManager.Instance.CheckContainsMap(mapData.ID + 1);
                }
            }
        }

        public int GetGoldValue => mapData.getGoldValue;
        public Map(MapData mapData)
        {
            this.mapData = mapData;

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

        public List<Unit> GetMapUnitList()
        {
            List<Unit> mapEnemyList = new List<Unit>();

            foreach (var stage in stageList)
            {
                mapEnemyList.AddRange(stage.EnemyList);
            }

            List<int> unitIDList = new List<int>();
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

        private bool test(Unit arg)
        {
            throw new NotImplementedException();
        }
    }
}