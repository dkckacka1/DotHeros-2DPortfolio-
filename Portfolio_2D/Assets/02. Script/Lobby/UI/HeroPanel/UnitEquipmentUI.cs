using Portfolio.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitEquipmentUI : MonoBehaviour, UndoAble
    {
        [SerializeField] private Animator unitAnim;
        [SerializeField] private UnitEquipmentSlotUI weaponSlot;
        [SerializeField] private UnitEquipmentSlotUI helemtSlot;
        [SerializeField] private UnitEquipmentSlotUI armorSlot;
        [SerializeField] private UnitEquipmentSlotUI shoeSlot;
        [SerializeField] private UnitEquipmentSlotUI amuletSlot;
        [SerializeField] private UnitEquipmentSlotUI ringSlot;

        internal void Init()
        {
            LobbyManager.UIManager.unitChangedEvent += ShowEquipment;
        }

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            unitAnim.runtimeAnimatorController = unit.animController;
            unitAnim.Play("IDLE");
            weaponSlot.ShowEquipment(unit.weaponData);
            helemtSlot.ShowEquipment(unit.helmetData);
            armorSlot.ShowEquipment(unit.armorData);
            shoeSlot.ShowEquipment(unit.shoeData);
            amuletSlot.ShowEquipment(unit.amuletData);
            ringSlot.ShowEquipment(unit.ringData);
        }

        public void Undo()
        {
            gameObject.SetActive(false);
        }
    }
}
