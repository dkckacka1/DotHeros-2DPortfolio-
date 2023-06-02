using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class UnitSkillPanelUI : MonoBehaviour, UndoAble
    {
        private Unit unit;

        [SerializeField] UnitSkillUI activeSkilll_1_UI;
        [SerializeField] UnitSkillUI activeSkilll_2_UI;
        [SerializeField] UnitSkillUI passiveSkill_1_UI;
        [SerializeField] UnitSkillUI passiveSkill_2_UI;

        private void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        public void Init(Unit unit)
        {
            this.unit = unit;

            activeSkilll_1_UI.Init(unit.activeSkill_1, unit.ActiveSkillLevel_1);
            activeSkilll_2_UI.Init(unit.activeSkill_2, unit.ActiveSkillLevel_2);
            passiveSkill_1_UI.Init(unit.passiveSkill_1, unit.PassiveSkillLevel_1);
            passiveSkill_2_UI.Init(unit.passiveSkill_2, unit.PassiveSkillLevel_2);
        }

        public void Undo()
        {
            this.gameObject.SetActive(false);
        }
    }

}