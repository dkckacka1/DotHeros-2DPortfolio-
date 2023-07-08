using Portfolio.Lobby;
using Portfolio.Lobby.Hero;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  유닛 정보를 보여줄 유닛 슬롯 UI 클래스
 */

namespace Portfolio.UI
{
    public class UnitSlotUI : MonoBehaviour
    {
        private Unit currentUnit;           // 현재 유닛 정보
        private UnitData currentUnitData;   // 현재 유닛의 유저유닛 데이터

        [SerializeField] Image unitPortraitImage;                   // 유닛의 포트레이트 이미지
        [SerializeField] TextMeshProUGUI unitLevelText;             // 유닛의 레벨 텍스트
        [SerializeField] List<Image> starImages = new List<Image>();// 유닛 등급을 보여줄 별 이미지

        public Unit CurrentUnit => currentUnit;
        public UnitData CurrentUnitData => currentUnitData;

        // 유닛의 정보를 보여줍니다.
        public void ShowUnit(Unit unit, bool isShowLevelText = true, bool isShowGradeImage = true)
        {
            if (unit == null)
            {
                currentUnit = null;
                return;
            }

            this.currentUnit = unit;
            unitPortraitImage.sprite = unit.portraitSprite;
            unitLevelText.text = unit.UnitCurrentLevel.ToString();
            unitLevelText.gameObject.SetActive(isShowLevelText);
            // 유닛 등급만큼 별을 표시합니다.
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < unit.UnitGrade && isShowGradeImage);
            }
        }

        // 유닛 데이터를 토대로 정보를 보여줍니다.
        public void ShowUnit(UnitData unitData, bool isShowGradeImage = true)
        {
            if (unitData == null)
            {
                currentUnitData = null;
                return;
            }

            this.currentUnitData = unitData;
            unitLevelText.gameObject.SetActive(false);
            // 데이터는 스프라이트를 가지고 있지 않으므로 게임매니저에게 요청하여 포트레이트 이미지를 가져옵니다.
            unitPortraitImage.sprite = GameManager.Instance.GetSprite(currentUnitData.portraitImageName);
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < currentUnitData.defaultGrade && isShowGradeImage);
            }
        }
    } 
}
