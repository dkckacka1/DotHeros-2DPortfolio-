using Portfolio.Lobby;
using Portfolio.Lobby.Hero;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.UI
{
    public class UnitSlotUI : MonoBehaviour
    {
        private Unit currentUnit;

        [SerializeField] Image unitPortraitImage;
        [SerializeField] TextMeshProUGUI unitLevelText;
        [SerializeField] List<Image> starImages = new List<Image>();

        public Unit CurrentUnit { get => currentUnit; }

        public void Init(Unit unit, bool isShowLevelText = true, bool isShowGradeImage = true)
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
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < unit.UnitGrade && isShowGradeImage);
            }
        }

        public void HeroPanelSelectUnit()
        {
            HeroPanelUI.SelectUnit = currentUnit;
        }
    } 
}
