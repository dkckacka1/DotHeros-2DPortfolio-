using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * ���� �������� ǥ��� �ؽ�Ʈ
 */


namespace Portfolio.Battle
{
    public class BattleTextUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI battleText;// ǥ�� �ؽ�Ʈ

        [SerializeField] Color damagedColor;        // �������� �Ծ������� �ؽ�Ʈ ��
        [SerializeField] Color healedColor;         // ü���� ȸ���� ���� �ؽ�Ʈ ��
        [SerializeField] Color manaColor;           // ������ ȸ���� ���� �ؽ�Ʈ ��
        [SerializeField] Color buffColor;           // ������ ������ �ؽ�Ʈ ��
        [SerializeField] Color debuffColor;         // ������� ���� �� �ؽ�Ʈ ��

        // ������ �ؽ�Ʈ�� ���´�.
        public void SetDamage(int damage)
        {
            battleText.color = damagedColor;
            battleText.text = damage.ToString();    
        }

        // �� �ؽ�Ʈ�� ���´�.
        public void SetHeal(int heal)
        {
            battleText.color = healedColor;
            battleText.text = heal.ToString();
        }

        // ���� �ؽ�Ʈ�� ���´�.
        public void SetMana(int manaValue)
        {
            battleText.color = manaColor;
            battleText.text = manaValue.ToString();
        }

        // ���� �ؽ�Ʈ�� ���´�.
        public void SetBuff(string buffName)
        {
            battleText.color = buffColor;
            battleText.text = buffName;
        }

        // ����� �ױ�Ʈ�� ���´�.
        public void SetDebuff(string debuffName)
        {
            battleText.color = debuffColor;
            battleText.text = debuffName;
        }

        // ���� �ؽ�Ʈ�� �ִϸ��̼��� ����� ��� ������Ʈ Ǯ�� ��ȯ�ȴ�.
        public void AnimationEvent_Release()
        {
            BattleManager.ObjectPool.ReleaseBattleText(this);
        }
    }
}