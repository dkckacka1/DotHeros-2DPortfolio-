using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class UnitSkillUI : MonoBehaviour
    {
        private BattleUnit unit;

        [SerializeField] Button turnEndBtn;
        [SerializeField] Button BasicAttackBtn;
        [SerializeField] Button activeSkill_1_ActionBtn;
        [SerializeField] Button activeSkill_2_ActionBtn;
        [SerializeField] Button actionBtn;

        private Skill basicAttackSkill;
        private Skill activeSkill_1;
        private Skill activeSkill_2;

        private void Start()
        {
            
        }

        public void SetUnit(BattleUnit unit)
        {
            this.unit = unit;
        }

        public void ShowSkillUI() => this.gameObject.SetActive(true);
        public void HideSkillUI() => this.gameObject.SetActive(false);

        //===========================================================
        // ButtonPlugin
        //===========================================================
        public void TurnEnd()
        {
            BattleManager.TurnBaseSystem.TurnEnd();
        }

        public void BasicAttack()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
        }

        public void ActiveSkill_1_Action()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
        }

        public void ActiveSkill_2_Action()
        {
            BattleManager.ActionSystem.ClearSelectedUnits();
        }
    }
}