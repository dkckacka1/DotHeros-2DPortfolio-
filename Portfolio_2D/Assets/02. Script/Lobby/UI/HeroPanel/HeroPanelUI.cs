using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class HeroPanelUI : MonoBehaviour
    {
        [SerializeField] UnitListUI unitListUI;
        [SerializeField] UnitStatusUI unitStatusUI;
        [SerializeField] UnitEquipmentUI unitEquipmentUI;

        private void OnEnable()
        {
            unitEquipmentUI.gameObject.SetActive(false);
        }
    } 
}
