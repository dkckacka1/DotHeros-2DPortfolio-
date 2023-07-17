using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� ������ �� ������ ǥ�����ִ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitSequenceUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitFrame; // �Ǿƽĺ��� ������ �̹���
        [SerializeField] Image unitPortraitImage; // ������ ��Ʈ����Ʈ �̹���

        // ������ ������ UI ǥ��
        public void ShowUnit(BattleUnit unit)
        {
            unitPortraitImage.sprite = unit.Unit.portraitSprite;
            if (unit.IsEnemy)
            {
                unitPortraitFrame.color = Constant.EnemyUnitColor;
            }
            else
            {
                unitPortraitFrame.color = Constant.PlayerUnitColor;
            }
        }
    }
}