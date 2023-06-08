using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class UnitSlotUI : MonoBehaviour
    {
        private Unit currentUnit;

        [SerializeField] Image unitPortraitImage;
        [SerializeField] TextMeshProUGUI unitLevelText;
        [SerializeField] List<Image> starImages = new List<Image>();

        public void Init(Unit unit)
        {
            this.currentUnit = unit;
            unitPortraitImage.sprite = unit.portraitImage;
            unitLevelText.text = unit.UnitCurrentLevel.ToString();
            for (int i = 0; i < 5; i++)
            {
                starImages[i].gameObject.SetActive(i < unit.UnitGrade);
            }
        }

        public void SelectUnit(HeroPanelUI heroPanelUI)
        {
            heroPanelUI.SelectUnit(currentUnit);
        }
    } 
}
