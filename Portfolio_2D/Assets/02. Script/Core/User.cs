using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio
{
    public class User
    {
        private UserData userData;
        public List<Unit> userUnitList;
        public List<EquipmentItemData> userEquipmentItemDataList;

        //===========================================================
        // Property
        //===========================================================
        public Dictionary<int, int> UserConsumableItemDic
        {
            get
            {
                return userData.consumalbeItemDic;
            }
        }
        public int MaxEnergy
        {
            get
            {
                return 30 + (userData.userLevel * 5);
            }
        }
        public int CurrentEnergy
        {
            get => userData.energy;
            set
            {
                if (value > MaxEnergy)
                {
                    userData.energy = MaxEnergy;
                }
                else
                {
                    userData.energy = value;
                    GameManager.UIManager.UserInfoUI.ShowEnergy(userData.energy, MaxEnergy);
                }
            }
        }
        public int MaxExperience
        {
            get
            {
                return (userData.userLevel * 100);
            }
        }
        public string UserNickName 
        { 
            get => userData.userName; 
            set => userData.userName = value; 
        }
        public int UserID
        {
            get => userData.userID;
        }
        public int UserLevel
        {
            get => userData.userLevel;
            set => userData.userLevel = value;
        }      
        public int UserCurrentExperience
        {
            get => userData.userCurrentExperience;
            set
            {
                if (value >= MaxExperience)
                {
                    userData.userCurrentExperience = value - MaxExperience;
                    UserLevel++;
                }
                else
                {
                    userData.userCurrentExperience = value;
                }
            }
        }
        public int Gold
        {
            get => userData.gold;
            set 
            {
                userData.gold = value;
                GameManager.UIManager.UserInfoUI.ShowGold(userData.gold);
            }
        }
        public int Diamond
        {
            get => userData.diamond;
            set
            {
                userData.diamond = value;
                GameManager.UIManager.UserInfoUI.ShowGold(userData.diamond);
            }
        }
        public int MaxUnitListCount
        {
            get => userData.maxUnitListCount;
            set => userData.maxUnitListCount = value;
        }
        public bool IsMaxUnitCount
        {
            get
            {
                return userData.maxUnitListCount == userUnitList.Count;
            }
        }
        public int ClearHighestMapID
        {
            get
            {
                if (userData.clearMapList.Count == 0)
                {
                    return 500;
                }
                else
                {
                    return userData.clearMapList.Where(mapID => mapID < 600).OrderByDescending(mapID => mapID).First();
                }
            }
        }


        //===========================================================
        // SetUserData
        //===========================================================
        public User(UserData userData)
        {
            this.userData = userData;
            userUnitList = new List<Unit>();
            userEquipmentItemDataList = new List<EquipmentItemData>();

            foreach (var userUnitData in userData.unitDataList)
            {
                //Debug.Log(userUnitData.unitID);
                if (GameManager.Instance.TryGetData<UnitData>(userUnitData.unitID, out UnitData unitData))
                {
                    userUnitList.Add(new Unit(unitData, userUnitData));
                }
            }

            foreach (var item in userData.equipmentItemDataList)
            {
                switch (item.equipmentType)
                {
                    case EquipmentItemType.Weapon:
                        userEquipmentItemDataList.Add(item as WeaponData);
                        break;
                    case EquipmentItemType.Helmet:
                        userEquipmentItemDataList.Add(item as HelmetData);
                        break;
                    case EquipmentItemType.Armor:
                        userEquipmentItemDataList.Add(item as ArmorData);
                        break;
                    case EquipmentItemType.Amulet:
                        userEquipmentItemDataList.Add(item as AmuletData);
                        break;
                    case EquipmentItemType.Ring:
                        userEquipmentItemDataList.Add(item as RingData);
                        break;
                    case EquipmentItemType.Shoe:
                        userEquipmentItemDataList.Add(item as ShoeData);
                        break;
                }
            }
        }
        public UserData GetSaveUserData()
        {
            userData.unitDataList = this.userUnitList.Select(item => item.UserData).ToList();
            userData.equipmentItemDataList = this.userEquipmentItemDataList;

            return userData;
        }
        //===========================================================
        // Unit
        //===========================================================
        public void AddNewUnit(Unit unit)
        {
            userUnitList.Add(unit);
            userData.unitDataList.Add(new UserUnitData(unit));
            GameManager.Instance.SaveUser();
        }

        public void AddNewUnit(List<Unit> units)
        {
            foreach (var unit in units)
            {
                userUnitList.Add(unit);
                userData.unitDataList.Add(new UserUnitData(unit));
            }

            GameManager.Instance.SaveUser();
        }

        //===========================================================
        // ConsumableItem
        //===========================================================
        public void AddConsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.IsData<ConsumableItemData>(ID))
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                {
                    UserConsumableItemDic[ID] += count;
                }
                else
                {
                    UserConsumableItemDic.Add(ID, count);
                }
            }
            else
            {
                Debug.LogWarning("ConsumalbeData ID is null");
            }
        }

        public void ConsumItem(int ID, int count = 1)
        {
            if (IsHaveComsumableItem(ID, count))
            {
                if (UserConsumableItemDic[ID] == count)
                {
                    UserConsumableItemDic.Remove(ID);
                }
                else
                {
                    UserConsumableItemDic[ID] -= count;
                }
            }
            else
            {
                return;
            }
        }

        public int GetConsumItemCount(int ID)
        {
            if(IsHaveComsumableItem(ID))
            {
                return UserConsumableItemDic[ID];
            }
            else
            {
                return 0;
            }
        }

        public bool IsHaveComsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.IsData<ConsumableItemData>(ID))
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                {
                    return UserConsumableItemDic[ID] >= count;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //===========================================================
        // Map
        //===========================================================
        public bool isClearMap(int mapID) => userData.clearMapList.Contains(mapID);

        public void ClearMap(int mapID)
        {
            if (!isClearMap(mapID))
            {
                userData.clearMapList.Add(mapID);
            }
        }
    }
}
