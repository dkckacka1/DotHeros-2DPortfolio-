
using System.Collections.Generic;

namespace Portfolio
{
    public class UserData
    {

        public int userID = 00000001;
        public string userName;
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

        public UserData()
        {
            unitDataList = new List<UserUnitData>();
            equipmentItemDataList = new List<EquipmentItemData>();
            consumalbeItemDic = new Dictionary<int, int>();
            clearMapList = new List<int>();
        }
    }
}
