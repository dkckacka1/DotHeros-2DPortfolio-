using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Portfolio
{
    public class User
    {
        public UserData userData;
        public List<Unit> userUnitList;
        public List<EquipmentItemData> userEquipmentItemDataList;
        public Dictionary<ConsumableItemData, int> userConsumableItemDic;

        public bool IsMaxUnitCount
        {
            get
            {
                return userData.maxUnitListCount == userUnitList.Count;
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

        public User(UserData userData)
        {
            this.userData = userData;
            userUnitList = new List<Unit>();
            userEquipmentItemDataList = new List<EquipmentItemData>();
            userConsumableItemDic = new Dictionary<ConsumableItemData, int>();

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

            //foreach (var item in userEquipmentItemDataList)
            //{
            //    Debug.Log(item == null);
            //}

            //Debug.Log(userUnitDic.Count);
        }

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

        public UserData GetSaveUserData()
        {
            userData.unitDataList = this.userUnitList.Select(item => item.UserData).ToList();
            userData.equipmentItemDataList = this.userEquipmentItemDataList;
            userData.consumalbeItemDic = this.userConsumableItemDic.ToDictionary(item => item.Key.ID, item => item.Value);

            return userData;
        }
    }
}
