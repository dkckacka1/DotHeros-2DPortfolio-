using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/*
 * ���� �����͸� ���� ���� ���� Ŭ����
 */

namespace Portfolio
{
    public class User
    {
        private UserData userData;                                  // ���� ������
        public List<Unit> userUnitList;                             // ������ ������ �ִ� ���� ����Ʈ
        public List<EquipmentItemData> userEquipmentItemDataList;   // ������ ������ �ִ� ��� ����Ʈ

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
                // �⺻ 30 + ���� ���� * 5
                return 30 + (userData.userLevel * 5);
            }
        }
        public int CurrentEnergy
        {
            get => userData.energy;
            set
            {
                // ���� �������� 0 ~ �ִ� �������� �����Ѵ�.
                userData.energy = Mathf.Clamp(value, 0, MaxEnergy);
                // �������� ����Ǹ� ���� ������ ������Ʈ�Ѵ�.
                GameManager.UIManager.UserInfoUI.ShowEnergy(userData.energy, MaxEnergy);
            }
        }
        public int MaxExperience
        {
            get
            {
                //�ִ� ����ġ�� ���� ���� * 100
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
                // ���� �Ŵ������� �̹��� ��������Ʈ�� �ҷ��´�.
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
                    // �ִ� ����ġ �̻��̸�
                {
                    // �ʰ���ġ�� �����.
                    userData.userCurrentExperience = value - MaxExperience;
                    // ���� ������
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
                // ���� ���� ������Ʈ
                GameManager.UIManager.UserInfoUI.ShowGold(userData.gold);
            }
        }
        public int Diamond
        {
            get => userData.diamond;
            set
            {
                userData.diamond = value;
                // ���� ���� ������Ʈ
                GameManager.UIManager.UserInfoUI.ShowDiamond(userData.diamond);
            }
        }
        public int MaxUnitListCount
        {
            get => userData.maxUnitListCount;
            set => userData.maxUnitListCount = value;
        }
        public bool IsMaxUnitCount => userData.maxUnitListCount == userUnitList.Count;  // ���� ���� ����Ʈ ������ �ִ� ���� ����Ʈ�͵�������
        public int ClearHighestMapID
        {
            get
            {
                if (userData.clearMapList.Count == 0)
                    // Ŭ������ ���� ���ٸ� ���� ���� ���� 500
                {
                    return 500;
                }
                else
                {
                    // ���� ���� �ƴ� Ŭ������ ���� ������ ������ ������ �� ù��° �� ����
                    return userData.clearMapList.Where(mapID => mapID < 600).OrderByDescending(mapID => mapID).First();
                }
            }
        }

        public DateTime LastAccessTime // ���� ������ ���� �ð�
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
