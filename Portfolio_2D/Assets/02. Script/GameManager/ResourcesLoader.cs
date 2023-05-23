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
        public static bool TryLoadSkillData(Dictionary<int, Skill> skillDataDic)
        {
            string skillResourcePath = @"Data/SkillData";

            var json = Resources.Load<TextAsset>(skillResourcePath);
            var obj = JsonConvert.DeserializeObject<SkillData[]>(json.text);

            foreach (var skillData in obj)
            {
                skillDataDic.Add(skillData.ID, new Skill(skillData));
            }

            return true;
        }

        public static bool TryLoadUnitData(Dictionary<int, Unit> unitDataDic)
        {
            string UnitResourcePath = @"Data/UnitData";

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