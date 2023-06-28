
using System;
using System.Collections.Generic;

namespace Portfolio
{
    public class UserData
    {
        public string userID;
        public string userPassword;
        public string userNickName;
        public string userPortraitName;
        public bool isNewUser = false;
        public int userLevel = 1;
        public int userCurrentExperience;
        public int gold;
        public int diamond;
        public int energy;

        // 유닛 부문
        public int maxUnitListCount = 30;
        public List<UserUnitData> unitDataList;

        // 인벤토리 부문
        public List<EquipmentItemData> equipmentItemDataList;
        public Dictionary<int, int> consumalbeItemDic;  // Key : ID, Value : itemCount

        // 진행도 부문
        public List<int> clearMapList;

        public DateTime LastAccessTime;

        public UserData(string userID, string userPassword,string userNickName)
        {
            this.userID = userID;
            this.userPassword = userPassword;
            this.userNickName = userNickName;
            this.userPortraitName = "Dwarf_1";
            isNewUser = true;

            unitDataList = new List<UserUnitData>();
            equipmentItemDataList = new List<EquipmentItemData>();
            consumalbeItemDic = new Dictionary<int, int>();
            clearMapList = new List<int>();
        }
    }
}
