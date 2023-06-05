
using System.Collections.Generic;

namespace Portfolio
{
    public class UserData
    {
        public int userID;
        public string userName;
        public int userLevel;
        public int userCurrentExperience;
        public int gold;
        public int diamond;
        public int energy;

        // 유닛 부문
        public int maxUnitListCount = 30;
        public List<UserUnitData> unitDataList;
        public List<EquipmentItemData> equipmentItemDataList;

        public UserData()
        {
            unitDataList = new List<UserUnitData>();
            equipmentItemDataList = new List<EquipmentItemData>();
        }
    }
}
