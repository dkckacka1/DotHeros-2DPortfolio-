using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��� �гο����� ��� ���� UI ������ Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentSlotSelector_EquipmentPanel : MonoBehaviour
    {
        EquipmentItemSlot equipmentItemSlot;

        private void Awake()
        {
            equipmentItemSlot = GetComponent<EquipmentItemSlot>();
        }

        public void BTN_OnClick_SelectEquipmentItem()
        {
            HeroPanelUI.SelectEquipmentItem = equipmentItemSlot.EquipmentData;
            HeroPanelUI.SelectEquipmentItemType = equipmentItemSlot.EquipmentItemType;
        }
    }
}