using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  유저가 선택한 스킬을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] UnitSkillUI activeSkilll_1_UI; // 유저 액티브 스킬 1
        [SerializeField] UnitSkillUI activeSkilll_2_UI; // 유저 액티브 스킬 2
        [SerializeField] UnitSkillUI passiveSkill_1_UI; // 유저 패시브 스킬 1
        [SerializeField] UnitSkillUI passiveSkill_2_UI; // 유저 패시브 스킬 3

        internal void Init()
        {
            // 유저가 선택한 유닛이 변경될 때 UI를 업데이트한다.
            LobbyManager.UIManager.unitChangedEvent += ShowSkill;
        }
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // 유저가 선택한 유닛의 스킬을 보여줍니다.
        public void ShowSkill(object sender, EventArgs eventArgs)
        {
            // 유저가 선택한 유닛을 참조합니다.
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            // 스킬을 보여줍니다.
            activeSkilll_1_UI.Show(unit.activeSkill_1, unit.ActiveSkillLevel_1, true, UnitSkillType.ActiveSkill_1);
            activeSkilll_2_UI.Show(unit.activeSkill_2, unit.ActiveSkillLevel_2, true, UnitSkillType.ActiveSkill_2);
            passiveSkill_1_UI.Show(unit.passiveSkill_1, unit.PassiveSkillLevel_1, true, UnitSkillType.PassiveSkill_1);
            passiveSkill_2_UI.Show(unit.passiveSkill_2, unit.PassiveSkillLevel_2, true, UnitSkillType.PassiveSkill_2);
        }

        public void Undo()
        {
            this.gameObject.SetActive(false);
        }
    }
}