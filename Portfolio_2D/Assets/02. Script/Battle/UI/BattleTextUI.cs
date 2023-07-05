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

        // ���� �ؽ�Ʈ�� �ִϸ��̼��� ����� ��� ������Ʈ Ǯ�� ��ȯ�ȴ�.
        public void AnimationEvent_Release()
        {
            BattleManager.ObjectPool.ReleaseBattleText(this);
        }
    }

}