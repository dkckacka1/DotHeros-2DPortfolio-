using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 플레이어가 사용하는 마나 관련 시스템 클래스
 */

namespace Portfolio.Battle
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
                // 마나값이 변화하면 0 ~ Constant.MAX_MANA_COUNT 사이 값으로 조절해준다.
                mana = Mathf.Clamp(value, 0, Constant.MAX_MANA_COUNT);
                // 마나 UI 변화
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