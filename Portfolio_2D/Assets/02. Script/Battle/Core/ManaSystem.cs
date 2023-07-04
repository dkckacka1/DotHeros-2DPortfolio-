using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �÷��̾ ����ϴ� ���� ���� �ý��� Ŭ����
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
                // �������� ��ȭ�ϸ� 0 ~ Constant.MAX_MANA_COUNT ���� ������ �������ش�.
                mana = Mathf.Clamp(value, 0, Constant.MAX_MANA_COUNT);
                // ���� UI ��ȭ
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