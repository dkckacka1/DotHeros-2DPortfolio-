using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace Portfolio
{
    public static class ResourcesLoader
    {
        public static bool TryLoadSkillData(ref Dictionary<int, Data> DataDic)
        {
            string skillResourcePath = @"Data/SkillData";
            var json = Resources.Load<TextAsset>(skillResourcePath);

            var obj = JsonConvert.DeserializeObject<SkillData[]>(json.text);

            foreach (var skillData in obj)
            {
                DataDic.Add(skillData.ID, skillData);
            }


            return false;
        }
    }
}