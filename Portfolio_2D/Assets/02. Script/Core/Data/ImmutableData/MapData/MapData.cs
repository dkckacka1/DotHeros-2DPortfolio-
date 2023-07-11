
/*
 *  맵 데이터 클래스
 */

namespace Portfolio
{
    public class MapData : Data
    {
        public string mapName;                      // 맵 이름
        public bool isExternalMap = false;          // 외전 맵인지?
        public int stage_1_ID = -1;                 // 스테이지 ID 1
        public int stage_2_ID = -1;                 // 스테이지 ID 2
        public int stage_3_ID = -1;                 // 스테이지 ID 3
        public int stage_4_ID = -1;                 // 스테이지 ID 4
        public int stage_5_ID = -1;                 // 스테이지 ID 5
        public int consumEnergy = 5;                // 소비 에너지 량
        public int experienceValue = 10;            // 획득 경험치
        public int getGoldValue = 100;              // 획득 골드 량
        public int userExperienceValue = 100;       // 획득 유저 경험치
        public string lootingTableName;             // 루팅 테이블 스크립테이블 오브젝트의 이름
    }
}
