using Portfolio;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.skill;
using System;
using Portfolio.condition;
using Portfolio.Battle;
using Portfolio.UI;

/*
 * ���� �Ŵ��� Ŭ����
 */

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

        private static UIManager uiManager;
        public static UIManager UIManager { get => uiManager; }

        //===========================================================
        // Dictionary
        //===========================================================
        private Dictionary<int, Data> dataDictionary = new Dictionary<int, Data>();                                                         // ���� ������ Dic
        private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();                                             // ���� �̹��� Dic
        private Dictionary<int, Unit> unitDictionary = new Dictionary<int, Unit>();                                                         // ���� ���� Dic
        private Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();                                                      // ���� ��ų Dic
        private Dictionary<int, Condition> conditionDictionary = new Dictionary<int, Condition>();                                          // ���� �����̻� Dic
        private Dictionary<int, Map> mapDictionary = new Dictionary<int, Map>();                                                            // ���� �� Dic
        private Dictionary<string, RuntimeAnimatorController> animationDictionary = new Dictionary<string, RuntimeAnimatorController>();    // ���� ���� �ִϸ����� Dic

        //===========================================================
        // TestValue
        //===========================================================
        [Header("TestValue")]
        public bool isTest;
        public bool ShowUserUI;

        //===========================================================
        // UserData
        //===========================================================
        public static User CurrentUser; // �������� ���� Ŭ����

        private void Awake()
        {
            // �̱��� Ŭ���� ����
            if (instance == null)
            {
                instance = this;
                itemCreator = GetComponentInChildren<ItemGenerator>();
                timeChecker = GetComponentInChildren<TimeChecker>();
                uiManager = GetComponentInChildren<UIManager>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            if (isTest)
            // �׽�Ʈ ���̶��
            {
                // ������ �� ���ҽ� �ҷ�����
                LoadResource();
                // �׽��Ϳ� ���� ������ �ҷ�����
                SaveManager.LoadUserData("tester", out UserData user);
                LoadUser(user);

                uiManager.ShowUserInfo();
            }

            uiManager.HideUserInfoCanvas();
        }


        private void Start()
        {
            if (!isTest)
            {
                Debug.LogWarning("GameManager Test");
            }
            else
            {
                if (ShowUserUI)
                {
                    uiManager.ShowUserInfoCanvas();
                }
                else
                {
                    uiManager.HideUserInfoCanvas();
                }
            }
        }

        private void OnApplicationQuit()
        {
            // ���� ����� ���� ������ ������ ���� �ð� �ֱ�
            if (CurrentUser != null)
            {
                CurrentUser.LastAccessTime = DateTime.Now;
                // ���� ���� ����
                SaveUser();
            }
        }

        public void LoadData()
        {
            LoadResource();
        }

        public void LoadUser(UserData userData)
        // ���� ������ ������ �����Ѵ�.
        {
            if (!userData.isNewUser)
            // �ű� ������ �ƴ� ���
            {
                CurrentUser = new User(userData);
                // ������ ���� �ð��� ���� �ð��� ���Ͽ� �������� ������Ų��.
                int timeCheck = (int)((CurrentUser.LastAccessTime - DateTime.Now).TotalSeconds * -1);
                CurrentUser.CurrentEnergy += (int)(timeCheck / Constant.energyChargeTime);
                timeChecker.energyChargeCount = (int)(timeCheck % Constant.energyChargeTime);
            }
            else
            // �ű� ���� ���
            {
                userData.isNewUser = false;
                CurrentUser = new User(userData);
                // ���� ����(��ũ) ����
                UnitData defaultUnitData;
                TryGetData(100, out defaultUnitData);
                UserUnitData defaultUserUnitData = new UserUnitData(defaultUnitData);
                Unit defaultUnit = new Unit(defaultUnitData, defaultUserUnitData);

                // ������ �ִ�� ä���ֱ�
                CurrentUser.CurrentEnergy = CurrentUser.MaxEnergy;

                // �Һ� ������ ����
                CurrentUser.AddNewUnit(defaultUnit);
                CurrentUser.AddConsumableItem(2000, 10);
                CurrentUser.AddConsumableItem(2001, 10);
                CurrentUser.AddConsumableItem(2002, 10);
                SaveUser();
            }
        }

        // ��� ���ҽ� �ε�
        private void LoadResource()
        {
            ResourcesLoader.LoadAllData(dataDictionary);
            ResourcesLoader.LoadAllResource(spriteDictionary, animationDictionary);
            CreateGameSource();
        }

        // ���� ���� ����
        public void SaveUser()
        {
            SaveManager.SaveUserData(CurrentUser.GetSaveUserData());
        }

        // ������ Ȯ��
        public bool HasData<T>(int ID) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                Debug.LogWarning("KeyValue is not Contains");
                return false;
            }

            if (!(dataDictionary[ID] is T))
            // T�� �ƴ϶��
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                return false;
            }

            return true;
        }

        // ������ �ޱ�
        public bool TryGetData<T>(int ID, out T data) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                Debug.LogWarning("KeyValue is not Contains");
                data = null;
                return false;
            }

            if (!(dataDictionary[ID] is T))
            // T�� �ƴ϶��
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                data = null;
                return false;
            }

            data = dataDictionary[ID] as T;
            return true;
        }

        public List<T> GetDatas<T>() where T : Data
            // ������ ����Ʈ ��������
        {
            // ������ Dic���� T�� Data�鸸 ��������
            var list = dataDictionary.Values.Where((data) => data is T).Select(kv => kv as T).ToList();

            return list;
        }

        // ���� ��������
        public bool TryGetUnit(int ID, out Unit unit)
        {
            if (!unitDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                Debug.LogWarning(ID + " is not Contains");
                unit = null;
                return false;
            }

            unit = unitDictionary[ID];
            return true;
        }

        // �����̻� ��������
        public bool TryGetCondition(int ID, out Condition condition)
        {
            if (!conditionDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                Debug.LogWarning(ID + " is not Contains");
                condition = null;
                return false;
            }

            condition = conditionDictionary[ID];
            return true;
        }

        // ��ų ��������
        public bool TryGetSkill<T>(int ID, out T skill) where T : Skill
        {
            if (!skillDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                skill = null;
                return false;
            }

            if (!(skillDictionary[ID] is T))
            // T�� �ƴ϶��
            {
                skill = null;
                return false;
            }

            skill = skillDictionary[ID] as T;
            return true;
        }

        // �� ��������
        public bool TryGetMap(int ID, out Map map)
        {
            if (!mapDictionary.ContainsKey(ID))
            // Ű�� ���ٸ�
            {
                map = null;
                return false;
            }

            map = mapDictionary[ID];
            return true;
        }

        // ��������Ʈ ��������
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

        // �ִϸ����� ��������(��Ÿ���� �ִϸ����͸� �����ؾ��ϱ⿡ RuntimeAnimatorController�� �����´�.)
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

        // ���� �����ϴ��� Ȯ��
        public bool CheckContainsMap(int ID)
        {
            return mapDictionary.ContainsKey(ID);
        }

        #region �����͸� �������� ��ȯ

        // Ŭ���� ������ ������ ���ε�
        private void CreateGameSource()
        {
            LoadCondition();
            LoadSkill();
            LoadUnit();
            LoadMap();
        }


        // ���� �����ͷ� ���� Ŭ���� ����
        private void LoadUnit()
        {
            foreach (var data in GetDatas<UnitData>())
            {
                unitDictionary.Add(data.ID, new Unit((UnitData)data));
            }
        }

        // ORDER : C# ���÷����� �̿��ؼ� ��ų, �����̻� Ŭ���� ��Ÿ�� ���� �� ������ ����
        // ��ų �����ͷ� ��ų Ŭ���� ����
        private void LoadSkill()
        {
            foreach (var data in GetDatas<SkillData>())
            {
                SkillData skillData = (data as SkillData);
                // �������� ��ų Ŭ���� �̸����� ��ų Ŭ���� Ÿ�� ��������
                var type = Type.GetType("Portfolio.skill." + (data as SkillData).skillClassName);
                // ���� Ŭ����
                object obj = null;
                switch (skillData.skillType)
                {
                    case SkillType.ActiveSkill:
                        {
                            // ���� Ÿ������ Ŭ������ ��Ÿ�� ������ �� ��ų Dic �� �־��ش�.
                            obj = Activator.CreateInstance(type, skillData as ActiveSkillData);
                            skillDictionary.Add(data.ID, obj as ActiveSkill);
                        }
                        break;
                    case SkillType.PassiveSkill:
                        {
                            // ���� Ÿ������ Ŭ������ ��Ÿ�� ������ �� ��ų Dic �� �־��ش�.
                            obj = Activator.CreateInstance(type, skillData as PassiveSkillData);
                            skillDictionary.Add(data.ID, obj as PassiveSkill);
                        }
                        break;
                }
            }
        }

        // �����̻� �����ͷ� �����̻� Ŭ���� ����
        private void LoadCondition()
        {
            foreach (var data in GetDatas<ConditionData>())
            {
                // �������� Ŭ���� �̸����� �����̻� Ŭ���� Ÿ�� ��������
                var type = Type.GetType("Portfolio.condition." + (data as ConditionData).conditionClassName);
                // ���� Ÿ������ Ŭ������ ��Ÿ�� ������ �� �����̻� Dic �� �־��ش�.
                object obj = Activator.CreateInstance(type, data as ConditionData);
                conditionDictionary.Add(data.ID, obj as Condition);
            }
        }

        // �� �����ͷ� �� Ŭ���� ����
        private void LoadMap()
        {
            foreach (var data in GetDatas<MapData>())
            {
                mapDictionary.Add(data.ID, new Map(data));
            }
        }
        #endregion

        // ���� ���� ��ư
        public void BTN_OnClick_GameQuit()
        {
            UIManager.ShowConfirmation("���� ����", "������ ������ �����Ͻðڽ��ϱ�?", () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
        }

        // �α׾ƿ� ��ư
        public void BTN_OnClick_Logout()
        {
            UIManager.ShowConfirmation("�α׾ƿ�", "������ �α׾ƿ� �Ͻðڽ��ϱ�?", () =>
            {
                SceneLoader.LoadStartScene();
            });
        }
    }
}