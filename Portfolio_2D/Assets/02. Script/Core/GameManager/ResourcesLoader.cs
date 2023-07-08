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

/*
 * ���ҽ����ε��ϴ� �Լ��� ��Ƴ��� static Ŭ����
 */

namespace Portfolio
{
    public static class ResourcesLoader
    {
        private const string dataResourcesPath = @"Data/";              // Dataã�� path
        private const string spriteResourcesPath = @"Sprite/";          // Sprite�� ã�� path
        private const string AnimationResourcesPath = @"Animation/";    // Animation�� ã�� path

        // ��� �����͸� �ε� �Ѵ�.
        public static void LoadAllData(Dictionary<int, Data> dataDic)
        {
            LoadData<UnitData>(dataDic, dataResourcesPath + Constant.unitDataJsonName);
            LoadData<ActiveSkillData>(dataDic, dataResourcesPath + Constant.activeSkillJsonName);
            LoadData<PassiveSkillData>(dataDic, dataResourcesPath + Constant.passiveSkillJsonName);
            LoadData<ConditionData>(dataDic, dataResourcesPath + Constant.conditionDataJsonName);
            LoadData<MapData>(dataDic, dataResourcesPath + Constant.mapDataJsonName);
            LoadData<StageData>(dataDic, dataResourcesPath + Constant.stageDataJsonName);
            LoadData<ConsumableItemData>(dataDic, dataResourcesPath +Constant.consumableItemDataJsonName);
        }

        // �����Ͱ� �ƴ� ��� ���ҽ��� �ε��Ѵ�.
        public static void LoadAllResource(Dictionary<string, Sprite> spriteDic, Dictionary<string, RuntimeAnimatorController> animDic)
        {
            var sprites = Resources.LoadAll<Sprite>(spriteResourcesPath);
            // ��������Ʈ �ε�
            foreach (var sprite in sprites)
            {
                spriteDic.Add(sprite.name, sprite);
            }

            var animations = Resources.LoadAll<RuntimeAnimatorController>(AnimationResourcesPath);
            // �ִϸ��̼� �ε�
            foreach (var anim in animations)
            {
                animDic.Add(anim.name, anim);
            }
        }


        // ORDER : JSON�� Data Ŭ������ ��ȯ �� �� Dictionary �����ؼ� ������ ����
        // ������ Ÿ���� �ε��Ѵ�.
        private static void LoadData<T>(Dictionary<int, Data> dataDic, string jsonPath) where T : Data
        {
            // JSON ������ �ε� �ϱ����� TextAsset Ÿ������ �ε��Ѵ�.
            var json = Resources.Load<TextAsset>(jsonPath);
            // ������ TextAsset�� �����ͼ� DataŸ������ ������ȭ �Ѵ�.
            var datas = JsonConvert.DeserializeObject<T[]>(json.text);

            // ������ȭ�� DataŸ���� Dic�� �־��ش�.
            foreach (var data in datas)
            {
                dataDic.Add(data.ID ,data);
            }
        }
    }
}