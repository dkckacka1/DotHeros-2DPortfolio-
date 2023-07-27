도트 히어로즈
===


게임 장르 : 2D 턴제 RPG
---

개발 목적 : 즐겨 했던 턴제 RPG 게임 기능 직접 구현
---

사용 엔진 : UNITY 2021.3.25f1
---


개발 기간 : 10주 (2023.05.12 ~ 2023.07.23)
---


포트폴리오 영상
---
[유튜브 영상 링크](https://www.youtube.com/watch?v=ZACqozWcwWE&ab_channel=%EB%85%B8%EC%96%B4%EC%9D%B4)


빌드 파일
---
[구들 드라이브 다운로드 링크](https://drive.google.com/file/d/1AINMf5aS2t6UDCPbDsIh2ihyqnkEpRmg/view?usp=sharing)


사용 서드파티
---
* ExcelDataReader
* NewtonJSON
* google Firebase
* Dotween
* TextMeshPro


주요 활용 기술
---
* #01)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/BattleTargetSetExtensions.cs)) [확장 메서드, 메서드 체이닝을 이용해서 전투 대상을 찾는 함수 구현](https://copractice.tistory.com/47)

<details>
<summary>예시 코드</summary>
  
```csharp
public override IEnumerable<BattleUnit> SetTarget(BattleUnit actionUnit, List<GridPosition> targetUnits)
{
    // 적군에서 가장 체력 낮은 적을 타겟으로 잡음
    return targetUnits.GetEnemyTarget(actionUnit, this).OrderLowHealth().GetTargetNum(this).SelectBattleUnit();
}
```

</details>

---
* #02)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/Skill.cs)) [상속을 이용하여 만든 스킬 시스템 구현](https://copractice.tistory.com/60)

<details>
<summary>예시 UML</summary>

![SkillSystem](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/ab31661a-247f-41c9-a62b-4fc06d5780e7)


</details>

---
* #03)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/Editor/TableToJson.cs)) [ExcelDataReader 와 Newtonsoft.Json 을 활용해서 만든 데이터 테이블 JSON 변환 시스템 구현](https://copractice.tistory.com/61)

<details>
<summary>예시 코드</summary>
  
```csharp
 // 엑셀에서 읽은 정보를 토대로 JSON파일을 생성합니다.
  private static bool WriteJson(DataTableReader reader, int rowCount, string excelPath)
  {
      using (var writer = new JsonTextWriter(File.CreateText(Application.dataPath + Constant.ResorucesDataPath + excelPath + ".json")))
          // JSON 쓰기를 실행합니다.
      {
          // JSON 파일의 속성 이름을 가져옵니다.
          List<string> propertyList = new List<string>();
  
          // 첫번째 행을 읽어옵니다.
          reader.Read();
          for (int i = 0; i < rowCount; i++)
          {
              try
              {
                  // 첫번째 행은 변수의 이름이므로 속성리스트에 넣어줍니다.
                  propertyList.Add(reader.GetString(i));
              }
              catch (InvalidCastException)
              {
                  Debug.LogError("Invalid data type.");
                  return false;
              }
          }
  
          // JSON파일을 만들때 읽기 쉽게 만듭니다.
          writer.Formatting = Formatting.Indented;
          // JSON을 배열값으로 만듭니다.
          writer.WriteStartArray();
          do
          {
              while (reader.Read())
                  // 행을 읽습니다.
              {
                  writer.WriteStartObject();
                  // JSON의 요소를 만듭니다.
                  for (int i = 0; i < propertyList.Count; i++)
                  {
                      writer.WritePropertyName(propertyList[i]);
                      // 각 행열에 맞는 속성값을 가져옵니다.
                      if (int.TryParse(reader.GetValue(i).ToString(), out int intValue))
                          // 가져온 값이 int형인지 확인
                      {
                          // 값을 int형으로 넣어줍니다.
                          writer.WriteValue(intValue);
                      }
                      else if (bool.TryParse(reader.GetValue(i).ToString(), out bool boolValue))
                          // 가져온 값이 bool형인지 확인
                      {
                          // 값을 bool 형으로 넣어줍니다.
                          writer.WriteValue(boolValue);
                      }
                      else if (float.TryParse(reader.GetValue(i).ToString(), out float floatValue))
                          // 가져온 값이 float 형인지 확인
                      {
                          // 값을 float형으로 넣어줍니다.
                          writer.WriteValue(floatValue);
                      }
                      else
                      {
                          // 어떠한 것도 아니라면 문자열형태로 넣어줍니다.
                          writer.WriteValue(reader.GetString(i));
                      }
                  }
  
                  //배열에 요소를 채워 넣습니다.
                  writer.WriteEndObject();
              }
          }
          // 다음행이 존재하지 않을 때 까지 반복합니다
          while (reader.NextResult());
          // JSON 배열 작성을 종료합니다.
          writer.WriteEndArray();
          return true;
      }
  }
```

</details>

---
* #04)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/ResourcesLoader.cs#L63C1-L63C1)) [JSON을 Data 클래스로 변환 한 후 Dictionary 저장해서 데이터 관리](https://copractice.tistory.com/62)

<details>
<summary>예시 코드</summary>
  
```csharp
  // 데이터 타입을 로드합니다
  private static void LoadData<T>(Dictionary<int, Data> dataDic, string jsonPath) where T : Data
  {
      // JSON 파일을 로드 하기위해 TextAsset 타입으로 로드합니다.
      var json = Resources.Load<TextAsset>(jsonPath);
      // 가져온 TextAsset를 가져와서 Data타입으로 역직렬화 합니다.
      var datas = JsonConvert.DeserializeObject<T[]>(json.text);

      // 역직렬화한 Data타입을 Dic에 넣어줍니다.
      foreach (var data in datas)
      {
          dataDic.Add(data.ID, data);
      }
  }
```

</details>

---
* #05)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/GameManager.cs#L385)) [C# 리플렉션을 이용해서 스킬, 상태이상 클래스 런타임 생성 후 데이터 관리](https://copractice.tistory.com/63)

<details>
<summary>예시 코드</summary>
  
```csharp
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
```

</details>

---
* #06)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/Skill.cs#L87)) [string.Format을 활용한 스킬 설명 구현](https://copractice.tistory.com/64)

<details>
<summary>Ex</summary>
  
![#6](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/1dd11497-3c97-4ac4-be72-2e4e5faa055b)


</details>

---
* #07)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/GameManager/ItemGenerator.cs)) [ScriptableObject로 만든 랜덤 데이터 생성기를 통한 랜덤 아이템 데이터 생성 구현](https://copractice.tistory.com/65)

<details>
<summary>예시 코드</summary>
  
```csharp
public class ItemGenerator : MonoBehaviour
{
    [SerializeField] EquipmentCreateData normalCreateData;      // 평범 등급 아이템 랜덤 데이타 생성기
    [SerializeField] EquipmentCreateData rareCreateData;        // 희귀 등급 아이템 랜덤 데이타 생성기
    [SerializeField] EquipmentCreateData uniqueCreateData;      // 고유 등급 아이템 랜덤 데이타 생성기
    [SerializeField] EquipmentCreateData legendaryCreateData;   // 전설 등급 아이템 랜덤 데이타 생성기

    // 장비 데이타를 생성하는 클래스 new() 제한자를 넣어야 클래스를 생성할 수 있다.
    public T CreateEquipmentItemData<T>(eGradeType itemGrade) where T : EquipmentItemData, new()
    {
        // 새로운 클래스
        T newData = new T();
        EquipmentCreateData creator = null;

        newData.equipmentGrade = itemGrade;

        // 만들 려는 등급에 맞는 랜덤 데이터 생성기를 참조한다.
        switch (itemGrade)
        {
            case eGradeType.Normal:
                creator = this.normalCreateData;
                break;
            case eGradeType.Rare:
                creator = this.rareCreateData;
                break;
            case eGradeType.Unique:
                creator = this.uniqueCreateData;
                break;
            case eGradeType.Legendary:
                creator = this.legendaryCreateData;
                break;
            default:
                Debug.LogWarning("CreateItem Error #1");
                return null;
        }

        if (newData is WeaponData)
            // 만들려는 데이터가 무기데이터 라면
        {
            // 주 스텟은 공격력, 랜덤한 능력치 입력
            SetNewPropertyRound(ref (newData as WeaponData).attackPoint, creator.attackPoint.min, creator.attackPoint.max);
            newData.equipmentType = eEquipmentItemType.Weapon;
        }
        else if (newData is HelmetData)
            // 만들려는 데이터가 헬멧데이터 라면
        {
            // 주 스텟은 생명력, 랜덤한 능력치 입력
            SetNewPropertyRound(ref (newData as HelmetData).healthPoint, creator.healthPoint.min, creator.healthPoint.max);
            newData.equipmentType = eEquipmentItemType.Helmet;
        }
        else if (newData is ArmorData)
            // 만들려는 데이터가 갑옷데이터 라면
        {
            // 주 스텟은 방어력, 랜덤한 능력치 입력
            SetNewPropertyRound(ref (newData as ArmorData).defencePoint, creator.defencePoint.min, creator.defencePoint.max);
            newData.equipmentType = eEquipmentItemType.Armor;
        }
        else if (newData is ShoeData)
            // 만들려는 데이터가 신발데이터 라면
        {
            // 주 스텟은 속도, 랜덤한 능력치 입력
            SetNewPropertyRound(ref (newData as ShoeData).speed, creator.speed.min, creator.speed.max);
            newData.equipmentType = eEquipmentItemType.Shoe;
        }
        else if (newData is AmuletData)
            // 만들려는 데이터가 목걸이데이터 라면
        {
            // 주 스텟은 치명타 확률, 데미지, 랜덤한 능력치 입력
            SetNewProperty(ref (newData as AmuletData).criticalPercent, creator.criticalPercent.min, creator.criticalPercent.max);
            SetNewProperty(ref (newData as AmuletData).criticalDamage, creator.criticalDamage.min, creator.criticalDamage.max);
            newData.equipmentType = eEquipmentItemType.Amulet;
        }
        else if (newData is RingData)
            // 만들려는 데이터가 반지이데이터 라면
        {
            // 주 스텟은 효과 적중, 저항력, 랜덤한 능력치 입력
            SetNewProperty(ref (newData as RingData).effectHit, creator.effectHit.min, creator.effectHit.max);
            SetNewProperty(ref (newData as RingData).effectResistance, creator.effectRes.min, creator.effectRes.max);
            newData.equipmentType = eEquipmentItemType.Ring;
        }
        else
        {
            Debug.LogWarning("CreateItem Error #2");
            return null;
        }

        //랜덤한 세트 입력
        newData.setType = (eSetType)Random.Range(0, (int)eSetType.Count);
        return newData;
    }

    // 아이템 강화
    public void ReinforceEquipment(EquipmentItemData data)
    {
        // 강화 수치 증가
        data.reinforceCount++;

        // 각 아이템에 맞는 능력치 증가
        if (data is WeaponData)
        {
            (data as WeaponData).attackPoint = Mathf.Floor((data as WeaponData).attackPoint * 1.2f);
        }
        else if (data is ArmorData)
        {
            (data as ArmorData).defencePoint = Mathf.Floor((data as ArmorData).defencePoint * 1.2f);
        }
        else if (data is HelmetData)
        {
            (data as HelmetData).healthPoint = Mathf.Floor((data as HelmetData).healthPoint * 1.2f);

        }
        else if (data is ShoeData)
        {
            (data as ShoeData).speed = Mathf.Floor((data as ShoeData).speed * 1.1f);
        }
        else if (data is AmuletData)
        {
            (data as AmuletData).criticalPercent = (data as AmuletData).criticalPercent + 0.01f;
            (data as AmuletData).criticalDamage = (data as AmuletData).criticalDamage + 0.01f;

        }
        else if (data is RingData)
        {
            (data as RingData).effectHit = (data as RingData).effectHit + 0.01f;
            (data as RingData).effectResistance = (data as RingData).effectResistance + 0.01f;
        }

        // 강화 수치가 3늘어날때마다 옵션 스탯을 하나씩 부여한다.
        if (data.reinforceCount == 3)
        {
            AddOption(ref data.optionStat_1_Type, ref data.optionStat_1_value, GetEquipmentOptionStat(data), data.equipmentGrade);
        }
        else if (data.reinforceCount == 6)
        {
            AddOption(ref data.optionStat_2_Type, ref data.optionStat_2_value, GetEquipmentOptionStat(data), data.equipmentGrade);
        }
        else if (data.reinforceCount == 9)
        {
            AddOption(ref data.optionStat_3_Type, ref data.optionStat_3_value, GetEquipmentOptionStat(data), data.equipmentGrade);
        }
        else if (data.reinforceCount == 12)
        {
            AddOption(ref data.optionStat_4_Type, ref data.optionStat_4_value, GetEquipmentOptionStat(data), data.equipmentGrade);
        }
    }
```

</details>

---
* #08)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/UI/InventoryPanel/InventoryPanel.cs#L66)) [UGUI 에서 피봇과 포지션 좌표를 통한 툴팁 생성](https://copractice.tistory.com/66)

<details>
<summary>Ex</summary>
  
![#8](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/b39df7ac-075d-482b-99f5-038cbb2c6a7c)

</details>

---
* #09)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/UI/InventoryPanel/EquipmentInventory.cs#L77)) [다중 선택 토글에 의한 장비아이템 일반 선택 혹은 다중 선택 시스템 구현](https://copractice.tistory.com/67)

<details>
<summary>Ex</summary>
  
![#9](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/3028fbff-a6e4-409e-b511-b4b3d7553578)

</details>

---
* #10)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/UI/BattleLogUI.cs)) [큐를 이용한 로그 시스템](https://copractice.tistory.com/68)

<details>
<summary>예시 코드</summary>
  
```csharp
public class BattleLogUI : MonoBehaviour
{
    [SerializeField] int logCount = 5;                          // 로그가 표시될 최대 갯수
    [SerializeField] TextMeshProUGUI logText;                   // 로그 텍스트

    private Queue<string> logQueue = new Queue<string>();       // 로그 큐

    private void Start()
    {
        // 로그를 초기화 한다.
        ResetLog();
    }

    // 로그를 업데이트 한다.
    private void UpdateLog()
    { 
        string logtxt = string.Empty;

        // 로그 큐를 순회하며 로그를 쌓는다.
        foreach (string log in logQueue)
        {
            logtxt += ("\n" + log);
        }

        // 로그 출력
        logText.text = logtxt;
    }

    // 로그를 더해준다.
    public void AddLog(string logText)
    {
        // 로그를 로그 큐에 넣는다.
        logQueue.Enqueue(logText);
        
        // 만약 로그큐가 최대 카운트를 넘어가면
        if (logQueue.Count > logCount)
        {
            // 가장 오래된 로그를 없애준다.
            logQueue.Dequeue();
        }

        // 로그를 업데이트 합니다.
        UpdateLog();
    }

    // 로그텍스트와 큐를 초기화한다.
    public void ResetLog()
    {
        logQueue.Clear();
        logText.text = "";
    }
}
```

</details>

---
* #11)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Lobby/UI/HeroPanel/CompositionPanel/UnitCompositionPanelUI.cs)) [스택을 이용한 영웅 합성 시스템](https://copractice.tistory.com/69)

<details>
<summary>Ex</summary>
  
![#11](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/729f0a24-42f3-401c-96d6-1dc18f628ec7)

</details>

---
* #12)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/WorldMap/UI/UnitSlotSelector_FormationPopup.cs)) [드래그 앤 드랍으로 만든 유닛 포지션 배정 시스템](https://copractice.tistory.com/70)

<details>
<summary>Ex</summary>
  
![#12](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/224bd48d-5c42-4eb5-bc88-954dd662589c)

</details>

---
* #13)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/UIManager.cs#L81)) [자주 사용되는 UI 팝업창을 재활용](https://copractice.tistory.com/71)

<details>
<summary>Ex</summary>
  
![#13](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/90c5387c-f1fa-45df-b579-bcbbdb51c8ac)

</details>

---
* #14)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/Core/LobbyUIManager.cs#L99)) [커맨드 패턴을 이용한 Undo 시스템](https://copractice.tistory.com/72)

<details>
<summary>Ex</summary>
  
![#14](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/166bedf4-37e4-4bfd-a6a5-6dca7cc76a7b)

</details>

---
* #15)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Core/BattleManager.cs#L446)) [이벤트 버스를 이용해서 만든 전투 상태에 따른 이벤트 호출 시스템](https://copractice.tistory.com/73)

<details>
<summary>예시 코드</summary>
  
```csharp
// 전투 상태에 이벤트 구독
  public void PublishEvent(eBattleState state, UnityAction action)
  {
      if (StateEventHandlerDic.ContainsKey(state))
      // 이벤트 Dic에 해당 전투 상태 KEY가 있다면
      {
          //이벤트 구독
          StateEventHandlerDic[state].AddListener(action);
      }
      else
      {
          //KV 생성후 이벤트 구독
          StateEventHandlerDic.Add(state, new UnityEvent());
          StateEventHandlerDic[state].AddListener(action);
      }
  }

  // 이벤트 구독 해제
  public void UnPublishEvent(eBattleState state, UnityAction action)
  {
      if (StateEventHandlerDic.ContainsKey(state))
      {
          StateEventHandlerDic[state].RemoveListener(action);
      }
  }

  // 구독한 이벤트 모두 호출
  public void InvokeStateEvent(eBattleState state)
  {
      if (StateEventHandlerDic.ContainsKey(state))
      {
          StateEventHandlerDic[state]?.Invoke();
      }
  }
```

</details>

---
* #16)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Core/BattleManager.cs#L67)) [싱글톤 매니저 클래스 생성](https://copractice.tistory.com/74)

<details>
<summary>예시 코드</summary>
  
```csharp
private void Awake()
{
    // 싱글톤 생성
    if (Instance != null)
    {
        Debug.LogError("BattleManager is already created");
        Destroy(this.gameObject);
        return;
    }

    Instance = this;

    uiManager = GetComponentInChildren<BattleUIManager>();
    battleFactory = GetComponentInChildren<BattleFactory>();
    turnBaseSystem = GetComponentInChildren<TurnBaseSystem>();
    actionSystem = GetComponentInChildren<ActionSystem>();
    manaSystem = GetComponentInChildren<ManaSystem>();
    objectPool = GetComponentInChildren<ObjectPool>();
}
```

</details>

---
* #17)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Start/Core/StartManager.cs#L39)) [비동기 씬 호출을 통한 로딩 구현](https://copractice.tistory.com/75)

<details>
<summary>Ex</summary>
  
![#17](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/9a905e9d-2552-496d-9095-95486a522306)


</details>

---
* #18)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/ObjectPool.cs)) [오브젝트 풀링 사용 예](https://copractice.tistory.com/76)

<details>
<summary>예시 코드</summary>
  
```csharp
private BattleTextUI CreateBattleText()
    // 전투 텍스트 생성
{
    var newBattleText = Instantiate(battleTextPrefab, battleTextParents.transform);
    ReleaseBattleText(newBattleText);
    return newBattleText;
}

public BattleTextUI SpawnBattleText(bool isActive = true)
    // 전투 텍스트 소환
{
    if (battleTextPool.Count == 0)
        // 만약 풀이 비어있다면 새로운 텍스트 생성
    {
        CreateBattleText();
    }

    var battleText = battleTextPool.Dequeue();
    battleText.gameObject.SetActive(isActive);

    return battleText;
}

public void ReleaseBattleText(BattleTextUI releaseBattleText)
    // 전투 텍스트 반환
{
    releaseBattleText.gameObject.SetActive(false);
    releaseBattleText.transform.position = Vector3.zero;
    releaseBattleText.transform.rotation = Quaternion.identity;
    // 풀에 넣어준다.
    battleTextPool.Enqueue(releaseBattleText);
}
```

</details>

---
* #19)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/TurnBaseSystem.cs)) [게임 턴제 배틀 시스템 구현](https://copractice.tistory.com/77)

<details>
<summary>Ex</summary>
  
![#19](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/bfc3008d-2696-4982-bce4-b865e7ff82b6)


</details>

---
* #20)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/AISystem/AISystem.cs)) [자동 전투 시스템 구현](https://copractice.tistory.com/78)

<details>
<summary>Ex</summary>
  
![#20](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/4626c4a3-f49e-4b40-ac75-6789a2ca3068)


</details>

---
* #21)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/ActionSystem.cs)) [선택한 스킬에 따라 유닛을 선택하는 액션시스템 구현](https://copractice.tistory.com/79)

<details>
<summary>Ex</summary>
  
![#21](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/ceba155a-1576-462d-9332-66c081b5df31)


</details>

---
* #22)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/Map/LootItemTable.cs)) [루팅 테이블을 만드는 스크립테이블 오브젝트 (커스텀 인스펙터)](https://copractice.tistory.com/80)

<details>
<summary>Ex</summary>
  
![#22](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/5a5c8dd1-a873-45f0-b6c3-6581743de457)


</details>

---
* #23)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/WorldMap/Editor/MapNodeDraw.cs#L23)) [트리 구조로 만든 맵 노드 트리를 DFS 재귀를 이용해서 만든 맵노드 끼리 노드 연결 자동화](https://copractice.tistory.com/81)

<details>
<summary>Ex</summary>
  
![#23](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/c8a91640-0a4a-4c55-b3f2-d8c6af7b8f4c)


</details>

---
* #24)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/f98487a65e925662b1aa78bcfee3bd133d675ace/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/SkillList/ActiveSkill/Skill_GWEN_BaseAttack.cs#L29)) [Dotween을 활용한 투사체 던지기 스킬 구현](https://copractice.tistory.com/85)

<details>
<summary>Ex</summary>
  
![#24](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/2b56d2a5-cccc-44f9-81a2-5b59f14e581f)


</details>

---
* #25)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/GameManager/NetworkManager.cs)) [파이어베이스 연동 구현](https://copractice.tistory.com/86)

<details>
<summary>Ex</summary>
  
![#25](https://github.com/dkckacka1/DotHeros-2DPortfolio-/assets/125544460/34a7f0a6-9041-47e5-a17f-e8356e62c14b)


</details>

