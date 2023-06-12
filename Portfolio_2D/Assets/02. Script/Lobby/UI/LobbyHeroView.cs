using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby
{
    public class LobbyHeroView : MonoBehaviour
    {
        [SerializeField] Animator unitAnim;
        [SerializeField] TextMeshProUGUI unitNameText;
        [SerializeField] TextMeshProUGUI unitLevelText;
        [SerializeField] Image[] gradeStarImages;

        public void ShowUnit(Unit unit)
        {
            unitAnim.runtimeAnimatorController = unit.animController;
            unitNameText.text = unit.Data.unitName;
            unitLevelText.text = $"LV {unit.UnitCurrentLevel}";
            for (int i = 0; i < gradeStarImages.Length; i++)
            {
                gradeStarImages[i].gameObject.SetActive(i < unit.UnitGrade);
            }
        }
    }
}