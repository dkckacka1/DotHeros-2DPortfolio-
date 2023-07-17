using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
 * 맵 데이터를 토대로 만든 맵 클래스
 */

namespace Portfolio
{
    public class Map
    {
        private MapData mapData;

        private List<Stage> stageList = new List<Stage>();  // 현재 맵이 스테이지 리스트
        public LootItemTable lootItemTable; // 현재 맵의 루팅아이템 테이블
        public List<Stage> StageList => stageList;
        public int MapID => mapData.ID;
        public string MapName => mapData.mapName;
        public float MapExperience => mapData.experienceValue;
        public int MapUserExperience => mapData.consumEnergy * 10;        // 획득 유저 경험치는 소비 에너지양의 * 10
        public int ConsumEnergy => mapData.consumEnergy;
        public bool IsExternMap => mapData.isExternalMap;
        public bool IsHeigestMapID => !IsExternMap && GameManager.CurrentUser.ClearHighestMapID == mapData.ID; // 이 맵이 유저가 가장 높이 깬 맵이 맞는지 확인
        public bool IsNextMapVaild
        {
            get
            {
                if (IsExternMap)
                    // 외전 맵이면 다음맵은 없으니 false 리턴
                {
                    return false;
                }
                else
                {
                    // 게임매니저를 통해 다음 맵이 있는지 확인한다.
                    return GameManager.Instance.CheckContainsMap(mapData.ID + 1);
                }
            }
        }

        public int GetGoldValue => mapData.getGoldValue;
        // 맵 데이터를 토대로 만든다.
        public Map(MapData mapData)
        {
            this.mapData = mapData;

            // 루팅아이템 스크립테이블 오브젝트 가져오기
            lootItemTable = Resources.Load<LootItemTable>(Constant.ScriptableObjectResourcesPath + "\\" + Constant.LootingTableResourcesPath + "\\" + $"LootingTable_{mapData.ID}");


            // 맵 데이터의 스테이지 ID를 통해서 스테이지를 생성한다.
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

        // 현재 맵에서 출현할 수 있는 유닛 리스트를 리턴한다.
        public List<Unit> GetMapUnitList()
        {
            List<Unit> mapEnemyList = new List<Unit>();

            foreach (var stage in stageList)
            {
                mapEnemyList.AddRange(stage.EnemyList);
            }

            List<int> unitIDList = new List<int>();
            // 같은 ID가 이미 존재하면 리스트에 넣지 않도록 하여 중복을 피한다.
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