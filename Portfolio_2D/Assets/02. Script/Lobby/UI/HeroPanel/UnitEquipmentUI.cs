using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitEquipmentUI : MonoBehaviour, UndoAble
    {
        private Unit unit;
        [SerializeField] Image unitImage;
        [SerializeField] UnitEquipmentSlotUI weaponSlot;
        [SerializeField] UnitEquipmentSlotUI helemtSlot;
        [SerializeField] UnitEquipmentSlotUI armorSlot;
        [SerializeField] UnitEquipmentSlotUI shoeSlot;
        [SerializeField] UnitEquipmentSlotUI amuletSlot;
        [SerializeField] UnitEquipmentSlotUI ringSlot;

        private void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        public void Init(Unit unit)
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
            this.gameObject.SetActive(false);
        }
    }

}