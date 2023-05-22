using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class GameManager : MonoBehaviour
    {

        private Dictionary<int, Data> DataDictionary = new Dictionary<int, Data>();

        private static GameManager instance;

        public static GameManager Instance { get => instance; }

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
        }

        private void Start()
        {
            Debug.Log(ResourcesLoader.TryLoadSkillData(ref DataDictionary));
        }
    }

}