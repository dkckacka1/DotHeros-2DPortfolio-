using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 유닛 확률 팝업창에서 유닛 슬롯 당 유닛 확률을 보여주는 UI 클래스
 * 유닛 슬롯 UI 컴포넌트가 부착된 오브젝트에만 부착할 수 있다.
 */

namespace Portfolio.Lobby.Summon
{
    [RequireComponent(typeof(UnitSlotUI))]
    public class UnitProbaility : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unitNameText;      // 유닛의 이름을 보여주는 텍스트
        [SerializeField] TextMeshProUGUI probabilityText;   // 확률을 보여주는 텍스트

        UnitSlotUI unitSlotUI;

        private void Awake()
        {
            unitSlotUI = GetComponent<UnitSlotUI>();
        }

        // 확률을 보여줍니다.
        public void Show(float probability)
        {
            unitNameText.text = unitSlotUI.CurrentUnitData.unitName;
            // 소수점 2자리까지 표현합니다.
            probabilityText.text = $"{(probability * 100).ToString("0.00")} %";
        }
    }

}