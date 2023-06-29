using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using Portfolio;
using System;
using Portfolio.skill;
using Portfolio.condition;
using Portfolio.Battle;

namespace Portfolio
{
    public static class ResourcesLoader
    {
        private const string dataResourcesPath = @"Data/";
        private const string spriteResourcesPath = @"Sprite/";
        private const string AnimationResourcesPath = @"Animation/";
        private const string SkillEffectResourcesPath = @"SkillEffect/";

        public static void LoadAllData(Dictionary<int, Data> dataDic)
        {
            //Debug.Log(dataResourcesPath + Constant.unitDataJsonName);
            LoadData<UnitData>(dataDic, dataResourcesPath + Constant.unitDataJsonName);
            LoadData<ActiveSkillData>(dataDic, dataResourcesPath + Constant.activeSkillJsonName);
            LoadData<PassiveSkillData>(dataDic, dataResourcesPath + Constant.passiveSkillJsonName);
            LoadData<ConditionData>(dataDic, dataResourcesPath + Constant.conditionDataJsonName);
            LoadData<MapData>(dataDic, dataResourcesPath + Constant.mapDataJsonName);
            LoadData<StageData>(dataDic, dataResourcesPath + Constant.stageDataJsonName);
            LoadData<ConsumableItemData>(dataDic, dataResourcesPath +Constant.consumableItemDataJsonName);
        }

        public static void LoadAllResource(Dictionary<string, Sprite> spriteDic, Dictionary<string, RuntimeAnimatorController> animDic)
        {
            var sprites = Resources.LoadAll<Sprite>(spriteResourcesPath);
            foreach (var sprite in sprites)
            {
                spriteDic.Add(sprite.name, sprite);
            }

            var animations = Resources.LoadAll<RuntimeAnimatorController>(AnimationResourcesPath);
            foreach (var anim in animations)
            {
                animDic.Add(anim.name, anim);
            }
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
    }
}