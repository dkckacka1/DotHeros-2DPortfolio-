using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 영웅 합성창에서의 유닛 슬롯 UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class CompositionUnitSlot : MonoBehaviour
    {
        [SerializeField] RectTransform unitPortraitMask;                    // 기본 이미지
        [SerializeField] RectTransform lockMask;                            // 자물쇠 이미지
        [SerializeField] Image unitPortraitImage;                           // 유닛 포트레이트 이미지
        [SerializeField] TextMeshProUGUI unitNameText;                      // 유닛 이름
        [SerializeField] GridLayoutGroup gradeLayout;                       // 유닛 등급 레이웃
        [SerializeField] Sprite defaultSprite;                              // 빈칸일 때 표시할 스프라이트
        [SerializeField] Image selectImage;                                 // 현재 들어가야할 슬롯 표시할 이미지

        [HideInInspector] public UnitSlotHeroCompositionSelector selector;  // 현재 선택한 유닛 슬롯 UI 셀렉터
        private bool isSelect;                                              // 현재 선택 되었는지
        Unit unit;                                                          // 보여줄 유닛
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

        // 영웅 함성창 초기화
        public void Reset()
        {
            unit = null;
            // 이미 선택한 셀렉터가 있다면 초기화
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

        // 잠겨있는 슬롯 표시
        public void ShowLock()
        {
            unit = null;
            lockMask.gameObject.SetActive(true);
            unitPortraitMask.gameObject.SetActive(false);
            unitNameText.gameObject.SetActive(false);
            gradeLayout.gameObject.SetActive(false);
        }

        // 슬롯에 유닛이 들어온다면 표시
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

        // 등급 표시
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