using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.UI
{
    public class UserProfilPortraitSelector : MonoBehaviour
    {
        [SerializeField] RectTransform currentChoice;
        [SerializeField] RectTransform userSelect;

        public Sprite GetCurrentSprite => GetComponent<UnitSlotUI>().CurrentUnit.portraitSprite;
        public bool IsCurrentChoice => currentChoice.gameObject.activeInHierarchy;
        public bool IsUserSelect => userSelect.gameObject.activeInHierarchy;

        public void CurrentChoice()
        {
            currentChoice.gameObject.SetActive(true);
        }

        public void ChangeChoice()
        {
            currentChoice.gameObject.SetActive(false);
        }

        public void UserSelect()
        {
            userSelect.gameObject.SetActive(true);
        }

        public void UnSelect()
        {
            userSelect.gameObject.SetActive(false);
        }

        public void Select(UserPortraitChangePopupUI popupUI)
        {
            if (IsCurrentChoice) return;
            if (IsUserSelect) return;

            popupUI.SelectPortrait(this);
            UserSelect();
        }
    }
}