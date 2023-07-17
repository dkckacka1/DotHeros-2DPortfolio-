using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  전투 유닛의 턴 진행을 표시해주는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitSequenceUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitFrame; // 피아식별할 프레임 이미지
        [SerializeField] Image unitPortraitImage; // 유닛의 포트레이트 이미지

        // 유닛의 턴진행 UI 표시
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