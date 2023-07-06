using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Portfolio.UI;
using UnityEngine.UI;
using TMPro;

namespace Portfolio.Lobby.Inventory
{
    public class EquipmentInventory : MonoBehaviour
    {
        private List<UnitEquipmentSlotUI> equipmentSlotUIList;

        [SerializeField] TextMeshProUGUI equipmentListSizeText;
        [SerializeField] ScrollRect slotScrollView;
        [SerializeField] Toggle multipleSelectionToggle;

        public bool IsMultipleSelection => multipleSelectionToggle.isOn;

        public void Init()
        {
            equipmentSlotUIList = new List<UnitEquipmentSlotUI>();
            foreach (var slot in slotScrollView.content.GetComponentsInChildren<UnitEquipmentSlotUI>())
            {
                equipmentSlotUIList.Add(slot);
            }
        }

        private void OnEnable()
        {
            var list = GameManager.CurrentUser.userEquipmentItemDataList;
            for (int i = 0; i < equipmentSlotUIList.Count; i++)
            {
                if (list.Count <= i)
                {
                    equipmentSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                equipmentSlotUIList[i].ShowEquipment(list[i]);
                equipmentSlotUIList[i].gameObject.SetActive(true);
            }

            equipmentListSizeText.text = $"{list.Count} / {GameManager.CurrentUser.MaxEquipmentListCount}";
        }

        public Vector2 GetScrollViewMiddlePoint()
        {
            return (slotScrollView.transform as RectTransform).position;
        }
    }

}