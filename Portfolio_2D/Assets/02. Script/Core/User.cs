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

        public int MaxEquipmentListCount
        {
            get => userData.maxEquipmentListCount;
            set => userData.maxEquipmentListCount = value;
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
        // 유저 데이터를 토대로 만든 생성자
        public User(UserData userData)
        {
            this.userData = userData;
            // 유저가 가지고 있는 유닛 리스트
            userUnitList = new List<Unit>();
            // 유저 유닛 데이터로 유닛 데이터를 참조한 후 두 데이터를 통해 유닛을 생성 후 리스트에 넣어준다.
            foreach (var userUnitData in userData.unitDataList)
            {
                if (GameManager.Instance.TryGetData<UnitData>(userUnitData.unitID, out UnitData unitData))
                {
                    userUnitList.Add(new Unit(unitData, userUnitData));
                }
            }

            // 유저가 가지고 있는 장비 리스트
            userEquipmentItemDataList = new List<EquipmentItemData>();
            // 장비 데이터의 장비 아이템 타입에 맞춰 장비 아이템을 리스트에 넣어준다.
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

        // 유저에서 유저 데이터를 업데이트해서 리턴해준다.
        public UserData GetSaveUserData()
        {
            // 유닛 리스트에서 유저 유닛 데이터만 추출해서 유저 데이터에 넣어준다.
            userData.unitDataList = this.userUnitList.Select(item => item.UserData).ToList();
            // 유저 데이터에서 장비 아이템 데이터를 업데이트 해준다.
            userData.equipmentItemDataList = this.userEquipmentItemDataList;

            return userData;
        }
        //===========================================================
        // Unit
        //===========================================================
        // 유닛 클래스를 통해서 유닛 추가 (유닛 1명 뽑기)
        public void AddNewUnit(Unit unit)
        {
            userUnitList.Add(unit);
            userData.unitDataList.Add(new UserUnitData(unit));
            GameManager.Instance.SaveUser();
        }

        // 유닛 리스트를 통해서 유닛 추가 (유닛 10명 뽑기)
        public void AddNewUnit(List<Unit> units)
        {
            foreach (var unit in units)
            {
                userUnitList.Add(unit);
                userData.unitDataList.Add(new UserUnitData(unit));
            }

            GameManager.Instance.SaveUser();
        }

        // 유닛이 장착한 아이템들을 전부 해제한 후 장비 인벤토리에 넣어준다.
        // 영웅합성 시 사용
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

        // 유저 유닛 리스트에서 중복이 없도록 리스트를 만든 후 리턴
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
        // 다이아를 사용할 수 있는지 확인한다.
        public bool CanDIamondConsume(int consumeValue) => userData.diamond >= consumeValue;

        //===========================================================
        // ConsumableItem
        //===========================================================
        // 소비아이템을 추가한다.
        public void AddConsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
                // 해당  ID를 가진 소비아이템 데이터가 있는지 확인
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                    // 이미 인벤토리에 있는 소비아이템이라면 갯수만 더해준다.
                {
                    UserConsumableItemDic[ID] += count;
                }
                else
                    // 인벤토리에 없는 아이템이라면 KV를 추가한다.
                {
                    UserConsumableItemDic.Add(ID, count);
                }
            }
            else
            {
                Debug.LogWarning("ConsumalbeData ID is null");
            }
        }

        // 소비아이템 사용
        public void ConsumItem(int ID, int count = 1)
        {
            if (IsHaveComsumableItem(ID, count))
                // count 만큼의 갯수를 가지고 있는지 확인한다.
            {
                if (UserConsumableItemDic[ID] == count)
                    // 모든 갯수를 사용한다면 KV를 삭제한다.
                {
                    UserConsumableItemDic.Remove(ID);
                }
                else
                    // 사용하려는 갯수가 가방의 갯수보다 적다면 사용하는 갯수만큼 빼준다.
                {
                    UserConsumableItemDic[ID] -= count;
                }
            }
            else
            {
                return;
            }
        }

        // 소비아이템이 몇개 있는지 확인한다.
        public int GetConsumItemCount(int ID)
        {
            if (IsHaveComsumableItem(ID))
                // 가방에 있는 아이템이면 갯수 리턴
            {
                return UserConsumableItemDic[ID];
            }
            else
                // 가방에 없다면 0 고정
            {
                return 0;
            }
        }

        // 충분한 소비 아이템을 가지고 있는지 확인한다.
        public bool IsHaveComsumableItem(int ID, int count = 1)
        {
            if (GameManager.Instance.HasData<ConsumableItemData>(ID))
                // 적절한 ID인지 확인
            {
                if (UserConsumableItemDic.ContainsKey(ID))
                    // 가방에 있다면 갯수 비교
                {
                    return UserConsumableItemDic[ID] >= count;
                }
                else
                    // 가방에 없다면 false 고정
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
        // mapID 가 내가 클리어한 맵인지 확인한다.
        public bool IsClearMap(int mapID) => userData.clearMapList.Contains(mapID);

        // 맵을 클리어한다.
        public void ClearMap(int mapID)
        {
            if (!IsClearMap(mapID))
                // 클리어한 맵 리스트에 없다면 추가해준다.
            {
                userData.clearMapList.Add(mapID);
            }
        }

        // 에너지 여분을 확인한다.
        public bool IsLeftEnergy(int consumeEnergy)
        {
            return CurrentEnergy >= consumeEnergy;
        }
    }
}
