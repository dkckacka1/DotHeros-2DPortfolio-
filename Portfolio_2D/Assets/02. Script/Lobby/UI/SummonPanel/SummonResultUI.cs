using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 소환 결과창을 보여주는 UI 클래스
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonResultUI : MonoBehaviour
    {
        [Header("UnitSummonDirectorUnit")]
        [SerializeField] GameObject resultUnitDirectorObj;  // 소환 연출을 담당하는 오브젝트
        [SerializeField] TextMeshProUGUI unitNameText;      // 소환 연출 시 보여줄 유닛 이름
        [SerializeField] Animator unitAnim;                 // 소환 연출 시 보여줄 유닛 애니메이션
        [SerializeField] Image[] gradeImages;               // 소환 연출 시 보여줄 유닛 등급 이미지
        [SerializeField] float summonDirectorTerm = .5f;    // 소환 연출 사이의 텀
        [SerializeField] TextMeshProUGUI summonCountText;   // 소환 횟수 텍스트
        private bool isDirector;                            // 소환 연출 중인지 여부
        private IEnumerator summonDirector;                 // 소환 연출을 담당할 열거자

        [Header("ResultUnitSlotLayout")]
        [SerializeField] GameObject slotLayout;                // 소환 슬롯 레이아웃 오브젝트
        [SerializeField] List<UnitSlotUI> resultUnitSlotList; // 소환 결과창의 유닛 슬롯 리스트

        // 창이 꺼질때 초기화합니다.
        private void OnDisable()
        {
            if (isDirector)
            {
                StopCoroutine(summonDirector);
                isDirector = false;
            }
            resultUnitDirectorObj.SetActive(false);
        }

        // 소환 결과창을 비워줍니다.
        public void ClearSummonResult()
        {
            // 만약 소환 연출중이었다면 종료시킵니다.
            if (isDirector)
            {
                StopCoroutine(summonDirector);
                resultUnitDirectorObj.SetActive(false);
                isDirector = false;
            }

            // 모든 유닛 슬롯을 비활성화 합니다.
            foreach (var unitSlot in resultUnitSlotList)
            {
                unitSlot.gameObject.SetActive(false);
            }
        }

        // 소환 결과를 보여줍니다.
        public void ShowSummonResult(List<Unit> summonList)
        {
            // 소환 연출을 시작합니다.
            summonDirector = ShowSummonDirector(summonList);
            StartCoroutine(summonDirector);

            // 소환 횟수만큼 유닛 슬롯을 활성화 합니다.
            for (int i = 0; i < 10; i++)
            {
                if (summonList.Count <= i)
                {
                    resultUnitSlotList[i].gameObject.SetActive(false);
                    continue;
                }

                // 소환한 유닛의 정보를 표시합니다.
                resultUnitSlotList[i].ShowUnit(summonList[i], false, true);
                resultUnitSlotList[i].gameObject.SetActive(true);
            }

            slotLayout.SetActive(false);
        }

        //  소환 연출을 보여줍니다.
        private IEnumerator ShowSummonDirector(List<Unit> summonList)
        {
            isDirector = true;
            resultUnitDirectorObj.SetActive(true);
            for (int i = 0; i < summonList.Count; i++)
            {
                //소환 유닛을 순차적으로 보여줍니다.
                unitNameText.text = summonList[i].UnitName;
                unitAnim.runtimeAnimatorController = summonList[i].animController;
                for (int j = 0; j < 5; j++)
                {
                    gradeImages[j].gameObject.SetActive(summonList[i].UnitGrade > j);
                }

                // 소환 횟수를 보여줍니다.
                summonCountText.text = $"{i + 1}회 소환";

                // 유닛을 보여준 후 텀을 둡니다.
                yield return new WaitForSeconds(summonDirectorTerm);
            }

            resultUnitDirectorObj.SetActive(false);
            slotLayout.SetActive(true);
            isDirector = false;
        }

        // 소환 연출을 건너 뜁니다.
        public void BTN_OnClick_FastForwardSummon()
        {
            // 연출을 종료합니다.
            StopCoroutine(summonDirector);
            // 연출 담당자를 숨겨줍니다.
            resultUnitDirectorObj.SetActive(false);
            slotLayout.SetActive(true);
        }
    }
}
