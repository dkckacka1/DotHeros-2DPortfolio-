using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
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
            weaponSlot.Init(unit.weaponData);
            helemtSlot.Init(unit.helmetData);
            armorSlot.Init(unit.armorData);
            shoeSlot.Init(unit.shoeData);
            amuletSlot.Init(unit.amuletData);
            ringSlot.Init(unit.ringData);
        }

        public void Undo()
        {
            gameObject.SetActive(false);
        }
    }
}
