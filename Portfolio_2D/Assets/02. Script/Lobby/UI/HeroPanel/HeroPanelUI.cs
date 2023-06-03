using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class HeroPanelUI : MonoBehaviour, UndoAble
    {
        [SerializeField] UnitListUI unitListUI;
        [SerializeField] UnitStatusUI unitStatusUI;
        [SerializeField] UnitEquipmentUI unitEquipmentUI;
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;

        private void Start()
        {
            Unit unit = GameManager.CurrentUser.userUnitDic[0];
            unitEquipmentUI.gameObject.SetActive(false);
            unitSkillPanelUI.gameObject.SetActive(false);
            unitStatusUI.Init(unit);
            unitEquipmentUI.Init(unit);
            unitSkillPanelUI.Init(unit);
        }

        public void Undo()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
            unitEquipmentUI.gameObject.SetActive(false);
        }
    } 
}
