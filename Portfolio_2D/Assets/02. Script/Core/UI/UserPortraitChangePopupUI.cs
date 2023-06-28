using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.UI
{
    public class UserPortraitChangePopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect portraitScrollView;

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();

        UserProfilPortraitSelector currentChoice;
        UserProfilPortraitSelector selectPortrait;

        public void Awake()
        {
            foreach (var slot in portraitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(slot);
            }
            this.gameObject.SetActive(false);
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            var collectList = GameManager.CurrentUser.GetUserCollectUnitList();
            string currentPortraitName = GameManager.CurrentUser.UserPortrait.name;
            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if (collectList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].Init(collectList[i], false, false);
                if (unitSlotUIList[i].CurrentUnit.portraitSprite.name == currentPortraitName)
                {
                    currentChoice = unitSlotUIList[i].GetComponent<UserProfilPortraitSelector>();
                    currentChoice.CurrentChoice();
                }

                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            selectPortrait = null;
            foreach (var slot in unitSlotUIList)
            {
                slot.GetComponent<UserProfilPortraitSelector>().UnSelect();
            }
        }

        public void SelectPortrait(UserProfilPortraitSelector userProfilPortraitSelector)
        {
            if (selectPortrait != null)
            {
                selectPortrait.UnSelect();
            }

            selectPortrait = userProfilPortraitSelector;
            selectPortrait.UserSelect();
        }

        public void ChangePortrait()
        {
            currentChoice.ChangeChoice();
            GameManager.CurrentUser.UserPortrait = selectPortrait.GetCurrentSprite;
            GameManager.UIManager.ShowUserInfo();
            GameManager.Instance.SaveUser();
            this.gameObject.SetActive(false);
        }
    }

}