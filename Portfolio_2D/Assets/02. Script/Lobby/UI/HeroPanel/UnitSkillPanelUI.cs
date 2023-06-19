using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillPanelUI : MonoBehaviour, UndoAble
    {
        [SerializeField] UnitSkillUI activeSkilll_1_UI;
        [SerializeField] UnitSkillUI activeSkilll_2_UI;
        [SerializeField] UnitSkillUI passiveSkill_1_UI;
        [SerializeField] UnitSkillUI passiveSkill_2_UI;
        internal void Init()
        {
            LobbyManager.UIManager.unitChangedEvent += ShowSkill;
        }
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void ShowSkill(object sender, EventArgs eventArgs)
        {
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            activeSkilll_1_UI.Init(unit.activeSkill_1, unit.ActiveSkillLevel_1, true, UnitSkillType.ActiveSkill_1);
            activeSkilll_2_UI.Init(unit.activeSkill_2, unit.ActiveSkillLevel_2, true, UnitSkillType.ActiveSkill_2);
            passiveSkill_1_UI.Init(unit.passiveSkill_1, unit.PassiveSkillLevel_1, true, UnitSkillType.PassiveSkill_1);
            passiveSkill_2_UI.Init(unit.passiveSkill_2, unit.PassiveSkillLevel_2, true, UnitSkillType.PassiveSkill_2);
        }

        public void Undo()
        {
            this.gameObject.SetActive(false);
        }
    }
}