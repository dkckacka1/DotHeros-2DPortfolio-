using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class HeroPanelUI : PanelUI
    {
        [SerializeField] UnitListUI unitListUI;
        [SerializeField] UnitStatusUI unitStatusUI;
        [SerializeField] UnitEquipmentUI unitEquipmentUI;
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;


        private void Start()
        {
            Unit unit = GameManager.CurrentUser.userUnitDic[0];
            unitStatusUI.Init(unit);
            unitEquipmentUI.Init(unit);
            unitSkillPanelUI.Init(unit);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            unitEquipmentUI.gameObject.SetActive(false);
            unitSkillPanelUI.gameObject.SetActive(false);
        }
    } 
}
