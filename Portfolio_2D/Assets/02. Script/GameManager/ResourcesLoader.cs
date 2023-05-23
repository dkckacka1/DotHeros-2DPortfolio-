using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Portfolio.skill.Option;
using System;

namespace Portfolio
{
    public static class ResourcesLoader
    {
        public static bool TryLoadSkillData(Dictionary<int, Data> DataDic)
        {
            string skillResourcePath = @"Data/SkillData";

            var json = Resources.Load<TextAsset>(skillResourcePath);
            var obj = JsonConvert.DeserializeObject<SkillData[]>(json.text);

            foreach (var skillData in obj)
            {
                DataDic.Add(skillData.ID, skillData);
            }

            return true;
        }

        public static bool TryLoadUnitData(Dictionary<int, Data> DataDic)
        {
            string UnitResourcePath = @"Data/UnitData";

            var json = Resources.Load<TextAsset>(UnitResourcePath);
            var obj = JsonConvert.DeserializeObject<UnitData[]>(json.text);

            foreach (var unitData in obj)
            {
                DataDic.Add(unitData.ID, unitData);
            }

            return true;
        }
    }
}