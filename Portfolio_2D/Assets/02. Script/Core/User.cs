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
        private List<Unit> userUnitList;                             // ������ ������ �ִ� ���� ����Ʈ
        private List<EquipmentItemData> userEquipmentItemDataList;   // ������ ������ �ִ� ��� ����Ʈ

        //===========================================================
        // Property
        //===========================================================
        public List<Unit> UserUnitList => userUnitList;
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

        

        // ���ް����� ���� ���� �� ID�� �����մϴ�
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
                    // ���� ���� �ƴ� Ŭ������ ���� ������ ������ ������ �� ù��° �� ID
                    int nextMapID = userData.clearMapList.Where(mapID => mapID < 600).OrderByDescending(mapID => mapID).First();

                    // Ŭ������ ���� �������� �ִٸ� ������ ����, �������� ���ٸ� Ŭ������ ���� ���� ������ ����
                    return GameManager.Instance.HasData<MapData>(nextMapID + 1) ? nextMapID + 1 : nextMapID;
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
        // ���� �����͸� ���� ���� ������
        public User(UserData userData)
        {
            this.userData = userData;
            // ������ ������ �ִ� ���� ����Ʈ
            userUnitList = new List<Unit>();
            // ���� ���� �����ͷ� ���� �����͸� ������ �� �� �����͸� ���� ������ ���� �� ����Ʈ�� �־��ش�.
            foreach (var userUnitData in userData.unitDataList)
            {
                if (GameManager.Instance.TryGetData<UnitData>(userUnitData.unitID, out UnitData unitData))
                {
                    userUnitList.Add(new Unit(unitData, userUnitData));
                }
            }

            // ������ ������ �ִ� ��� ����Ʈ
            userEquipmentItemDataList = new List<EquipmentItemData>();
            // ��� �������� ��� ������ Ÿ�Կ� ���� ��� �������� ����Ʈ�� �־��ش�.
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

        // �������� ���� �����͸� ������Ʈ�ؼ� �������ش�.
        public UserData GetSaveUserData()
        {
            // ���� ����Ʈ���� ���� ���� �����͸� �����ؼ� ���� �����Ϳ� �־��ش�.
            userData.unitDataList = this.userUnitList.Select(item => item.UserData).ToList();
            // ���� �����Ϳ��� ��� ������ �����͸� ������Ʈ ���ش�.
            userData.equipmentItemDataList = this.userEquipmentItemDataList;

            return userData;
        }
        //===========================================================
        // Unit
        //===========================================================
        // �ִ� ���� ����Ʈ ������
        public int MaxUnitListCount
        {
            get => userData.maxUnitListCount;
            set => userData.maxUnitListCount = value;
        }

        // ���� ���� ����Ʈ ������ �ִ� ���� ����Ʈ�͵�������
        public bool IsMaxUnitCount => MaxUnitListCount == userUnitList.Count;


        // ���� Ŭ������ ���ؼ� ���� �߰� (���� 1�� �̱�)
        public void AddNewUnit(Unit unit)
        {
            userUnitList.Add(unit);
            userData.unitDataList.Add(new UserUnitData(unit));
            GameManager.Instance.SaveUser();
        }

        // ���� ����Ʈ�� ���ؼ� ���� �߰� (���� 10�� �̱�)
        public void AddNewUnit(List<Unit> units)
        {
            foreach (var unit in units)
            {
                userUnitList.Add(unit);
                userData.unitDataList.Add(new UserUnitData(unit));
            }

            GameManager.Instance.SaveUser();
        }

        // ������ ������ �����۵��� ���� ������ �� ��� �κ��丮�� �־��ش�.
        // �����ռ� �� ���
        public void GetUnitEquipment(Unit unit)
        {
            if (unit.WeaponData != null)
            {
                AddEquipmentItem(unit.WeaponData);
                unit.WeaponData = null;
            }

            if (unit.ArmorData != null)
            {
                AddEquipmentItem(unit.ArmorData);
                unit.ArmorData = null;
            }

            if (unit.HelmetData != null)
            {
                AddEquipmentItem(unit.HelmetData);
                unit.HelmetData = null;
            }

            if (unit.ShoeData != null)
            {
                AddEquipmentItem(unit.ShoeData);
                unit.ShoeData = null;
            }

            if (unit.AmuletData != null)
            {
                AddEquipmentItem(unit.AmuletData);
                unit.AmuletData = null;
            }

            if (unit.RingData != null)
            {
                AddEquipmentItem(unit.RingData);
                unit.RingData = null;
            }
        }

        // ���� ���� ����Ʈ���� �ߺ��� ������ ����Ʈ�� ���� �� ����
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
        // ���̾Ƹ� ����� �� �ִ��� Ȯ���Ѵ�.
        public bool CanDIamondConsume(int consumeValue) => userData.diamond >= consumeValue;

        //===========================================================
        // EquipmentItem
        //===========================================================
        // ������ ���������� ��ȯ�մϴ�.
        public List<EquipmentItemData> GetInventoryEquipmentItem => userEquipmentItemDataList;

        // �ִ� ��� ������ �κ��丮 ������
        public int MaxEquipmentListCount
        {
            get => userData.maxEquipmentListCount;
            set => userData.maxEquipmentListCount = value;
        }

        // ��� �κ��丮�� ������ �ִ��� Ȯ��
        public bool IsMaxEquipmentCount => MaxEquipmentListCount <= GetInventoryEquipmentItem.Count;

        // ��� ���� ������ ���������� ��ȯ�մϴ�.
        public List<EquipmentItemData> GetAllUnitEquipmentItem()
        {
            List<EquipmentItemData> equipmentItemList = new List<EquipmentItemData>();
            foreach (var unit in userUnitList)
            {
                equipmentItemList.AddRange(unit.GetAllEquipmentItem());
            }

            return equipmentItemList;
        }

        // ��� �κ��丮�� �������� �߰��Ѵ�.
        public void AddEquipmentItem(EquipmentItemData equipmentItemData) => userEquipmentItemDataList.Add(equipmentItemData);

        // ��� �������� �κ��丮�� �߰��غ��� �Ѵ�.
        public bool TryAddEquipmentItem(EquipmentItemData equipmentItemData)
        {
            if (IsMaxEquipmentCount)
                // ��� �������� �߰��� ������ ����.
            {
                return false;
            }
            else
                // �߰��� ������ �ִ�.
            {
                AddEquipmentItem(equipmentItemData);
                return true;
            }
        }

        // ��� �������� �����غ��� �Ѵ�.
        public bool TryRemoveEquipmentItem(EquipmentItemData equipmentItemData)
        {
            if (userEquipmentItemDataList.Contains(equipmentItemData))
                // ���������� �κ��丮�� �ִٸ� ���� ����
            {
                userEquipmentItemDataList.Remove(equipmentItemData);
                return true;
            }
            else
                // ���ٸ� ���� ����
            {
                return false;
            }
        }

        //===========================================================
        // ConsumableItem
        //===========================================================
        // �Һ�������� �߰��Ѵ�.
        public void AddConsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
                // �ش�  ID�� ���� �Һ������ �����Ͱ� �ִ��� Ȯ��
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                    // �̹� �κ��丮�� �ִ� �Һ�������̶�� ������ �����ش�.
                {
                    UserConsumableItemDic[ID] += count;
                }
                else
                    // �κ��丮�� ���� �������̶�� KV�� �߰��Ѵ�.
                {
                    UserConsumableItemDic.Add(ID, count);
                }
            }
            else
            {
                Debug.LogWarning("ConsumalbeData ID is null");
            }
        }

        // �Һ������ ���
        public void ConsumItem(int ID, int count = 1)
        {
            if (IsHaveComsumableItem(ID, count))
                // count ��ŭ�� ������ ������ �ִ��� Ȯ���Ѵ�.
            {
                if (UserConsumableItemDic[ID] == count)
                    // ��� ������ ����Ѵٸ� KV�� �����Ѵ�.
                {
                    UserConsumableItemDic.Remove(ID);
                }
                else
                    // ����Ϸ��� ������ ������ �������� ���ٸ� ����ϴ� ������ŭ ���ش�.
                {
                    UserConsumableItemDic[ID] -= count;
                }
            }
            else
            {
                return;
            }
        }

        // �Һ�������� � �ִ��� Ȯ���Ѵ�.
        public int GetConsumItemCount(int ID)
        {
            if (IsHaveComsumableItem(ID))
                // ���濡 �ִ� �������̸� ���� ����
            {
                return UserConsumableItemDic[ID];
            }
            else
                // ���濡 ���ٸ� 0 ����
            {
                return 0;
            }
        }

        // ����� �Һ� �������� ������ �ִ��� Ȯ���Ѵ�.
        public bool IsHaveComsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
                // ������ ID���� Ȯ��
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                    // ���濡 �ִٸ� ���� ��
                {
                    return UserConsumableItemDic[ID] >= count;
                }
                else
                    // ���濡 ���ٸ� false ����
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
        // mapID �� ���� Ŭ������ ������ Ȯ���Ѵ�.
        public bool IsClearMap(int mapID) => userData.clearMapList.Contains(mapID);

        // ���� Ŭ�����Ѵ�.
        public void ClearMap(int mapID)
        {
            if (!IsClearMap(mapID))
                // Ŭ������ �� ����Ʈ�� ���ٸ� �߰����ش�.
            {
                userData.clearMapList.Add(mapID);
            }
        }

        // ������ ������ Ȯ���Ѵ�.
        public bool IsLeftEnergy(int consumeEnergy)
        {
            return CurrentEnergy >= consumeEnergy;
        }
    }
}
