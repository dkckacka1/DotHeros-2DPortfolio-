using Portfolio.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ������ ��� �������� �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitEquipmentPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] private Animator unitAnim;             // ���� ǥ�õ� ����
        [SerializeField] private EquipmentItemSlot weaponSlot;  // ������ ������ ���� ����
        [SerializeField] private EquipmentItemSlot helemtSlot;  // ������ ������ ��� ����
        [SerializeField] private EquipmentItemSlot armorSlot;   // ������ ������ ���� ����
        [SerializeField] private EquipmentItemSlot shoeSlot;    // ������ ������ �Ź� ����
        [SerializeField] private EquipmentItemSlot amuletSlot;  // ������ ������ ����� ����
        [SerializeField] private EquipmentItemSlot ringSlot;    // ������ ������ ���� ����
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            // ������ ������ �ٲ�� UI�� ������Ʈ�ϵ��� ����
            LobbyManager.UIManager.unitChangedEvent += ShowEquipment;
        }

        // â�� ������ ���� ����
        private void OnDisable()
        {
            LobbyManager.UIManager.unitChangedEvent -= ShowEquipment;
        }


        // ������ �������� �����ݴϴ�.
        public void ShowEquipment(object sender, EventArgs eventArgs)
        {
            // ������ ������ ������ �����մϴ�.
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            // �ִϸ��̼��� �����մϴ�.
            unitAnim.runtimeAnimatorController = unit.animController;
            unitAnim.Play("IDLE");

            // ��� ���Կ� ������ ��� �������� �����ݴϴ�.
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
