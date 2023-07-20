using UnityEngine;

/*
 * 게임에서 사용되는 상수들만 모아놓은 static 클래스
 */

namespace Portfolio
{
    public static class Constant
    {
        //===========================================================
        // GameValue
        //===========================================================
        // 최대 강화 횟수
        public const float MaxReinforceCount = 15;
        // 강화 레벨에 따른 강화 비용
        public static readonly int[] ReinforceConsumeGoldValues = { 100, 500, 1000, 2000, 4000, 10000, 15000, 30000, 75000, 100000, 150000, 300000, 750000, 1500000, 2000000 };
        // 평범 등급 아이템의 색
        public static readonly Color NormalItemGradeColor = new Color(1, 1, 1, 1);
        // 희귀 등급 아이템의 색
        public static readonly Color RareItemGradeColor = new Color(0.3f, 0.3f, 1, 1);
        // 고유 등급 아이템의 색
        public static readonly Color UniqueItemGradeColor = new Color(1, 1, 0, 1);
        // 전설 등급 아이템의 색
        public static readonly Color LegendaryItemGradeColor = new Color(1, 0.5f, 0, 1);
        // 아군의 색
        public static readonly Color PlayerUnitColor = new Color(0, 1, 0, 1);
        // 적군의 색
        public static readonly Color EnemyUnitColor = new Color(1, 0, 0, 1);
        // 유닛 레벨당 올라갈 최대 경험치 량
        public const float UnitLevelUpExperienceValue = 1000f;
        // 에너지 회복 시간
        public const float EnergyChargeTime = 10f;
        // 유저 유닛 리스트 최대 갯수를 올릴 때 소비할 다이아 량
        public const int UnitListSizeUPDiaConsumeValue = 50;
        // 유저 유닛 리스트 최대 갯수를 올릴 때 올라갈 갯수
        public const int UnitListSizeUpCount = 5;
        // 유저 유닛 리스트 최대 갯수의 맥시멈
        public const int UnitListMaxSizeCount = 100;
        // 한번 소환할 때 소비되는 다이아 량
        public const int Summon_1_unitConsumeDiaValue = 30;
        // 열번 소환할 때 소비되는 다이아 량
        public const int Summon_10_unitConsumeDiaValue = 270;
        // 장비 리스트 사이즈 올릴 떄 소비될 다이아 량
        public const int EquipmentListSizeUpDiaConsumeValue = 50;
        // 장비 리스트 사이즈 올릴 때 올라갈 갯수
        public const int EquipmentListSizeUpCount = 5;
        // 장비 리스트 최대 사이즈 수
        public const int EquipmentListMaxSizeCount = 100;
        // 상점에서 소비아이템의 판매시 최대 갯수
        public static int ShopProductMaxCount = 6;
        // 이 게임의 기본 프레임
        public static int GameDefualtFrame = 60;
        // 소비아이템 상점 새로고침시 소비되는 다이아 양
        public const int ShopRefreshDiaConsumValue = 20;
        // 마지막 맵의 ID
        public const int LastMapID = 505;


        //===========================================================
        // RandomValue
        //===========================================================
        // 유닛 소환 시 1성 등급 유닛이 나올 확률
        public const float NormalUnitSummonPercent = 0.70f;
        // 유닛 소환 시 2성 등급 유닛이 나올 확률
        public const float RareUnitSummonPercent = 0.25f;
        // 유닛 소환 시 3성 등급 유닛이 나올 확률
        public const float UniqueUnitSummonPercent = 0.05f;
        // 장비 강화시 강화 레벨에 따른 강화 확률
        public static readonly float[] ReinforceProbabilitys = { 1f, 0.95f, 0.9f, 0.85f, 0.8f, 0.78f, 0.75f, 0.73f, 0.71f, 0.7f, 0.69f, 0.67f, 0.65f, 0.63f, 0.6f };
        // 상점의 할인율
        public static readonly float[] ShopProductDiscountValues = { 0f, 0.1f, 0.25f, 0.33f, 0.5f, 0.7f, 0.9f };

        //===========================================================
        // BattleValue
        //===========================================================
        // 전투 시 최대 마나량
        public const int MaxManaValue = 4;
        // 방어도를 계산할 때 사용하는 방어 상수
        public const float DefenciveConstValue = 100;

        //===========================================================
        // AIValue
        //===========================================================

        //===========================================================
        // Path
        //===========================================================
        // 엑셀 데이터 테이블 참조 위치
        public const string DataTablePath = @"\05. DataTable\";
        // 엑셀을 json으로 만들 때 Json 저장 위치
        public const string ResorucesDataPath = @"\Resources\Data\";
        // 스킬 데이터 테이블 이름
        public const string SkillDataTableName = @"SkillDataTable";
        // 액티브 스킬 Json 파일 이름
        public const string ActiveSkillJsonName = @"ActiveSkillData";
        // 패시브 스킬 Json 파일 이름
        public const string PassiveSkillJsonName = @"PassiveSkillData";
        // 유닛 데이터 테이블 이름
        public const string UnitDataTableName = @"UnitDataTable";
        // 유닛 Json 파일 이름
        public const string UnitDataJsonName = @"UnitData";
        // 상태이상 데이터 테이블 이름
        public const string ConditionDataTableName = @"ConditionDataTable";
        // 상태이상 Json 파일 이름
        public const string ConditionDataJsonName = @"ConditionData";
        // 맵 데이터 테이블 이름
        public const string MapDataTableName = @"MapDataTable";
        // 맵 Json 파일 이름
        public const string MapDataJsonName = @"MapData";
        // 스테이지 Json 파일 이름
        public const string StageDataJsonName = @"StageData";
        // 아이템 데이터 테이블 이름
        public const string ItemDataTableName = @"ItemDataTable";
        // 소비아이템 Json 파일 이름
        public const string ConsumableItemDataJsonName = @"ConsumableItemData";
        // 리소스폴더내의 스크립테이블 오브젝트 폴더 위치
        public const string ScriptableObjectResourcesPath = @"ScriptableObject";
        // 리소스폴더내의 루팅테이블 스크립테이블 폴더 위치
        public const string LootingTableResourcesPath = @"LootingTable";
        // 유저의 효과음 볼륨 값이 저장된 플레이어프랩스의 이름
        public const string SoundVolumeConfigureLoadPath = "SoundVolume";
        // 유저의 음악 볼륨 값이 저장된 플레이어프랩스의 이름
        public const string MusicVolumeConfigureLoadPath = "MusicVolume";

        //===========================================================
        // Network
        //===========================================================
        public const string userDatabasePath = "UserData";

        //===========================================================
        // Configure
        //===========================================================
        // 해상도 설정 사이즈 배열
        public static readonly Resolution[] resolutions =
        {
            new Resolution(){ width = 1920, height = 1080, refreshRate = 60},
            new Resolution(){ width = 1600, height = 900, refreshRate = 60 },
            new Resolution(){ width = 1280, height = 720, refreshRate = 60 },
            new Resolution(){ width = 960, height = 540, refreshRate = 60 },
        };
    }
}
