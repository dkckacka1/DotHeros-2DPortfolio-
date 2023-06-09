using Portfolio.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitEquipmentUI : MonoBehaviour, UndoAble
    {
        [SerializeField] private Image unitImage;
        [SerializeField] private UnitEquipmentSlotUI weaponSlot;
        [SerializeField] private UnitEquipmentSlotUI helemtSlot;
        [SerializeField] private UnitEquipmentSlotUI armorSlot;
        [SerializeField] private UnitEquipmentSlotUI shoeSlot;
        [SerializeField] private UnitEquipmentSlotUI amuletSlot;
        [SerializeField] private UnitEquipmentSlotUI ringSlot;

        private void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        public void ShowEquipment(Unit unit)
        {
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
