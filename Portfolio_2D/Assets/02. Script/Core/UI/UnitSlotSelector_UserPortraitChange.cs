using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��Ʈ����Ʈ �̹����� �����ϴ� ������
 * ���� ���� UI���ִ� ������Ʈ���� ������ �� �ִ�.
 */

namespace Portfolio.UI
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_UserPortraitChange : MonoBehaviour
    {
        [SerializeField] RectTransform currentChoice;   // ���� ��������� ǥ���ϴ� ������Ʈ
        [SerializeField] RectTransform userSelect;      // ������ �������� ǥ���ϴ� ������Ʈ

        public Sprite GetCurrentSprite => GetComponent<UnitSlotUI>().CurrentUnit.portraitSprite;
        public bool IsCurrentChoice => currentChoice.gameObject.activeInHierarchy;
        public bool IsUserSelect => userSelect.gameObject.activeInHierarchy;

        // ���� ������� ǥ���Ѵ�.
        public void CurrentChoice()
        {
            currentChoice.gameObject.SetActive(true);
        }

        // ����� ����Ѵ�.
        public void ChangeChoice()
        {
            currentChoice.gameObject.SetActive(false);
        }

        // ������ ������ �̹����� ǥ���Ѵ�.
        public void UserSelect()
        {
            userSelect.gameObject.SetActive(true);
        }

        // ������ ������ ����Ѵ�.
        public void UnSelect()
        {
            userSelect.gameObject.SetActive(false);
        }

        // ������ �̹����� �����Ѵ�.
        public void BTN_OnClick_Select(UserPortraitChangePopupUI popupUI)
        {
            if (IsCurrentChoice) return; // �̹� ������ �Ŷ�� ����
            if (IsUserSelect) return; // �̹� ������ �Ŷ�� ����

            // ������ �����ѰŸ� UserPortraitChangePopupUI�� �˷��ش�.
            popupUI.SelectPortrait(this);
            UserSelect();
        }
    }
}