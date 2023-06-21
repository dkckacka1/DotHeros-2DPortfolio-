using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class CompositionUnitSlot : MonoBehaviour
    {
        [SerializeField] RectTransform unitPortraitMask;
        [SerializeField] RectTransform lockMask;
        [SerializeField] Image unitPortraitImage;
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] GridLayoutGroup gradeLayout;
        [SerializeField] Toggle toggle;
        [SerializeField] Sprite defaultSprite;

        Unit unit;

        public Unit Unit
        {
            get => unit;
        }

        public bool IsChoiceUnit
        {
            get => unit != null;
        }

        public void Reset()
        {
            unit = null;
            toggle.onValueChanged.Invoke(false);
            unitPortraitImage.sprite = defaultSprite;
            lockMask.gameObject.SetActive(false);
            unitPortraitMask.gameObject.SetActive(true);
            unitNameText.gameObject.SetActive(false);
            gradeLayout.gameObject.SetActive(false);
            toggle.interactable = true;
        }

        public void ShowLock()
        {
            unit = null;
            toggle.interactable = false;
            lockMask.gameObject.SetActive(true);
            unitPortraitMask.gameObject.SetActive(false);
            unitNameText.gameObject.SetActive(false);
            gradeLayout.gameObject.SetActive(false);
        }

        public void ShowUnit(Unit unit)
        {
            this.unit = unit;
            unitNameText.text = unit.UnitName;
            unitPortraitImage.sprite = unit.portraitSprite;
            SetGrade(unit.UnitGrade);

            lockMask.gameObject.SetActive(false);
            unitPortraitMask.gameObject.SetActive(true);
            unitNameText.gameObject.SetActive(true);
            gradeLayout.gameObject.SetActive(true);
        }

        public void SelectCompositionSlot(UnitCompositionPanelUI unitCompositionPanelUI)
        {
            if (toggle.isOn)
            {
                unitCompositionPanelUI.SelectedCompositionUnitSlot = this;
            }
        }


        private void SetGrade(int grade)
        {
            for (int i = 0; i < 5; i++)
            {
                if (grade <= i)
                {
                    gradeLayout.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    gradeLayout.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}