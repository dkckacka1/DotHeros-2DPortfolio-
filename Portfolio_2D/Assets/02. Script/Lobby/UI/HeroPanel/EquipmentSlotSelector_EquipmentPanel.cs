using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유닛 장비 패널에서의 장비 슬롯 UI 셀렉터 클래스
 * 장비 슬롯 UI 컴포넌트가 부착된 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(EquipmentItemSlot))]
    public class EquipmentSlotSelector_EquipmentPanel : MonoBehaviour
    {
        EquipmentItemSlot equipmentItemSlot;    // 현재 장비 슬롯

        private void Awake()
        {
            equipmentItemSlot = GetComponent<EquipmentItemSlot>();
        }

        // 장비 슬롯의 정보를 영웅창에 전달해준다.
        public void BTN_OnClick_SelectEquipmentItem()
        {
            HeroPanelUI.SelectEquipmentItem = equipmentItemSlot.EquipmentData;
            HeroPanelUI.SelectEquipmentItemType = equipmentItemSlot.EquipmentItemType;
        }
    }
}