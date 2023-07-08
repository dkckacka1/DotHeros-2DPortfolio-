using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Portfolio.UI;
using System.Linq;

/*
 * ���� �̱� Ȯ��â�� �����ִ� �˾� UI Ŭ����
 */ 


namespace Portfolio.Lobby.Summon
{
    public class CheckProbabilityPopupUI : MonoBehaviour
    {
        [SerializeField] ScrollRect unitScrollView;                 // ���� ���� UI ��ũ�� ��
        [SerializeField] TextMeshProUGUI probablilityText;          // �̱� Ȯ���� �����ִ� �ؽ�Ʈ

        List<UnitSlotUI> unitSlotUIList = new List<UnitSlotUI>();   // ���� ���� UI ����Ʈ
        List<UnitData> gradeOneUnitList = new List<UnitData>();     // �⺻ 1�� ��� ���� ����Ʈ
        List<UnitData> gradeTwoUnitList = new List<UnitData>();     // �⺻ 2�� ��� ���� ����Ʈ
        List<UnitData> gradethreeUnitList = new List<UnitData>();   // �⺻ 3�� ��� ���� ����Ʈ

        // �˾�â�� �����ش�.
        public void Show()
        {
            this.gameObject.SetActive(true);
            ShowGradeOne();
        }

        // ���� ����Ʈ�� ���� ����Ʈ�� �ʱ�ȭ�մϴ�.
        public void Init()
        {
            foreach(var unitSlot in unitScrollView.content.GetComponentsInChildren<UnitSlotUI>())
            {
                unitSlotUIList.Add(unitSlot);
            }
            // ��ü ���� �����Ϳ��� �� ��޿� �´� �����͸� �����ؿ´�.
            gradeOneUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 1).ToList();
            gradeTwoUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 2).ToList();
            gradethreeUnitList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == 3).ToList();
        }

        // �⺻ 1�� ����� ������ �����ݴϴ�.
        public void ShowGradeOne()
        {
            probablilityText.text = $"1�� ���� ȹ�� Ȯ�� : {Constant.normalSummonPercent * 100}%";

            // �� 1�� ���ִ� ���� Ȯ���� üũ�մϴ�.
            float probability = Constant.normalSummonPercent / gradeOneUnitList.Count;

            for (int i = 0; i < unitSlotUIList.Count; i++)
            {
                if(gradeOneUnitList.Count <= i)
                {
                    unitSlotUIList[i].gameObject.SetActive(false);
                    continue;
                }

                // ���� �����͸� �����ְ� Ȯ���� �����ݴϴ�.
                unitSlotUIList[i].ShowUnit(gradeOneUnitList[i], false);
                unitSlotUIList[i].GetComponent<UnitProbaility>().Show(probability);
                unitSlotUIList[i].gameObject.SetActive(true);
            }
        }

        public void ShowGradeTwo()
        {
            probablilityText.text = $"1�� ���� ȹ�� Ȯ�� : {Constant.rareSummonPercent * 100}%";

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
            probablilityText.text = $"1�� ���� ȹ�� Ȯ�� : {Constant.uniqueSummonPercent * 100}%";

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