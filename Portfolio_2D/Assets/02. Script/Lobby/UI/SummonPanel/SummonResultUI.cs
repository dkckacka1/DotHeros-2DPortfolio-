using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��ȯ ���â�� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonResultUI : MonoBehaviour
    {
        [Header("UnitSummonDirectorUnit")]
        [SerializeField] GameObject resultUnitDirectorObj;  // ��ȯ ������ ����ϴ� ������Ʈ
        [SerializeField] TextMeshProUGUI unitNameText;      // ��ȯ ���� �� ������ ���� �̸�
        [SerializeField] Animator unitAnim;                 // ��ȯ ���� �� ������ ���� �ִϸ��̼�
        [SerializeField] Image[] gradeImages;               // ��ȯ ���� �� ������ ���� ��� �̹���
        [SerializeField] float summonDirectorTerm = .5f;    // ��ȯ ���� ������ ��
        [SerializeField] TextMeshProUGUI summonCountText;   // ��ȯ Ƚ�� �ؽ�Ʈ
        private bool isDirector;                            // ��ȯ ���� ������ ����
        private IEnumerator summonDirector;                 // ��ȯ ������ ����� ������

        [Header("ResultUnitSlotLayout")]
        [SerializeField] GameObject slotLayout;                // ��ȯ ���� ���̾ƿ� ������Ʈ
        [SerializeField] List<UnitSlotUI> resultUnitSlotList; // ��ȯ ���â�� ���� ���� ����Ʈ

        // â�� ������ �ʱ�ȭ�մϴ�.
        private void OnDisable()
        {
            if (isDirector)
            {
                StopCoroutine(summonDirector);
                isDirector = false;
            }
            resultUnitDirectorObj.SetActive(false);
        }

        // ��ȯ ���â�� ����ݴϴ�.
        public void ClearSummonResult()
        {
            // ���� ��ȯ �������̾��ٸ� �����ŵ�ϴ�.
            if (isDirector)
            {
                StopCoroutine(summonDirector);
                resultUnitDirectorObj.SetActive(false);
                isDirector = false;
            }

            // ��� ���� ������ ��Ȱ��ȭ �մϴ�.
            foreach (var unitSlot in resultUnitSlotList)
            {
                unitSlot.gameObject.SetActive(false);
            }
        }

        // ��ȯ ����� �����ݴϴ�.
        public void ShowSummonResult(List<Unit> summonList)
        {
            // ��ȯ ������ �����մϴ�.
            summonDirector = ShowSummonDirector(summonList);
            StartCoroutine(summonDirector);

            // ��ȯ Ƚ����ŭ ���� ������ Ȱ��ȭ �մϴ�.
            for (int i = 0; i < 10; i++)
            {
                if (summonList.Count <= i)
                {
                    resultUnitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // ��ȯ�� ������ ������ ǥ���մϴ�.
                resultUnitSlotList[i].ShowUnit(summonList[i], false, true);
                resultUnitSlotList[i].gameObject.SetActive(true);
            }

            slotLayout.SetActive(false);
        }

        //  ��ȯ ������ �����ݴϴ�.
        private IEnumerator ShowSummonDirector(List<Unit> summonList)
        {
            isDirector = true;
            resultUnitDirectorObj.SetActive(true);
            for (int i = 0; i < summonList.Count; i++)
            {
                //��ȯ ������ ���������� �����ݴϴ�.
                unitNameText.text = summonList[i].UnitName;
                unitAnim.runtimeAnimatorController = summonList[i].animController;
                for (int j = 0; j < 5; j++)
                {
                    gradeImages[j].gameObject.SetActive(summonList[i].UnitGrade > j);
                }

                // ��ȯ Ƚ���� �����ݴϴ�.
                summonCountText.text = $"{i + 1}ȸ ��ȯ";

                // ������ ������ �� ���� �Ӵϴ�.
                yield return new WaitForSeconds(summonDirectorTerm);
            }

            resultUnitDirectorObj.SetActive(false);
            slotLayout.SetActive(true);
            isDirector = false;
        }

        // ��ȯ ������ �ǳ� �ݴϴ�.
        public void BTN_OnClick_FastForwardSummon()
        {
            // ������ �����մϴ�.
            StopCoroutine(summonDirector);
            // ���� ����ڸ� �����ݴϴ�.
            resultUnitDirectorObj.SetActive(false);
            slotLayout.SetActive(true);
        }
    }
}
