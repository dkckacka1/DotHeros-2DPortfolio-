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
 * 게임 매니저 클래스
 */

namespace Portfolio
{
    public class GameManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static GameManager instance;
        public static GameManager Instance => instance; 

        // 아이템 생성기
        private static ItemGenerator itemCreator;
        public static ItemGenerator ItemCreator => itemCreator; 

        // 시간 확인자
        private static TimeChecker timeChecker;
        public static TimeChecker TimeChecker => timeChecker; 

        // 게임 UI 매니저
        private static UIManager uiManager;
        public static UIManager UIManager => uiManager; 

        // 오디오 관리자
        private static AudioManager audioManager;
        public static AudioManager AudioManager => audioManager;

        // 네트워크 관리자
        private static NetworkManager networkManager;
        public static NetworkManager NetworkManager => networkManager;

        //===========================================================
        // Dictionary
        //===========================================================
        private Dictionary<int, Data> dataDictionary = new Dictionary<int, Data>();                                                         // 게임 데이터 Dic
        private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();                                             // 게임 이미지 Dic
        private Dictionary<int, Unit> unitDictionary = new Dictionary<int, Unit>();                                                         // 게임 유닛 Dic
        private Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();                                                      // 게임 스킬 Dic
        private Dictionary<int, Condition> conditionDictionary = new Dictionary<int, Condition>();                                          // 게임 상태이상 Dic
        private Dictionary<int, Map> mapDictionary = new Dictionary<int, Map>();                                                            // 게임 맵 Dic
        private Dictionary<string, RuntimeAnimatorController> animationDictionary = new Dictionary<string, RuntimeAnimatorController>();    // 게임 유닛 애니메이터 Dic
        private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();                                        // 게임 오디오 Dic

        [HideInInspector] public bool isLoaded = false;  // 데이터와 리소스가 로드되었는지 확인

        //===========================================================
        // TestValue
        //===========================================================
        [Header("TestValue")]
        public bool isTest;
        public bool ShowUserUI;

        //===========================================================
        // UserData
        //===========================================================
        public static User CurrentUser; // 접속중인 유저 클래스

        private void Awake()
        {
            // 싱글턴 클래스 설정
            if (instance == null)
            {
                instance = this;
                itemCreator = GetComponentInChildren<ItemGenerator>();
                timeChecker = GetComponentInChildren<TimeChecker>();
                uiManager = GetComponentInChildren<UIManager>();
                audioManager = GetComponentInChildren<AudioManager>();
                networkManager = GetComponentInChildren<NetworkManager>();

                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            // 파이어베이스와 연동을 실시합니다.
            networkManager.CheckVaildFirebase();

            if (isTest)
            // 테스트 중이라면
            {
                // 데이터 및 리소스 불러오기
                LoadResource();
                // 테스터용 계정 데이터 불러오기
                SLManager.LoadUserData("tester", out UserData user);
                LoadUser(user);

                uiManager.ShowUserInfo();
            }

            uiManager.HideUserInfoCanvas();
            // 게임 기본 환경을 설정합니다.
            SetDefualtConfigure();
        }



        private void Start()
        {

            if (isTest)
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
            // 게임 종료시 유저 정보에 마지막 접속 시간 넣기
            if (CurrentUser != null)
            {
                // 유저 정보 저장
                SaveUser();
            }
        }

        public void LoadData()
        {
            LoadResource();
        }

        public void LoadUser(UserData userData)
        // 계정 정보로 유저를 생성한다.
        {
            if (!userData.isNewUser)
            // 신규 유저가 아닐 경우
            {
                CurrentUser = new User(userData);
                // 마지막 접속 시간과 현재 시간을 비교하여 에너지를 충전시킨다.
                int timeCheck = (int)((CurrentUser.LastAccessTime - DateTime.Now).TotalSeconds * -1);
                CurrentUser.CurrentEnergy += (int)(timeCheck / Constant.EnergyChargeTime);
                timeChecker.energyChargeCount = (int)(timeCheck % Constant.EnergyChargeTime);
            }
            else
            // 신규 유저 라면
            {
                userData.isNewUser = false;
                CurrentUser = new User(userData);
                // 최초 유닛(지크) 지급
                UnitData defaultUnitData;
                TryGetData(100, out defaultUnitData);
                UserUnitData defaultUserUnitData = new UserUnitData(defaultUnitData);
                Unit defaultUnit = new Unit(defaultUnitData, defaultUserUnitData);

                // 에너지 최대로 채워주기
                CurrentUser.CurrentEnergy = CurrentUser.MaxEnergy;

                // 기본 소비 아이템 지급
                CurrentUser.AddNewUnit(defaultUnit);
                CurrentUser.AddConsumableItem(2000, 10);
                CurrentUser.AddConsumableItem(2001, 10);
                CurrentUser.AddConsumableItem(2002, 10);
                // SAVE : 
                //SaveUser();
            }
        }

        // 모든 리소스 로드
        private void LoadResource()
        {
            ResourcesLoader.LoadAllData(dataDictionary);
            ResourcesLoader.LoadAllResource(spriteDictionary, animationDictionary, audioDictionary);
            CreateGameSource();
        }

        // 유저 정보 저장
        public void SaveUser()
        {
            CurrentUser.LastAccessTime = DateTime.Now;
            SLManager.SaveUserData(CurrentUser.GetSaveUserData());
        }

        // 데이터 확인
        public bool HasData<T>(int ID) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                Debug.LogWarning("KeyValue is not Contains");
                return false;
            }

            if (!(dataDictionary[ID] is T))
            // T가 아니라면
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                return false;
            }

            return true;
        }

        // 데이터 받기
        public bool TryGetData<T>(int ID, out T data) where T : Data
        {
            if (!dataDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                Debug.LogWarning("KeyValue is not Contains");
                data = null;
                return false;
            }

            if (!(dataDictionary[ID] is T))
            // T가 아니라면
            {
                Debug.LogWarning("Value is not " + typeof(T).Name);
                data = null;
                return false;
            }

            data = dataDictionary[ID] as T;
            return true;
        }

        public List<T> GetDatas<T>() where T : Data
            // 데이터 리스트 가져오기
        {
            // 데이터 Dic에서 T인 Data들만 가져오기
            var list = dataDictionary.Values.Where((data) => data is T).Select(kv => kv as T).ToList();

            return list;
        }

        // 유닛 가져오기
        public bool TryGetUnit(int ID, out Unit unit)
        {
            if (!unitDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                Debug.LogWarning(ID + " is not Contains");
                unit = null;
                return false;
            }

            unit = unitDictionary[ID];
            return true;
        }

        // 상태이상 가져오기
        public bool TryGetCondition(int ID, out Condition condition)
        {
            if (!conditionDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                Debug.LogWarning(ID + " is not Contains");
                condition = null;
                return false;
            }

            condition = conditionDictionary[ID];
            return true;
        }

        // 스킬 가져오기
        public bool TryGetSkill<T>(int ID, out T skill) where T : Skill
        {
            if (!skillDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                skill = null;
                return false;
            }

            if (!(skillDictionary[ID] is T))
            // T가 아니라면
            {
                skill = null;
                return false;
            }

            skill = skillDictionary[ID] as T;
            return true;
        }

        // 맵 가져오기
        public bool TryGetMap(int ID, out Map map)
        {
            if (!mapDictionary.ContainsKey(ID))
            // 키가 없다면
            {
                map = null;
                return false;
            }

            map = mapDictionary[ID];
            return true;
        }

        // 스프라이트 가져오기
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

        // 애니메이터 가져오기(런타임중 애니메이터를 변경해야하기에 RuntimeAnimatorController로 가져온다.)
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

        public AudioClip GetAudioClip(string clipName)
        {
            if (audioDictionary.ContainsKey(clipName))
            {
                return audioDictionary[clipName];
            }
            else
            {
                Debug.LogWarning(clipName + " Audio is null");
                return null;
            }
        }

        // 맵이 존재하는지 확인
        public bool CheckContainsMap(int ID)
        {
            return mapDictionary.ContainsKey(ID);
        }

        #region 데이터를 형식으로 변환

        // 클래스 생성후 데이터 바인딩
        private void CreateGameSource()
        {
            LoadCondition();
            LoadSkill();
            LoadUnit();
            LoadMap();
        }


        // 유닛 데이터로 유닛 클래스 생성
        private void LoadUnit()
        {
            foreach (var data in GetDatas<UnitData>())
            {
                unitDictionary.Add(data.ID, new Unit((UnitData)data));
            }
        }

        // ORDER : #5) C# 리플렉션을 이용해서 스킬, 상태이상 클래스 런타임 생성 후 데이터 관리
        // 스킬 데이터로 스킬 클래스 생성
        private void LoadSkill()
        {
            foreach (var data in GetDatas<SkillData>())
            {
                SkillData skillData = (data as SkillData);
                // 데이터의 스킬 클래스 이름으로 스킬 클래스 타입 가져오기
                var type = Type.GetType("Portfolio.skill." + (data as SkillData).skillClassName);
                // 만들 클래스
                object obj = null;
                switch (skillData.skillType)
                {
                    case eSkillType.ActiveSkill:
                        {
                            // 만든 타입으로 클래스를 런타임 생성한 후 스킬 Dic 에 넣어준다.
                            obj = Activator.CreateInstance(type, skillData as ActiveSkillData);
                            skillDictionary.Add(data.ID, obj as ActiveSkill);
                        }
                        break;
                    case eSkillType.PassiveSkill:
                        {
                            // 만든 타입으로 클래스를 런타임 생성한 후 스킬 Dic 에 넣어준다.
                            obj = Activator.CreateInstance(type, skillData as PassiveSkillData);
                            skillDictionary.Add(data.ID, obj as PassiveSkill);
                        }
                        break;
                    default:
                        Debug.LogWarning("unknownType");
                        break;
                }
            }
        }

        // 상태이상 데이터로 상태이상 클래스 생성
        private void LoadCondition()
        {
            foreach (var data in GetDatas<ConditionData>())
            {
                // 데이터의 클래스 이름으로 상태이상 클래스 타입 가져오기
                var type = Type.GetType("Portfolio.condition." + (data as ConditionData).conditionClassName);
                // 만든 타입으로 클래스를 런타임 생성한 후 상태이상 Dic 에 넣어준다.
                object obj = Activator.CreateInstance(type, data as ConditionData);
                conditionDictionary.Add(data.ID, obj as Condition);
            }
        }

        // 맵 데이터로 맵 클래스 생성
        private void LoadMap()
        {
            foreach (var data in GetDatas<MapData>())
            {
                mapDictionary.Add(data.ID, new Map(data));
            }
        }
        #endregion



        // 게임 기본 환경설정
        private void SetDefualtConfigure()
        {
            // 이 게임의 기본 프레임은 60
            Application.targetFrameRate = Constant.GameDefualtFrame;
            LoadScreenConfigureData();
        }

        // 화면 설정 값을 가져와 세팅합니다.
        private void LoadScreenConfigureData()
        {
            Screen.fullScreenMode = (PlayerPrefs.GetInt("FullScreenMode", 1) == 1) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            int screenResolutionSize = (PlayerPrefs.GetInt("ScreenSize", 0));
            var currentResolution = Constant.resolutions[screenResolutionSize];
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreenMode);
        }

        // 게임 종료 버튼
        public void BTN_OnClick_GameQuit()
        {
            UIManager.ShowConfirmation("게임 종료", "정말로 게임을 종료하시겠습니까?", () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
        }

        // 로그아웃 버튼
        public void BTN_OnClick_Logout()
        {
            UIManager.ShowConfirmation("로그아웃", "정말로 로그아웃 하시겠습니까?", () =>
            {
                // 유저 정보를 저장하고 로그아웃 합니다.
                SaveUser();
                networkManager.Logout();
                SceneLoader.LoadStartScene();
            });
        }

        // 전체화면 모드를 설정합니다.
        public void Toggle_OnValueChaned_SetWindowScreenMode(bool isOn)
        {
            Screen.fullScreenMode = isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            PlayerPrefs.SetInt("FullScreenMode", isOn ? 1 : 0);
        }

        // 해상도를 더 높게 설정합니다.
        public void BTN_OnClick_SetHigherResolution()
        {
            int screenResolutionSize = (PlayerPrefs.GetInt("ScreenSize", 0));
            // 가장 높은 사이즈면 리턴
            if (screenResolutionSize == 0) return;

            screenResolutionSize--;
            var currentResolution = Constant.resolutions[screenResolutionSize];
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt("ScreenSize", screenResolutionSize);
        }

        // 해상도를 너 낮게 설정합니다.
        public void BTN_OnClick_SetLowerResolution()
        {
            int screenResolutionSize = (PlayerPrefs.GetInt("ScreenSize", 0));
            // 가장 낮은 사이즈면 리턴
            if (screenResolutionSize == Constant.resolutions.Length - 1) return;

            screenResolutionSize++;
            var currentResolution = Constant.resolutions[screenResolutionSize];
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt("ScreenSize", screenResolutionSize);
        }
    }
}