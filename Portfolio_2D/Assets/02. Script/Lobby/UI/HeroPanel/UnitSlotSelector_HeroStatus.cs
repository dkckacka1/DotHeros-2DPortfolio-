using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 유닛 상태창과 연동되는 유닛 슬롯 UI 셀렉터 클래스
 * UnitSlotUI 컴포넌트를 부착한 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.Lobby.Hero
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitSlotSelector_HeroStatus : MonoBehaviour
    {
        UnitSlotUI unitSlotUI;
        Button button;

        private bool isActive = true;

        public bool IsActive { get => isActive; set => isActive = value; }

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
            button = GetComponent<Button>();
        }

        // 슬롯을 클릭했을때 유저가 선택한 유닛을 이유닛으로 변경합니다.
        public void BTN_OnClick_UnitStatusSelect()
        {
            if(IsActive)
            {
                HeroPanelUI.SelectUnit = unitSlotUI.CurrentUnit;
            }
        }
    }
}