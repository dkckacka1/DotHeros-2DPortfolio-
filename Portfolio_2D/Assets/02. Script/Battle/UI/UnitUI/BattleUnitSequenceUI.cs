using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Battle
{
    public class BattleUnitSequenceUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitFrame;
        [SerializeField] Image unitPortraitImage;

        public void ShowUnit(BattleUnit unit)
        {
            unitPortraitImage.sprite = unit.Unit.portraitSprite;
            if (unit.IsEnemy)
            {
                unitPortraitFrame.color = Constant.enemyColor;
            }
            else
            {
                unitPortraitFrame.color = Constant.playerColor;
            }
        }
    }
}