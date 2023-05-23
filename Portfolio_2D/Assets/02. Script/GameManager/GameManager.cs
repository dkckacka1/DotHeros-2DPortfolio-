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
        private Dictionary<int, Skill> SkillDictionary = new Dictionary<int, Skill>();
        private Dictionary<int, Unit> UnitDictionary = new Dictionary<int, Unit>();

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

            ResourcesLoader.TryLoadSkillData(SkillDictionary);
            ResourcesLoader.TryLoadUnitData(UnitDictionary);

            foreach (var unitKV in UnitDictionary)
            {
                Debug.Log(unitKV.Value.ToString());
            }
        }

        private void Start()
        {
            if (!isTest)
            {
                Debug.Log("GameManager Test");
            }
        }

        public bool TryGetUnit(int id, out Unit skill)
        {
            if (!UnitDictionary.ContainsKey(id))
            {
                Debug.Log("Unit ID is not ContainsKey");

                skill = null;
                return false;
            }

            skill = UnitDictionary[id];
            return true;
        }

        public bool TryGetSkill(int id, out Skill skill)
        {
            if (!SkillDictionary.ContainsKey(id))
            {
                Debug.Log("skill ID is not ContainsKey");

                skill = null;
                return false;
            }

            skill = SkillDictionary[id];
            return true;
        }
    }
}