using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class ManaSystem : MonoBehaviour
    {
        private int mana = Constant.MAX_MANA_COUNT;

        public int Mana 
        { 
            get
            {
                return mana;
            }
            set
            {
                mana = Mathf.Clamp(value, 0, Constant.MAX_MANA_COUNT);
                BattleManager.BattleUIManager.BattleManaUI.SetManaCount(mana);
            }
        }

        public void AddMana(int getMana)
        {
            Mana += getMana;
        }

        public void UseMana(int useMana)
        {
            Mana -= useMana;
        }

        public bool canUseMana(int useMana)
        {
            return mana >= useMana;
        }
    }

}