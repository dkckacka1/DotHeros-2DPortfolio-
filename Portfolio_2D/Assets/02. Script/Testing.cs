using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
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
                List<Unit> units = new List<Unit>();
                foreach (var userUnitData in GameManager.CurrentUser.unitDataList)
                {
                    if (GameManager.Instance.TryGetData<UnitData>(userUnitData.unitID, out UnitData unitData))
                    {
                        Unit unit = new Unit(unitData, userUnitData);
                        units.Add(unit);
                    }
                }

                foreach (var unit in units)
                {
                    Debug.Log(unit.Data.unitName);
                }

                GameManager.Instance.TryGetData<MapData>(500, out MapData mapData);

                SceneLoader.LoadBattleScene(units, mapData);
            }

            if (GUI.Button(new Rect(10, 120, 100, 100), "테스트용 유닛 넣기(1)"))
            {
                UserData userdata = GameManager.CurrentUser;

                UserUnitData userUnitData = new UserUnitData();
                userUnitData.unitID = 100;
                userUnitData.unitLevel = 100;
                userUnitData.unitGrade = 5;
                userUnitData.activeSkillLevel_1 = 5;
                userUnitData.activeSkillLevel_2 = 5;
                userUnitData.passiveSkillLevel_1 = 5;
                userUnitData.passiveSkillLevel_2 = 5;
                userdata.unitDataList.Add(userUnitData);

                ArmorData armorData = new ArmorData();
                userUnitData.armorData = armorData;

                SaveManager.SaveUserData(userdata);
            }

            if (GUI.Button(new Rect(120, 120, 100, 100), "테스트용 유닛 넣기(2)"))
            {
                UserData userdata = GameManager.CurrentUser;

                UserUnitData userUnitData = new UserUnitData();
                userUnitData.unitID = 101;
                userUnitData.unitLevel = 100;
                userUnitData.unitGrade = 5;
                userUnitData.activeSkillLevel_1 = 5;
                userUnitData.activeSkillLevel_2 = 5;
                userUnitData.passiveSkillLevel_1 = 5;
                userUnitData.passiveSkillLevel_2 = 5;
                userdata.unitDataList.Add(userUnitData);

                WeaponData weaponData = new WeaponData();
                userUnitData.weaponData = weaponData;
                SaveManager.SaveUserData(userdata);
            }

            if (GUI.Button(new Rect(10, 230, 100, 100), "유저 로드 데이터 테스트"))
            {
                UserData userdata = GameManager.CurrentUser;
                Debug.Log($"" +
                    $"userID = {userdata.userID}\n" +
                    $"userName = {userdata.userName}\n");

                foreach (var userUnit in userdata.unitDataList)
                {
                    Debug.Log($"" +
                        $"userUnitID = {userUnit.unitID}");
                }
            }
        }
    }
}