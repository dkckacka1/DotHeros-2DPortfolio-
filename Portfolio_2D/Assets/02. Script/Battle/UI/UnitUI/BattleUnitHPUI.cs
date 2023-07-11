using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  전투 유닛의 체력바를 표시하는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleUnitHPUI : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // 체력바 슬라이더 UI
        [SerializeField] TextMeshProUGUI hpText;// 체력 텍스트
        [SerializeField] Image damagedFillImage;// 체력 감소 이미지
        [SerializeField] float damagedTime;

        bool isDamaged;
        private float damagedFillImageWidthSize;

        private void Awake()
        {
            // 
            damagedFillImageWidthSize = damagedFillImage.rectTransform.sizeDelta.x;
        }

        // 체력바 처음 세팅
        public void SetHP(float maxHP)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = maxHP;
            hpText.text = $"( {maxHP} / {maxHP} )";
        }

        // 현재 체력 변경시 
        public void ChangeHP(float currentHP)
        {
            // 체력이 감소 됬는지 여부
            bool isHeal = hpSlider.value < currentHP;

            hpSlider.value = currentHP;
            hpText.text = $"( {hpSlider.maxValue.ToString("N0")} / {currentHP.ToString("N0")} )";

            if (!isHeal)
                // 체력이 감소 되었다면
            {
                if (hpSlider.value <= 0)
                    // 사망했다면
                {
                    if (isDamaged)
                    // 체력이 감소중이라면
                    {
                        // 기존에 감소중인 코루틴을 취소하고 새로 시작합니다.
                        StopCoroutine(DamageHPChange(hpSlider));
                    }
                }
                else
                    // 사망하지 않았다면
                {
                    if (isDamaged)
                    // 체력이 감소중이라면
                    {
                        // 기존에 감소중인 코루틴을 취소하고 새로 시작합니다.
                        StopCoroutine(DamageHPChange(hpSlider));
                    }

                    StartCoroutine(DamageHPChange(hpSlider));
                }
            }
            else
            {
                float normalizeValue = hpSlider.value / hpSlider.maxValue;
                float changeHPWidthSize = damagedFillImageWidthSize * normalizeValue;
                damagedFillImage.rectTransform.sizeDelta = new Vector2(changeHPWidthSize, damagedFillImage.rectTransform.sizeDelta.y);
            }

        }
        
        // 체력이 감소될 때 피해 체력 이미지 변화
        private IEnumerator DamageHPChange(Slider hpsSlider)
        {
            // 체력 감소 시작
            isDamaged = true;
            float changeWidthSize = 0f;
            // 타이머
            float timer = 1f;
            // 현재 피격 체력바 가로 이미지 사이즈
            float currentImageWidthSize = damagedFillImage.rectTransform.sizeDelta.x;
            // 최종 도달할 체려바 가로 이미지 사이즈 정규화
            float normalizeValue = hpSlider.value / hpSlider.maxValue;
            // 최종 도달할 체력바 가로 이미지 사이즈
            float changeHPWidthSize = damagedFillImageWidthSize * normalizeValue;
            while (true)
            {
                if (timer <= 0)
                {
                    break;
                }

                timer -= Time.deltaTime / damagedTime;
                changeWidthSize = Mathf.Lerp(changeHPWidthSize, currentImageWidthSize, timer);
                damagedFillImage.rectTransform.sizeDelta = new Vector2(changeWidthSize, damagedFillImage.rectTransform.sizeDelta.y);
                yield return null;
            }


            // 체력 감소 끝
            isDamaged = false;
        }
    }

}