using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Portfolio;
using System;
using Portfolio.skill;

namespace Portfolio
{
    public static class ResourcesLoader
    {
        private const string dataResourcesPath = @"Data/";

        public static void LoadAllData(Dictionary<int, Data> dataDic)
        {
            LoadData<UnitData>(dataDic, dataResourcesPath + Constant.unitDataJsonName);
            LoadData<ActiveSkillData>(dataDic, dataResourcesPath + Constant.activeSkillJsonName);
            LoadData<PassiveSkillData>(dataDic, dataResourcesPath + Constant.passiveSkillJsonName);
        }

        private static void LoadData<T>(Dictionary<int, Data> dataDic, string jsonPath) where T : Data
        {
            var json = Resources.Load<TextAsset>(jsonPath);
            var datas = JsonConvert.DeserializeObject<T[]>(json.text);

            foreach (var data in datas)
            {
                dataDic.Add(data.ID ,data);
            }
        }

        public static bool TryLoadUnitData(Dictionary<int, Unit> unitDataDic)
        {
            string UnitResourcePath = dataResourcesPath + Constant.unitDataJsonName;

            var json = Resources.Load<TextAsset>(UnitResourcePath);
            var obj = JsonConvert.DeserializeObject<UnitData[]>(json.text);

            foreach (var unitData in obj)
            {
                unitDataDic.Add(unitData.ID, new Unit(unitData));
            }

            return true;
        }
    }
}