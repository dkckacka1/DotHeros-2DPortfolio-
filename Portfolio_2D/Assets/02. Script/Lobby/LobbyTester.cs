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
            if (GUI.Button(new Rect(10, 340, 100, 100), "랜덤 아이템 추가하기"))
            {
                EquipmentItemData itemData = null;

                eEquipmentItemType type = (eEquipmentItemType)Random.Range(0, 6);
                switch (type)
                {
                    case eEquipmentItemType.Weapon:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<WeaponData>(eGradeType.Normal);
                        break;
                    case eEquipmentItemType.Helmet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<HelmetData>(eGradeType.Normal);
                        break;
                    case eEquipmentItemType.Armor:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ArmorData>(eGradeType.Normal);
                        break;
                    case eEquipmentItemType.Amulet:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<AmuletData>(eGradeType.Normal);
                        break;
                    case eEquipmentItemType.Ring:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<RingData>(eGradeType.Normal);
                        break;
                    case eEquipmentItemType.Shoe:
                        itemData = GameManager.ItemCreator.CreateEquipmentItemData<ShoeData>(eGradeType.Normal);
                        break;
                    default:
                        Debug.LogWarning("unknownType");
                        break;
                }

                if (itemData == null) return;

                GameManager.CurrentUser.AddEquipmentItem(itemData);
                GameManager.Instance.SaveUser();
            }

            if (GUI.Button(new Rect(10, 450, 100, 100), "골드 추가하기"))
            {
                GameManager.CurrentUser.Gold += 10000000;
            }

            if (GUI.Button(new Rect(110, 450, 100, 100), "다이아 추가하기"))
            {
                GameManager.CurrentUser.Diamond += 1000;
            }
        }
    }
}