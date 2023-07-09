using Portfolio.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * 유닛의 장착한 장비 아이템을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitEquipmentPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] private Animator unitAnim;             // 현재 표시될 유닛
        [SerializeField] private EquipmentItemSlot weaponSlot;  // 유닛이 장착한 무기 슬롯
        [SerializeField] private EquipmentItemSlot helemtSlot;  // 유닛이 장착한 헬멧 슬롯
        [SerializeField] private EquipmentItemSlot armorSlot;   // 유닛이 장착한 갑옷 슬롯
        [SerializeField] private EquipmentItemSlot shoeSlot;    // 유닛이 장착한 신발 슬롯
        [SerializeField] private EquipmentItemSlot amuletSlot;  // 유닛이 장착한 목걸이 슬롯
        [SerializeField] private EquipmentItemSlot ringSlot;    // 유닛이 장착한 반지 슬롯
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            // 선택한 유닛이 바뀌면 UI를 업데이트하도록 구독
            LobbyManager.UIManager.unitChangedEvent += ShowEquipment;
        }

        // 창이 꺼지면 구독 해제
        private void OnDisable()
        {
            LobbyManager.UIManager.unitChangedEvent -= ShowEquipment;
        }


        // 장착한 아이템을 보여줍니다.
        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            // 유저가 선택한 유닛을 참조합니다.
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            // 애니메이션을 변경합니다.
            unitAnim.runtimeAnimatorController = unit.animController;
            unitAnim.Play("IDLE");

            // 장비 슬롯에 장착한 장비 아이템을 보여줍니다.
            weaponSlot.ShowEquipment(unit.WeaponData);
            helemtSlot.ShowEquipment(unit.HelmetData);
            armorSlot.ShowEquipment(unit.ArmorData);
            shoeSlot.ShowEquipment(unit.ShoeData);
            amuletSlot.ShowEquipment(unit.AmuletData);
            ringSlot.ShowEquipment(unit.RingData);
        }

        public void Undo()
        {
            gameObject.SetActive(false);
        }
    }
}
