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
 * 리소스를로드하는 함수를 모아놓은 static 클래스
 */

namespace Portfolio
{
    public static class ResourcesLoader
    {
        private const string dataResourcesPath = @"Data/";              // Data 리소스를 찾는 path
        private const string spriteResourcesPath = @"Sprite/";          // Sprite 리소스를 찾는 path
        private const string animationResourcesPath = @"Animation/";    // Animation 리소스를 찾는 path
        private const string audioResourcesPath = @"Audio/";            // Audio 리소스를 찾는 path

        // 모든 데이터를 로드 한다.
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

        // 데이터가 아닌 모든 리소스를 로드한다.
        public static void LoadAllResource(Dictionary<string, Sprite> spriteDic, Dictionary<string, RuntimeAnimatorController> animDic, Dictionary<string, AudioClip> audioDic)
        {
            var sprites = Resources.LoadAll<Sprite>(spriteResourcesPath);
            // 스프라이트 로드
            foreach (var sprite in sprites)
            {
                spriteDic.Add(sprite.name, sprite);
            }

            var animations = Resources.LoadAll<RuntimeAnimatorController>(animationResourcesPath);
            // 애니메이션 로드
            foreach (var anim in animations)
            {
                animDic.Add(anim.name, anim);
            }

            var audioClips = Resources.LoadAll<AudioClip>(audioResourcesPath);
            // 모든 오디오클립을 로드
            foreach (var clip in audioClips)
            {
                audioDic.Add(clip.name, clip);
            }
        }


        // ORDER : #4) JSON을 Data 클래스로 변환 한 후 Dictionary 저장해서 데이터 관리
        // 데이터 타입을 로드한다.
        private static void LoadData<T>(Dictionary<int, Data> dataDic, string jsonPath) where T : Data
        {
            // JSON 파일을 로드 하기위해 TextAsset 타입으로 로드한다.
            var json = Resources.Load<TextAsset>(jsonPath);
            // 가져온 TextAsset를 가져와서 Data타입으로 역직렬화 한다.
            var datas = JsonConvert.DeserializeObject<T[]>(json.text);

            // 역직렬화한 Data타입을 Dic에 넣어준다.
            foreach (var data in datas)
            {
                dataDic.Add(data.ID, data);
            }
        }
    }
}