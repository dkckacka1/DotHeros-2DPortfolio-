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
        private const string dataResourcesPath = @"Data/";              // Data ���ҽ��� ã�� path
        private const string spriteResourcesPath = @"Sprite/";          // Sprite ���ҽ��� ã�� path
        private const string animationResourcesPath = @"Animation/";    // Animation ���ҽ��� ã�� path
        private const string audioResourcesPath = @"Audio/";            // Audio ���ҽ��� ã�� path

        // ��� �����͸� �ε� �Ѵ�.
        public static void LoadAllData(Dictionary<int, Data> dataDic)
        {
            LoadData<UnitData>(dataDic, dataResourcesPath + Constant.UnitDataJsonName);
            LoadData<ActiveSkillData>(dataDic, dataResourcesPath + Constant.ActiveSkillJsonName);
            LoadData<PassiveSkillData>(dataDic, dataResourcesPath + Constant.PassiveSkillJsonName);
            LoadData<ConditionData>(dataDic, dataResourcesPath + Constant.ConditionDataJsonName);
            LoadData<MapData>(dataDic, dataResourcesPath + Constant.MapDataJsonName);
            LoadData<StageData>(dataDic, dataResourcesPath + Constant.StageDataJsonName);
            LoadData<ConsumableItemData>(dataDic, dataResourcesPath + Constant.ConsumableItemDataJsonName);
        }

        // �����Ͱ� �ƴ� ��� ���ҽ��� �ε��Ѵ�.
        public static void LoadAllResource(Dictionary<string, Sprite> spriteDic, Dictionary<string, RuntimeAnimatorController> animDic, Dictionary<string, AudioClip> audioDic)
        {
            var sprites = Resources.LoadAll<Sprite>(spriteResourcesPath);
            // ��������Ʈ �ε�
            foreach (var sprite in sprites)
            {
                spriteDic.Add(sprite.name, sprite);
            }

            var animations = Resources.LoadAll<RuntimeAnimatorController>(animationResourcesPath);
            // �ִϸ��̼� �ε�
            foreach (var anim in animations)
            {
                animDic.Add(anim.name, anim);
            }

            var audioClips = Resources.LoadAll<AudioClip>(audioResourcesPath);
            // ��� �����Ŭ���� �ε�
            foreach (var clip in audioClips)
            {
                audioDic.Add(clip.name, clip);
            }
        }


        // ORDER : #4) JSON�� Data Ŭ������ ��ȯ �� �� Dictionary �����ؼ� ������ ����
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
                dataDic.Add(data.ID, data);
            }
        }
    }
}