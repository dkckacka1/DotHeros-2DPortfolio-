using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Portfolio.UI;

/*
 * 유닛 합성 패널에 사용되는 유닛 슬롯 UI 셀렉터 클래스
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class UnitSlotSelector_HeroComposition : MonoBehaviour
    {
        UnitSlotUI unitSlotUI;                         // 유닛 슬롯 UI
        Button button;                                 // 유닛 슬롯 버튼
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI; // 유닛 합성 패널 UI
        [SerializeField] RectTransform mainSelectImage;                 // 메인 선택 이미지
        [SerializeField] RectTransform subSelectImage;                  // 재료 선택 이미지
        [SerializeField] Image cantSelectImage;                         // 선택할 수 없음 이미지

        private bool canSelect = true;  // 선택할 수 있는지 여부
        private bool isMainSelect;      // 합성 메인 재료인지 여부
        private bool isSubSelect;       // 합성 서브 재료인지 여부

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
            button = GetComponent<Button>();
        }

        public bool IsSelected
        {
            // 일단 선택되었는지
            get => isMainSelect || isSubSelect;
        }

        // 현재 슬롯의 유닛 정보
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
                    // 메인 선택 재료라면
                {
                    // 서브 선택 재료 취소
                    IsSubSelect = false;
                }
                // 메인 선택 이미지 출력
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
                    // 서브 선택 재료라면
                {
                    // 메인 선택 재료 취소
                    IsMainSelect = false;
                }
                // 서브 선택 이미지 출력
                subSelectImage.gameObject.SetActive(isSubSelect);
            }
        }

        public bool CanSelect
        {
            get => canSelect;
            set
            {
                canSelect = value;
                // 사용 여부에 따라 이미지 출력
                cantSelectImage.gameObject.SetActive(!canSelect);
            }
        }

        // 이 컴포넌트가 활성화 되면 버튼 클릭에 유닛 선택 함수를 구독해준다.
        private void OnEnable()
        {
            button.onClick.AddListener(SelectUnit);
        }

        // 이 컴포넌트가 비활성화 되면 버튼 클릭에 유닛 선택 함수를 구독 해지 해준다.
        private void OnDisable()
        {
            button.onClick.RemoveListener(SelectUnit);
            // 선택되었다면 취소
            ResetSelect();
        }

        // 유닛을 선택한다.
        public void SelectUnit()
        {
            // 선택할 수 있는 상태라면
            if (canSelect)
            {
                // 합성 패널에 이 유닛을 넣어준다.
                unitCompositionPanelUI.InsertUnit(this);
            }
        }

        // 모든 선택을 취소한다.
        public void ResetSelect()
        {
            IsMainSelect = false;
            IsSubSelect = false;
        }
    }

}