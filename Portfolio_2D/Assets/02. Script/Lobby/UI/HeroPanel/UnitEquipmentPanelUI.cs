using Portfolio.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitEquipmentPanelUI : MonoBehaviour, UndoAble
    {
        [SerializeField] private Animator unitAnim;
        [SerializeField] private EquipmentItemSlot weaponSlot;
        [SerializeField] private EquipmentItemSlot helemtSlot;
        [SerializeField] private EquipmentItemSlot armorSlot;
        [SerializeField] private EquipmentItemSlot shoeSlot;
        [SerializeField] private EquipmentItemSlot amuletSlot;
        [SerializeField] private EquipmentItemSlot ringSlot;

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
