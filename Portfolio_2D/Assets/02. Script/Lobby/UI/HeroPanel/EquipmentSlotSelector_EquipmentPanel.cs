using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 장비 패널에서의 장비 슬롯 UI 셀렉터 클래스
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