using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class Map
    {
        private MapData mapData;

        public MapData MapData => mapData;

        private List<Stage> stageList = new List<Stage>();
        public List<Stage> StageList => stageList;

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
    }
}