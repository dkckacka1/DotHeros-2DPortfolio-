using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

namespace Portfolio.Lobby.Summon
{
    public class CheckProbabilityPopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect unitScrollView;
        [SerializeField] TextMeshProUGUI probablilityText;

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();
        List<UnitData> gradeOneUnitList = new List<UnitData>();
        List<UnitData> gradeTwoUnitList = new List<UnitData>();
        List<UnitData> gradethreeUnitList = new List<UnitData>();

        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowGradeOne();
        }

        public void Init()
        {
            foreach(var unitSlot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(unitSlot);
            }
            gradeOneUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 1).ToList();
            gradeTwoUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 2).ToList();
            gradethreeUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 3).ToList();
        }

        public void ShowGradeOne()
        {
            probablilityText.text = $"1¼º À¯´Ö È¹µæ È®·ü : {Constant.normalSummonPercent * 100}%";

            float probability = Constant.normalSummonPercent / gradeOneUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if(gradeOneUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].Init(gradeOneUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitSlotProbabilityUI>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        public void ShowGradeTwo()
        {
            probablilityText.text = $"1¼º À¯´Ö È¹µæ È®·ü : {Constant.rareSummonPercent * 100}%";

            float probability = Constant.rareSummonPercent / gradeTwoUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if (gradeTwoUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].Init(gradeTwoUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitSlotProbabilityUI>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        public void ShowGradeThree()
        {
            probablilityText.text = $"1¼º À¯´Ö È¹µæ È®·ü : {Constant.uniqueSummonPercent * 100}%";

            float probability = Constant.uniqueSummonPercent / gradethreeUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if (gradethreeUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                unitSlotUIList[i].Init(gradethreeUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitSlotProbabilityUI>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }
    }
}