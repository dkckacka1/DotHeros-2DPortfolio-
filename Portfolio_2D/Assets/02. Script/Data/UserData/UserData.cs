
using System.Collections.Generic;

namespace Portfolio
{
    public class UserData
    {
        public int userID;
        public int userName;

        public List<UserUnitData> unitDataList;

        public UserData()
        {
            unitDataList = new List<UserUnitData>();
        }
    }
}
