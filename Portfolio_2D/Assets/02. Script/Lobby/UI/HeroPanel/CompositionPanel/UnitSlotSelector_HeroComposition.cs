using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Portfolio.UI;

/*
 * ���� �ռ� �гο� ���Ǵ� ���� ���� UI ������ Ŭ����
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class UnitSlotSelector_HeroComposition : MonoBehaviour
    {
        UnitSlotUI unitSlotUI;                         // ���� ���� UI
        Button button;                                 // ���� ���� ��ư
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI; // ���� �ռ� �г� UI
        [SerializeField] RectTransform mainSelectImage;                 // ���� ���� �̹���
        [SerializeField] RectTransform subSelectImage;                  // ��� ���� �̹���
        [SerializeField] Image cantSelectImage;                         // ������ �� ���� �̹���

        private bool canSelect = true;  // ������ �� �ִ��� ����
        private bool isMainSelect;      // �ռ� ���� ������� ����
        private bool isSubSelect;       // �ռ� ���� ������� ����
        private bool isActive;          // ���� ������ Ȱ��ȭ ����

        public bool IsActive 
        {
            set 
            {
                isActive = value;
                CanSelect = true;
                ResetSelect();
            }
        }


        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
            button = GetComponent<Button>();
        }

        public bool IsSelected
        {
            // �ϴ� ���õǾ�����
            get => isMainSelect || isSubSelect;
        }

        // ���� ������ ���� ����
        public Unit CurrentUnit
        {
            get => unitSlotUI.CurrentUnit;
        }

        public bool IsMainSelect
        {
            get => isMainSelect;
            set 
            { 
                isMainSelect = value; 
                if (IsMainSelect == true)
                    // ���� ���� �����
                {
                    // ���� ���� ��� ���
                    IsSubSelect = false;
                }
                // ���� ���� �̹��� ���
                mainSelectImage.gameObject.SetActive(isMainSelect);
            }
        }
        public bool IsSubSelect 
        { 
            get => isSubSelect;
            set
            {
                isSubSelect = value;
                if (isSubSelect == true)
                    // ���� ���� �����
                {
                    // ���� ���� ��� ���
                    IsMainSelect = false;
                }
                // ���� ���� �̹��� ���
                subSelectImage.gameObject.SetActive(isSubSelect);
            }
        }

        public bool CanSelect
        {
            get => canSelect;
            set
            {
                canSelect = value;
                // ��� ���ο� ���� �̹��� ���
                cantSelectImage.gameObject.SetActive(!canSelect);
            }
        }

        // ������ �����Ѵ�.
        public void BTN_OnClick_SelectUnit()
        {
            // ���� Ȱ��ȭ ���°� ������ �� �ִ� ���¶��
            if (isActive && canSelect)
            {
                // �ռ� �гο� �� ������ �־��ش�.
                unitCompositionPanelUI.InsertUnit(this);
            }
        }

        // ��� ������ ����Ѵ�.
        public void ResetSelect()
        {
            IsMainSelect = false;
            IsSubSelect = false;
        }
    }

}