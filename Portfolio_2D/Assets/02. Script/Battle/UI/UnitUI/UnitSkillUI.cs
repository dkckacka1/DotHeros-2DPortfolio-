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
    }
}