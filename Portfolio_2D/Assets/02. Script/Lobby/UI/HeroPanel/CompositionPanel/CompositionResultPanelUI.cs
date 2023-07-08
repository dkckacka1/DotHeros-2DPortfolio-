using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 합성 결과를 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class CompositionResultPanelUI : MonoBehaviour, IUndoable
    {
        [SerializeField] UnitSlotUI unitSlotUI;                         // 유닛 슬롯
        [SerializeField] List<UnitGradeUpUI> unitGradeUIList;           // 유닛 등급 이미지들
        [SerializeField] TextMeshProUGUI unitNameText; // 유닛 이름 텍스트

        // 결과를 보여준다.
        public void Show(Unit unit)
        {
            this.gameObject.SetActive(true);

            // 유닛 슬롯과 이름을 설정한다.
            unitSlotUI.ShowUnit(unit, false, false);
            unitNameText.text = unit.UnitName;

            // 유닛 등급에 맞게 별 활성화
            for (int i = 0; i < 5; i++)
            {
                unitGradeUIList[i].IsActive = unit.UnitGrade >= (i + 1);
            }

            // 마지막 등급은 이펙트 출력
            unitGradeUIList[unit.UnitGrade - 1].PlayEffect();

            LobbyManager.UIManager.AddUndo(this);
        }

        // 결과창을 끌 수 있도록 Undo 인터페이스 구현
        public void Undo()
        {
            this.gameObject.SetActive(false);
        }
    }
}