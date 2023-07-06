using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Selector = Portfolio.UI.UnitSlotSelector_UserPortraitChange; // ������ �̸��� �ʹ� �� ���� ���


/*
 *  ���� �̹��� ���� �˾� UI Ŭ����
 */

namespace Portfolio.UI
{
    public class UserPortraitChangePopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect portraitScrollView;             // ���� ��Ʈ����Ʈ �̹��� ��ũ�� ��

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();   // ���� ���� ��Ʈ����Ʈ ����Ʈ

        Selector currentChoice;                   // ���� ������� ���� ���� ��Ʈ����Ʈ
        Selector selectPortrait;                  // ���� ������ ���� ���� ��Ʈ����Ʈ

        public void Awake()
        {
            // �ʱ� ����
            foreach (var slot in portraitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(slot);
            }
            this.gameObject.SetActive(false);
        }

        // �˾�â�� ������ ������ ������ �̹����� �ʱ�ȭ ���ش�.
        private void OnDisable()
        {
            selectPortrait = null;
            foreach (var slot in unitSlotUIList)
            {
                slot.GetComponent<Selector>().UnSelect();
            }
        }

        // �˾�â�� ǥ���Ѵ�.
        public void Show()
        {
            this.gameObject.SetActive(true);
            // ������ �������ִ� �ߺ� ���� ���� ����Ʈ
            var collectList = GameManager.CurrentUser.GetUserCollectUnitList();
            // ���� ������� ���� ��Ʈ����Ʈ �̹���
            string currentPortraitName = GameManager.CurrentUser.UserPortrait.name;
            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                // ������ ������ �ִ� ���� ���Ը� ǥ��
                if (collectList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // ���� ���� ����
                unitSlotUIList[i].Init(collectList[i], false, false);
                if (unitSlotUIList[i].CurrentUnit.portraitSprite.name == currentPortraitName)
                    // ���� ���� ���� �̹����� ���� �̹��� ��Ʈ����Ʈ �̸��� ���ٸ�
                {
                    // ���� ����� �̹��� ǥ��
                    currentChoice = unitSlotUIList[i].GetComponent<UnitSlotSelector_UserPortraitChange>();
                    currentChoice.CurrentChoice();
                }

                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        // ���� ��Ʈ����Ʈ �̹����� �����Ѵ�.
        public void SelectPortrait(Selector userProfilPortraitSelector)
        {
            if (selectPortrait != null)
                // ���� ������ �̹����� �ִٸ� ���� ��� ���ش�.
            {
                selectPortrait.UnSelect();
            }

            // ���Ӱ� ������ �̹����� �����Ѵ�.
            selectPortrait = userProfilPortraitSelector;
            selectPortrait.UserSelect();
        }

        // ������ ���� ��Ʈ����Ʈ �̹����� �����Ѵ�.
        public void ChangePortrait()
        {
            // ���� ������� �̹��� ��Ʈ����Ʈ�� ����Ѵ�.
            currentChoice.ChangeChoice();
            // ���� ��Ʈ����Ʈ �̹����� ���� ������ �̹��� ��Ʈ����Ʈ�� �����Ѵ�.
            GameManager.CurrentUser.UserPortrait = selectPortrait.GetCurrentSprite;
            // ���� ������ ������Ʈ�Ѵ�.
            GameManager.UIManager.ShowUserInfo();
            // ���� �����͸� �����Ѵ�.
            GameManager.Instance.SaveUser();
            // �˾�â�� ���ش�.
            this.gameObject.SetActive(false);
        }
    }

}