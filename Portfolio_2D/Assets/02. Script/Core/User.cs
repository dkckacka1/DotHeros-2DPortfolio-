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

            //Debug.Log(userUnitDic.Count);
        }

        public void AddNewUnit(Unit unit)
        {
            userUnitDic.Add(userUnitDic.Count,unit);
            userData.unitDataList.Add(new UserUnitData(unit));
            SaveManager.SaveUserData(userData);
        }

        public void AddNewUnit(List<Unit> units)
        {
            foreach (var unit in units)
            {
                userUnitDic.Add(userUnitDic.Count, unit);
                userData.unitDataList.Add(new UserUnitData(unit));
            }

            SaveManager.SaveUserData(userData);
        }
        public bool IsMaxUnitCount
        {
            get
            {
                return userData.maxUnitListCount == userUnitDic.Count;
            }
        }
        public int MaxEnergy
        {
            get
            {
                return 30 + (userData.userLevel * 5);
            }
        }

        public float MaxExperience
        {
            get
            {
                return (userData.userLevel * 100f);
            }
        }
    } 
}
