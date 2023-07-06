using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �ռ�â������ ���� ���� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class CompositionUnitSlot : MonoBehaviour
    {
        [SerializeField] RectTransform unitPortraitMask;                    // �⺻ �̹���
        [SerializeField] RectTransform lockMask;                            // �ڹ��� �̹���
        [SerializeField] Image unitPortraitImage;                           // ���� ��Ʈ����Ʈ �̹���
        [SerializeField] TextMeshProUGUI unitNameText;                      // ���� �̸�
        [SerializeField] GridLayoutGroup gradeLayout;                       // ���� ��� ���̿�
        [SerializeField] Sprite defaultSprite;                              // ��ĭ�� �� ǥ���� ��������Ʈ
        [SerializeField] Image selectImage;                                 // ���� ������ ���� ǥ���� �̹���

        [HideInInspector] public UnitSlotHeroCompositionSelector selector;  // ���� ������ ���� ���� UI ������
        private bool isSelect;                                              // ���� ���� �Ǿ�����
        Unit unit;                                                          // ������ ����
                                                                            // 
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

        // ���� �Լ�â �ʱ�ȭ
        public void Reset()
        {
            unit = null;
            // �̹� ������ �����Ͱ� �ִٸ� �ʱ�ȭ
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

        // ����ִ� ���� ǥ��
        public void ShowLock()
        {
            unit = null;
            lockMask.gameObject.SetActive(true);
            unitPortraitMask.gameObject.SetActive(false);
            unitNameText.gameObject.SetActive(false);
            gradeLayout.gameObject.SetActive(false);
        }

        // ���Կ� ������ ���´ٸ� ǥ��
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

        // ��� ǥ��
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