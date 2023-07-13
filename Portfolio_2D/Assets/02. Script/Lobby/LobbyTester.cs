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