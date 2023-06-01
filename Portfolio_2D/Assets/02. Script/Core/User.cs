using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class User
    {
        public UserData userData;
        public Dictionary<int, Unit> userUnitDic;

        public User(UserData userData)
        {
            this.userData = userData;
            userUnitDic = new Dictionary<int, Unit>();

            for (int i = 0; i < userData.unitDataList.Count; i++)
            {
                if (GameManager.Instance.TryGetData<UnitData>(userData.unitDataList[i].unitID, out UnitData unitData))
                {
                    userUnitDic.Add(i, new Unit(unitData, userData.unitDataList[i]));
                }
            }

            Debug.Log(userUnitDic.Count);
        }
    } 
}
