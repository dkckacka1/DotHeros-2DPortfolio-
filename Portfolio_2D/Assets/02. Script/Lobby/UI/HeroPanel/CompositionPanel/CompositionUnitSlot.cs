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
        [SerializeField] Sprite defaultSprite;
        [SerializeField] Image selectImage;

        [HideInInspector] public UnitSlotHeroCompositionSelector selector;
        private bool isSelect;
        Unit unit;

        public Unit CurrentUnit
        {
            get => unit;
        }

        public bool IsChoiceUnit
        {
            get => unit != null;
        }
        public bool IsSelect 
        {
            get => isSelect; 
            set
            {
                isSelect = value;
                selectImage.gameObject.SetActive(isSelect);
            }
        }

        public void Reset()
        {
            unit = null;
            if (selector != null)
            {
                selector.ResetSelect();
                selector = null;
            }
            IsSelect = false;
            unitPortraitImage.sprite = defaultSprite;
            lockMask.gameObject.SetActive(false);
            unitPortraitMask.gameObject.SetActive(true);
            unitNameText.gameObject.SetActive(false);
            gradeLayout.gameObject.SetActive(false);
        }

        public void ShowLock()
        {
            unit = null;
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