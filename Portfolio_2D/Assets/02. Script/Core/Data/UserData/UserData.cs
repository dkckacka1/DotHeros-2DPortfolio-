
using System;
using System.Collections.Generic;

/*
 * 유저 데이터 클래스
 */

namespace Portfolio
{
    public class UserData
    {
        public string userID;                                       // 유저 ID
        public string userPassword;                                 // 유저 패스워드
        public string userNickName;                                 // 유저 닉네임
        public string userPortraitName;                             // 유저 포트레이트 이미지
        public bool isNewUser = false;                              // 신규 유저 확인
        public int userLevel = 1;                                   // 유저 레벨
        public int userCurrentExperience;                           // 유저 현재 경험치
        public int gold;                                            // 유저 골드량
        public int diamond;                                         // 유저 다이아량
        public int energy;                                          // 유저 에너지량

        // 유닛 부문
        public int maxUnitListCount = 30;                           // 최대 유닛 갯수
        public List<UserUnitData> unitDataList;                     // 유저 소지 유닛 정보 리스트

        // 인벤토리 부문
        public int maxEquipmentListCount = 50;                      // 최대 소지 장비 갯수
        public List<EquipmentItemData> equipmentItemDataList;       // 유저 소지 장비 아이템 리스트
        public Dictionary<int, int> consumalbeItemDic;              // 유저 소지 소비 아이템 리스트 (KEY : ID, VALUE : COUNT)

        // 진행도 부문
        public List<int> clearMapList;                              // 유저 클리어 맵 정보리스트

        public DateTime LastAccessTime;                             // 유저 게임 종료 시각

        // 신규 유저 생성
        public UserData(string userID, string userPassword, string userNickName)
        {
            this.userID = userID;
            this.userPassword = userPassword;
            this.userNickName = userNickName;
            this.userPortraitName = "Sprite_Unit_Portrait_ZICH";
            isNewUser = true;

            unitDataList = new List<UserUnitData>();
            equipmentItemDataList = new List<EquipmentItemData>();
            consumalbeItemDic = new Dictionary<int, int>();
            clearMapList = new List<int>();
        }
    }
}
