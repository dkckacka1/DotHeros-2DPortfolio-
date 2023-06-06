using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class EquipmentSelectUI : MonoBehaviour
    {
        private UnitEquipmentSlotUI equipmentSlot;

        [SerializeField] Image selectedImage;

        private void Awake()
        {
            equipmentSlot = GetComponent<UnitEquipmentSlotUI>();
        }
    }
}