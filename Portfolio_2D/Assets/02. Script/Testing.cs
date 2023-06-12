using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio
{
    public class Testing : MonoBehaviour
    {
        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.T))
            //{
            //    GameManager.Instance.TryGetUnit(100, out Unit unit);

            //    if (BattleManager.BattleFactory.TryCreateBattleUnit(unit, false, out BattleUnit battleUnit))
            //    {
            //        battleUnit.Speed = Random.Range(50, 101);
            //        BattleManager.Instance.AddUnitinUnitList(battleUnit);
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.Y))
            //{
            //    GameManager.Instance.TryGetUnit(100, out Unit unit);

            //    if (BattleManager.BattleFactory.TryCreateBattleUnit(unit, true, out BattleUnit battleUnit))
            //    {
            //        battleUnit.Speed = Random.Range(50, 101);
            //        BattleManager.Instance.AddUnitinUnitList(battleUnit);
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    {
            //        GameManager.Instance.TryGetUnit(101, out Unit unit);

            //        if (BattleManager.BattleFactory.TryCreateBattleUnit(unit, false, out BattleUnit battleUnit))
            //        {
            //            battleUnit.Speed = Random.Range(50, 101);
            //            BattleManager.Instance.AddUnitinUnitList(battleUnit);
            //        }
            //    }
            //}
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 100), "전투 시작"))
            {
                List<Unit> units = GameManager.CurrentUser.userUnitList.OrderByDescending(unit => unit.UnitCurrentLevel).ThenByDescending(unit => unit.UnitGrade).Take(5).ToList();

                GameManager.Instance.TryGetMap(500, out Map map);

                SceneLoader.LoadBattleScene(units, map);
            }

            if (GUI.Button(new Rect(10, 120, 100, 100), "테스트용 유닛 넣기(1)"))
            {
                UserData userdata = GameManager.CurrentUser.userData;

                GameManager.Instance.TryGetData(100, out UnitData unitdata);
                UserUnitData userUnitData = new UserUnitData(unitdata);

                userUnitData.weaponData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(GradeType.Normal);
                userUnitData.helmetData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(GradeType.Normal);
                userUnitData.armorData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(GradeType.Normal);
                userUnitData.shoeData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(GradeType.Normal);
                userUnitData.amuletData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(GradeType.Normal);
                userUnitData.RingData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(GradeType.Normal);

                GameManager.CurrentUser.AddNewUnit(new Unit(unitdata, userUnitData));

                GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(120, 120, 100, 100), "테스트용 유닛 넣기(2)"))
            {
                UserData userdata = GameManager.CurrentUser.userData;

                GameManager.Instance.TryGetData(101, out UnitData unitdata);
                UserUnitData userUnitData = new UserUnitData(unitdata);

                userUnitData.weaponData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(GradeType.Normal);
                userUnitData.helmetData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(GradeType.Normal);
                userUnitData.armorData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(GradeType.Normal);
                userUnitData.shoeData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(GradeType.Normal);
                userUnitData.amuletData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(GradeType.Normal);
                userUnitData.RingData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(GradeType.Normal);

                GameManager.CurrentUser.AddNewUnit(new Unit(unitdata, userUnitData));

                GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(10, 230, 100, 100), "유저 로드 데이터 테스트"))
            {
                UserData userdata = GameManager.CurrentUser.userData;
                Debug.Log($"" +
                    $"userID = {userdata.userID}\n" +
                    $"userName = {userdata.userName}\n");

                foreach (var userUnit in userdata.unitDataList)
                {
                    Debug.Log($"" +
                        $"userUnitID = {userUnit.unitID}");
                }
            }

            if (GUI.Button(new Rect(10, 340, 100, 100), "랜덤 아이템 추가하기"))
            {
                EquipmentItemData itemData = null;

                EquipmentItemType type = (EquipmentItemType)Random.Range(0, 6);
                switch (type)
                {
                    case EquipmentItemType.Weapon:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(GradeType.Normal);
                        break;
                    case EquipmentItemType.Helmet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(GradeType.Normal);
                        break;
                    case EquipmentItemType.Armor:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(GradeType.Normal);
                        break;
                    case EquipmentItemType.Amulet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(GradeType.Normal);
                        break;
                    case EquipmentItemType.Ring:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(GradeType.Normal);
                        break;
                    case EquipmentItemType.Shoe:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(GradeType.Normal);
                        break;
                }

                if (itemData == null) return;

                GameManager.CurrentUser.userEquipmentItemDataList.Add(itemData);
                GameManager.Instance.SaveUser();
            }
        }
    }
}