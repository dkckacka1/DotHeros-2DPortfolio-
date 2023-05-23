using Portfolio.skill.Option;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class GameManager : MonoBehaviour
    {
        //===========================================================
        // Dictionary
        //===========================================================
        private Dictionary<int, Data> DataDictionary = new Dictionary<int, Data>();

        private static GameManager instance;
        public static GameManager Instance { get => instance; }

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

            ResourcesLoader.TryLoadSkillData(DataDictionary);
            ResourcesLoader.TryLoadUnitData(DataDictionary);
            Debug.Log(TryGetData(10000, out SkillData data1));
            Debug.Log(TryGetData(100, out UnitData data2));
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
            if (!DataDictionary.ContainsKey(ID))
            {
                Debug.Log("Key is not Contains");
                data = null;
                return false;
            }
            else
            {
                if (!(DataDictionary[ID] is T))
                {
                    Debug.Log("Value is not " + typeof(T).Name);
                    data = null;
                    return false;
                }
                else
                {
                    data = DataDictionary[ID] as T;
                }
            }

            return true;
        }

        public List<T> GetDataList<T>() where T : Data
        {
            var keyValueList = DataDictionary.Where((item) => item.Value is T).ToList();

            List<T> dataList = new List<T>();
            foreach (var list in keyValueList)
            {
                dataList.Add(list.Value as T);
            }

            return dataList;
        }
    }
}