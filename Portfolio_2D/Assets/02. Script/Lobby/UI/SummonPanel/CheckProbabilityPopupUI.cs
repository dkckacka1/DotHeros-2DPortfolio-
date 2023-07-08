using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * 유닛 뽑기 확률창을 보여주는 팝업 UI 클래스
 */ 


namespace Portfolio.Lobby.Summon
{
    public class CheckProbabilityPopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect unitScrollView;                 // 유닛 슬롯 UI 스크롤 뷰
        [SerializeField] TextMeshProUGUI probablilityText;          // 뽑기 확률을 보여주는 텍스트

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();   // 유닛 슬롯 UI 리스트
        List<UnitData> gradeOneUnitList = new List<UnitData>();     // 기본 1성 등급 유닛 리스트
        List<UnitData> gradeTwoUnitList = new List<UnitData>();     // 기본 2성 등급 유닛 리스트
        List<UnitData> gradethreeUnitList = new List<UnitData>();   // 기본 3성 등급 유닛 리스트

        // 팝업창을 보여준다.
        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowGradeOne();
        }

        // 슬롯 리스트와 유닛 리스트를 초기화합니다.
        public void Init()
        {
            foreach(var unitSlot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(unitSlot);
            }
            // 전체 유닛 데이터에서 각 등급에 맞는 데이터를 추출해온다.
            gradeOneUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 1).ToList();
            gradeTwoUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 2).ToList();
            gradethreeUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 3).ToList();
        }

        // 기본 1성 등급의 유닛을 보여줍니다.
        public void ShowGradeOne()
        {
            probablilityText.text = $"1성 유닛 획득 확률 : {Constant.normalSummonPercent * 100}%";

            // 각 1성 유닛당 나올 확률을 체크합니다.
            float probability = Constant.normalSummonPercent / gradeOneUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if(gradeOneUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // 유닛 데이터를 보여주고 확률을 보여줍니다.
                unitSlotUIList[i].ShowUnit(gradeOneUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitProbaility>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        public void ShowGradeTwo()
        {
            probablilityText.text = $"1성 유닛 획득 확률 : {Constant.rareSummonPercent * 100}%";

            float probability = Constant.rareSummonPercent / gradeTwoUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if (gradeTwoUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].ShowUnit(gradeTwoUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitProbaility>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        public void ShowGradeThree()
        {
            probablilityText.text = $"1성 유닛 획득 확률 : {Constant.uniqueSummonPercent * 100}%";

            float probability = Constant.uniqueSummonPercent / gradethreeUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if (gradethreeUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].ShowUnit(gradethreeUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitProbaility>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }
    }
}