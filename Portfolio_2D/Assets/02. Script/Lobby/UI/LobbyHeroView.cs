using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 로비 메인 화면에 보여질 유저의 메인 유닛
 */

namespace Portfolio.Lobby
{
    public class LobbyHeroView : MonoBehaviour
    {
        [SerializeField] Animator unitAnim;             // 유닛의 애니메이터
        [SerializeField] TextMeshProUGUI unitNameText;  // 유닛의 이름
        [SerializeField] TextMeshProUGUI unitLevelText; // 유닛의 레벨
        [SerializeField] Image[] gradeStarImages;       // 등급 별 이미지

        // 유닛 정보를 보여줍니다.
        public void ShowUnit(Unit unit)
        {
            // 유닛의 애니메이터를 변경합니다.
            unitAnim.runtimeAnimatorController = unit.animController;
            unitNameText.text = unit.UnitName;
            unitLevelText.text = $"LV {unit.UnitCurrentLevel}";
            for (int i = 0; i < gradeStarImages.Length; i++)
            {
                gradeStarImages[i].gameObject.SetActive(i < unit.UnitGrade);
            }
        }
    }
}