using Portfolio;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.skill;
using System;
using Portfolio.condition;

namespace Portfolio
{
    public class GameManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static GameManager instance;
        public static GameManager Instance { get => instance; }

        private static ItemGenerator itemCreator;
        public static ItemGenerator ItemCreator { get => itemCreator; }

        private static TimeChecker timeChecker;
        public static TimeChecker TimeChecker { get => timeChecker; }

        //===========================================================
        // Dictionary
        //===========================================================
        private Dictionary<int, Data> dataDictionary = new Dictionary<int, Data>();
        private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
        private Dictionary<int, Unit> unitDictionary = new Dictionary<int, Unit>();
        private Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();
        private Dictionary<int, Condition> conditionDictionary = new Dictionary<int, Condition>();
        private Dictionary<int, Map> mapDictionary = new Dictionary<int, Map>();
        private Dictionary<string, RuntimeAnimatorController> animationDictionary = new Dictionary<string, RuntimeAnimatorController>();

        public bool isTest;

        //===========================================================
        // UserData
        //===========================================================
        public static User CurrentUser;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                itemCreator = GetComponentInChildren<ItemGenerator>();
                timeChecker = GetComponentInChildren<TimeChecker>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            ResourcesLoader.LoadAllData(dataDictionary);
            ResourcesLoader.LoadAllResource(spriteDictionary, animationDictionary);
            //Debug.Log(spriteDictionary.Count);
            CreateGameSource();

            if (SaveManager.LoadUserData(out UserData CurrentUserData))
            {
                CurrentUser = new User(CurrentUserData);
            }
            else
            {
                CurrentUser = new User(SaveManager.CreateNewUser());
                UnitData defaultUnitData;
                TryGetData(100, out defaultUnitData);
                UserUnitData defaultUserUnitData = new UserUnitData(defaultUnitData);
                Unit defaultUnit = new Unit(defaultUnitData, defaultUserUnitData);
                CurrentUser.AddNewUnit(defaultUnit);
                CurrentUser.AddConsumableItem(2000, 10);
                CurrentUser.AddConsumableItem(2001, 10);
                CurrentUser.AddConsumableItem(2002, 10);
                SaveUser();
            }
        }

        private void Start()
        {
            if (!isTest)
            {
                Debug.LogWarning("GameManager Test");
                SceneLoader.LoadLobbyScene();
            }
        }

        public void SaveUser()
        {
            SaveManager.SaveUserData(CurrentUser.GetSaveUserData());
        }

        public bool IsData<T>(int ID) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            {
                Debug.LogWarning("KeyValue is not Contains");
                return false;
            }

            if (!(dataDictionary[ID] is T))
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                return false;
            }

            return true;
        }

        public bool TryGetData<T>(int ID, out T data) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            {
                Debug.LogWarning("KeyValue is not Contains");
                data = null;
                return false;
            }

            if (!(dataDictionary[ID] is T))
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                data = null;
                return false;
            }

            data = dataDictionary[ID] as T;
            return true;
        }

        public List<T> GetDatas<T>() where T : Data
        {
            var list = dataDictionary.Values.Where((data) => data is T).Select(kv => kv as T).ToList();

            return list;
        }

        public bool TryGetUnit(int ID, out Unit unit)
        {
            if (!unitDictionary.ContainsKey(ID))
            {
                Debug.LogWarning(ID + " is not Contains");
                unit = null;
                return false;
            }

            unit = unitDictionary[ID];
            return true;
        }

        public bool TryGetCondition(int ID, out Condition condition)
        {
            if (!conditionDictionary.ContainsKey(ID))
            {
                Debug.LogWarning(ID + " is not Contains");
                condition = null;
                return false;
            }

            condition = conditionDictionary[ID];
            return true;
        }

        public bool TryGetSkill<T>(int ID, out T skill) where T : Skill
        {
            if (!skillDictionary.ContainsKey(ID))
            {
                //Debug.LogWarning(ID + " is not Contains");
                skill = null;
                return false;
            }

            if (!(skillDictionary[ID] is T))
            {
                //Debug.LogWarning("Value is not " + typeof(T).Name);
                skill = null;
                return false;
            }

            skill = skillDictionary[ID] as T;
            return true;
        }

        public bool TryGetMap(int ID, out Map map)
        {
            if (!mapDictionary.ContainsKey(ID))
            {
                map = null;
                return false;
            }

            map = mapDictionary[ID];
            return true;
        }

        #region �����͸� �������� ��ȯ

        private void CreateGameSource()
        {
            LoadSkill();
            LoadUnit();
            LoadCondition();
            LoadMap();
        }


        private void LoadUnit()
        {
            foreach (var data in GetDatas<UnitData>())
            {
                unitDictionary.Add(data.ID, new Unit((UnitData)data));
            }
        }

        private void LoadSkill()
        {
            foreach (var data in GetDatas<SkillData>())
            {
                SkillData skillData = (data as SkillData);
                //Debug.Log((data as SkillData).skillClassName);
                var type = Type.GetType("Portfolio.skill." + (data as SkillData).skillClassName);
                //Debug.Log(type);
                object obj = null;
                switch (skillData.skillType)
                {
                    case SkillType.ActiveSkill:
                        {
                            obj = Activator.CreateInstance(type, skillData as ActiveSkillData);
                            skillDictionary.Add(data.ID, obj as ActiveSkill);
                        }
                        break;
                    case SkillType.PassiveSkill:
                        {
                            obj = Activator.CreateInstance(type, skillData as PassiveSkillData);
                            skillDictionary.Add(data.ID, obj as PassiveSkill);
                        }
                        break;
                }
            }
        }

        private void LoadCondition()
        {
            foreach (var data in GetDatas<ConditionData>())
            {
                var type = Type.GetType("Portfolio.condition." + (data as ConditionData).conditionClassName);
                object obj = Activator.CreateInstance(type, data as ConditionData);
                conditionDictionary.Add(data.ID, obj as Condition);
            }
        }

        private void LoadMap()
        {
            foreach (var data in GetDatas<MapData>())
            {
                mapDictionary.Add(data.ID, new Map(data));
            }
        }


        public Sprite GetSprite(string spriteName)
        {
            if (spriteDictionary.ContainsKey(spriteName))
            {
                return spriteDictionary[spriteName];
            }
            else
            {
                Debug.LogWarning(spriteName + " Sprite is null");
                return null;
            }
        }

        public RuntimeAnimatorController GetAnimController(string animName)
        {
            if (animationDictionary.ContainsKey(animName))
            {
                return animationDictionary[animName];
            }
            else
            {
                Debug.LogWarning(animName + " Anim is null");
                return null;
            }
        }
        #endregion
    }
}