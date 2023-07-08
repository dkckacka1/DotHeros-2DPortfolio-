using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  ������ ������ ��ų�� �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] UnitSkillUI activeSkilll_1_UI; // ���� ��Ƽ�� ��ų 1
        [SerializeField] UnitSkillUI activeSkilll_2_UI; // ���� ��Ƽ�� ��ų 2
        [SerializeField] UnitSkillUI passiveSkill_1_UI; // ���� �нú� ��ų 1
        [SerializeField] UnitSkillUI passiveSkill_2_UI; // ���� �нú� ��ų 3

        internal void Init()
        {
            // ������ ������ ������ ����� �� UI�� ������Ʈ�Ѵ�.
            LobbyManager.UIManager.unitChangedEvent += ShowSkill;
        }
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        // ������ ������ ������ ��ų�� �����ݴϴ�.
        public void ShowSkill(object sender, EventArgs eventArgs)
        {
            // ������ ������ ������ �����մϴ�.
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            // ��ų�� �����ݴϴ�.
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