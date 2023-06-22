using Portfolio.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyTester : MonoBehaviour
    {
        [SerializeField] string alertText;
     
        private void OnGUI()
        {

            if (GUI.Button(new Rect(10, 120, 100, 100), "�׽�Ʈ�� ���� �ֱ�(1)"))
            {
                GameManager.Instance.TryGetData(100, out UnitData unitdata);
                UserUnitData userUnitData = new UserUnitData(unitdata);

                userUnitData.weaponData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(GradeType.Normal);
                userUnitData.helmetData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(GradeType.Normal);
                userUnitData.armorData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(GradeType.Normal);
                userUnitData.shoeData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(GradeType.Normal);
                userUnitData.amuletData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(GradeType.Normal);
                userUnitData.ringData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(GradeType.Normal);

                GameManager.CurrentUser.AddNewUnit(new Unit(unitdata, userUnitData));

                GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(120, 120, 100, 100), "�׽�Ʈ�� ���� �ֱ�(2)"))
            {
                GameManager.Instance.TryGetData(101, out UnitData unitdata);
                UserUnitData userUnitData = new UserUnitData(unitdata);

                userUnitData.weaponData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(GradeType.Normal);
                userUnitData.helmetData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(GradeType.Normal);
                userUnitData.armorData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(GradeType.Normal);
                userUnitData.shoeData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(GradeType.Normal);
                userUnitData.amuletData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(GradeType.Normal);
                userUnitData.ringData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(GradeType.Normal);

                GameManager.CurrentUser.AddNewUnit(new Unit(unitdata, userUnitData));

                GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(10, 340, 100, 100), "���� ������ �߰��ϱ�"))
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

            if (GUI.Button(new Rect(10, 450, 100, 100), "��� �߰��ϱ�"))
            {
                GameManager.CurrentUser.Gold += 10000000;
            }

            if (GUI.Button(new Rect(110, 450, 100, 100), "���̾� �߰��ϱ�"))
            {
                GameManager.CurrentUser.Diamond += 1000;
            }

            if (GUI.Button(new Rect(10, 560, 100, 100), "��� ǥ���ϱ�"))
            {
                GameManager.UIManager.ShowAlert(alertText);
            }

            if (GUI.Button(new Rect(110, 560, 100, 100), "Ȯ��â ǥ���ϱ�"))
            {
                GameManager.UIManager.ShowConfirmation("���â ǥ��", "���â�� �Ȳ��ðڽ��ϱ�?", () => { Debug.Log("���̾�!"); });
            }
        }
    }
}