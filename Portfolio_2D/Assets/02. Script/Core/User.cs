using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
 * 유저 데이터를 토대로 만든 유저 클래스
 */

namespace Portfolio
{
    public class User
    {
        private UserData userData;                                  // 유저 데이터
        public List<Unit> userUnitList;                             // 유저가 가지고 있는 유닛 리스트
        public List<EquipmentItemData> userEquipmentItemDataList;   // 유저가 가지고 있는 장비 리스트

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
                // 기본 30 + 유저 레벨 * 5
                return 30 + (userData.userLevel * 5);
            }
        }
        public int CurrentEnergy
        {
            get => userData.energy;
            set
            {
                // 유닛 에너지를 0 ~ 최대 에너지로 제한한다.
                userData.energy = Mathf.Clamp(value, 0, MaxEnergy);
                // 에너지가 변경되면 유저 정보를 업데이트한다.
                GameManager.UIManager.UserInfoUI.ShowEnergy(userData.energy, MaxEnergy);
            }
        }
        public int MaxExperience
        {
            get
            {
                //최대 경험치는 유저 레벨 * 100
                return (userData.userLevel * 100);
            }
        }
        public string UserNickName
        {
            get => userData.userNickName;
            set => userData.userNickName = value;
        }
        public string UserID => userData.userID;

        public Sprite UserPortrait
        {
            get
            {
                // 게임 매니저에서 이미지 스프라이트를 불러온다.
                return GameManager.Instance.GetSprite(userData.userPortraitName);
            }
            set
            {
                userData.userPortraitName = value.name;
            }
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
                    // 최대 경험치 이상이면
                {
                    // 초과수치는 남긴다.
                    userData.userCurrentExperience = value - MaxExperience;
                    // 유저 레벨업
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
                // 유저 정보 업데이트
                GameManager.UIManager.UserInfoUI.ShowGold(userData.gold);
            }
        }
        public int Diamond
        {
            get => userData.diamond;
            set
            {
                userData.diamond = value;
                // 유저 정보 업데이트
                GameManager.UIManager.UserInfoUI.ShowDiamond(userData.diamond);
            }
        }
        public int MaxUnitListCount
        {
            get => userData.maxUnitListCount;
            set => userData.maxUnitListCount = value;
        }
        public bool IsMaxUnitCount => userData.maxUnitListCount == userUnitList.Count;  // 현재 유닛 리스트 갯수가 최대 유닛 리스트와동일한지
        public int ClearHighestMapID
        {
            get
            {
                if (userData.clearMapList.Count == 0)
                    // 클리어한 맵이 없다면 가장 높은 맵은 500
                {
                    return 500;
                }
                else
                {
                    // 외전 맵이 아닌 클리어한 맵을 내림차 순으로 정렬한 후 첫번째 맵 리턴
                    return userData.clearMapList.Where(mapID => mapID < 600).OrderByDescending(mapID => mapID).First();
                }
            }
        }

        public DateTime LastAccessTime // 가장 마지막 접속 시간
        {
            get => userData.LastAccessTime;
            set => userData.LastAccessTime = value;
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

        public void GetUnitEquipment(Unit unit)
        {
            if (unit.WeaponData != null)
            {
                userEquipmentItemDataList.Add(unit.WeaponData);
                unit.WeaponData = null;
            }

            if (unit.ArmorData != null)
            {
                userEquipmentItemDataList.Add(unit.ArmorData);
                unit.ArmorData = null;
            }

            if (unit.HelmetData != null)
            {
                userEquipmentItemDataList.Add(unit.HelmetData);
                unit.HelmetData = null;
            }

            if (unit.ShoeData != null)
            {
                userEquipmentItemDataList.Add(unit.ShoeData);
                unit.ShoeData = null;
            }

            if (unit.AmuletData != null)
            {
                userEquipmentItemDataList.Add(unit.AmuletData);
                unit.AmuletData = null;
            }

            if (unit.RingData != null)
            {
                userEquipmentItemDataList.Add(unit.RingData);
                unit.RingData = null;
            }
        }

        public List<Unit> GetUserCollectUnitList()
        {
            List<Unit> userCollectUnitList = new List<Unit>();

            foreach (var unit in userUnitList)
            {
                if (userCollectUnitList.Where(collectUnit => collectUnit.UnitID == unit.UnitID).Count() == 0)
                {
                    userCollectUnitList.Add(unit);
                }
            }

            return userCollectUnitList;
        }

        //===========================================================
        // Resources
        //===========================================================
        public bool CanDIamondConsume(int consumeValue) => userData.diamond >= consumeValue;

        //===========================================================
        // ConsumableItem
        //===========================================================
        public void AddConsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
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
            if (IsHaveComsumableItem(ID))
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
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
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
        public bool IsClearMap(int mapID) => userData.clearMapList.Contains(mapID);

        public void ClearMap(int mapID)
        {
            if (!IsClearMap(mapID))
            {
                userData.clearMapList.Add(mapID);
            }
        }

        public bool IsLeftEnergy(int consumeEnergy)
        {
            return CurrentEnergy >= consumeEnergy;
        }
    }
}
