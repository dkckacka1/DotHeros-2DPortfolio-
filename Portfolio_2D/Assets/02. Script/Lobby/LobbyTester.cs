using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyTester : MonoBehaviour
    {
        [SerializeField] int unitID = 100;
        [SerializeField] eEquipmentItemType equipmentItemType = eEquipmentItemType.Weapon;
        [SerializeField] eGradeType randomType = eGradeType.Normal;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 100), "랜덤 아이템 추가하기"))
            {
                EquipmentItemData itemData = null;

                switch (equipmentItemType)
                {
                    case eEquipmentItemType.Weapon:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(randomType);
                        break;
                    case eEquipmentItemType.Helmet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(randomType);
                        break;
                    case eEquipmentItemType.Armor:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(randomType);
                        break;
                    case eEquipmentItemType.Amulet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(randomType);
                        break;
                    case eEquipmentItemType.Ring:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(randomType);
                        break;
                    case eEquipmentItemType.Shoe:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(randomType);
                        break;
                    default:
                        Debug.LogWarning("unknownType");
                        break;
                }

                if (itemData == null) return;

                GameManager.CurrentUser.AddEquipmentItem(itemData);
                // SAVE : 
                //GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(10, 110, 100, 100), "최강 유닛 생성하기"))
            {
                if (GameManager.Instance.TryGetData(unitID, out UnitData data))
                {
                    UserUnitData unitData = new UserUnitData(data);
                    unitData.unitLevel = 50;
                    unitData.unitGrade = 5;
                    Unit summonUnit = new Unit(data, unitData);
                    GameManager.CurrentUser.AddNewUnit(summonUnit);
                    // SAVE : 
                    //GameManager.Instance.SaveUser();
                }
            }

            if (GUI.Button(new Rect(10, 210, 100, 100), "모든맵 클리어하기"))
            {
                var list = GameManager.Instance.GetDatas<MapData>();
                foreach (var mapData in list)
                {
                    GameManager.CurrentUser.ClearMap(mapData.ID);
                }
            }
        }
    }
}