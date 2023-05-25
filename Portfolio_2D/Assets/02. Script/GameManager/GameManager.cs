using Portfolio;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.skill;

namespace Portfolio
{
    public class GameManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static GameManager instance;
        public static GameManager Instance { get => instance; }

        //===========================================================
        // Dictionary
        //===========================================================
        private Dictionary<int, Data> dataDictionary = new Dictionary<int, Data>();
        private Dictionary<int, Unit> unitDictionary = new Dictionary<int, Unit>();
        private Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();

        public bool isTest;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            ResourcesLoader.LoadAllData(dataDictionary);
            LoadSkill();
            LoadUnit();
        }

        private void Start()
        {
            if (!isTest)
            {
                Debug.Log("GameManager Test");
            }
        }

        public bool TryGetData<T>(int ID, out T data) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            {
                Debug.Log("KeyValue is not Contains");
                data = null;
                return false;
            }

            if (!(dataDictionary[ID] is T))
            {
                Debug.Log("Value is not " + typeof(T).Name);
                data = null;
                return false;
            }

            data = dataDictionary[ID] as T;
            return true;
        }

        public List<Data> GetDatas<T>() where T : Data
        {
            var list = dataDictionary.Values.Where((data) => data is T).ToList();

            return list;
        }

        public bool TryGetUnit(int ID, out Unit unit)
        {
            if (!unitDictionary.ContainsKey(ID))
            {
                Debug.Log(ID + " is not Contains");
                unit = null;
                return false;
            }

            unit = unitDictionary[ID];
            return true;
        }

        public bool TryGetSkill<T>(int ID, out T skill) where T : Skill
        {
            if (!skillDictionary.ContainsKey(ID))
            {
                Debug.Log(ID + " is not Contains");
                skill = null;
                return false;
            }

            if (!(skillDictionary[ID] is T))
            {
                Debug.Log("Value is not " + typeof(T).Name);
                skill = null;
                return false;
            }

            skill = skillDictionary[ID] as T;
            return true;
        }

        #region LoadSystem

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
                switch (((SkillData)data).skillType)
                {
                    case SkillType.ActiveSkill:
                        {
                            skillDictionary.Add(data.ID, new ActiveSkill((ActiveSkillData)data));
                        }
                        break;
                    case SkillType.PassiveSkill:
                        {
                            skillDictionary.Add(data.ID, new PassiveSkill((PassiveSkillData)data));
                        }
                        break;
                }
            }
        } 
        #endregion
    }
}