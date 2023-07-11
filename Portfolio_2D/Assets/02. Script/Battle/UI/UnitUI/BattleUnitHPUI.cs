using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  ���� ������ ü�¹ٸ� ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleUnitHPUI : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // ü�¹� �����̴� UI
        [SerializeField] TextMeshProUGUI hpText;// ü�� �ؽ�Ʈ
        [SerializeField] Image damagedFillImage;// ü�� ���� �̹���
        [SerializeField] float damagedTime;

        bool isDamaged;
        private float damagedFillImageWidthSize;

        private void Awake()
        {
            // 
            damagedFillImageWidthSize = damagedFillImage.rectTransform.sizeDelta.x;
        }

        // ü�¹� ó�� ����
        public void SetHP(float maxHP)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = maxHP;
            hpText.text = $"( {maxHP} / {maxHP} )";
        }

        // ���� ü�� ����� 
        public void ChangeHP(float currentHP)
        {
            // ü���� ���� ����� ����
            bool isHeal = hpSlider.value < currentHP;

            hpSlider.value = currentHP;
            hpText.text = $"( {hpSlider.maxValue.ToString("N0")} / {currentHP.ToString("N0")} )";

            if (!isHeal)
                // ü���� ���� �Ǿ��ٸ�
            {
                if (hpSlider.value <= 0)
                    // ����ߴٸ�
                {
                    if (isDamaged)
                    // ü���� �������̶��
                    {
                        // ������ �������� �ڷ�ƾ�� ����ϰ� ���� �����մϴ�.
                        StopCoroutine(DamageHPChange(hpSlider));
                    }
                }
                else
                    // ������� �ʾҴٸ�
                {
                    if (isDamaged)
                    // ü���� �������̶��
                    {
                        // ������ �������� �ڷ�ƾ�� ����ϰ� ���� �����մϴ�.
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
        
        // ü���� ���ҵ� �� ���� ü�� �̹��� ��ȭ
        private IEnumerator DamageHPChange(Slider hpsSlider)
        {
            // ü�� ���� ����
            isDamaged = true;
            float changeWidthSize = 0f;
            // Ÿ�̸�
            float timer = 1f;
            // ���� �ǰ� ü�¹� ���� �̹��� ������
            float currentImageWidthSize = damagedFillImage.rectTransform.sizeDelta.x;
            // ���� ������ ü���� ���� �̹��� ������ ����ȭ
            float normalizeValue = hpSlider.value / hpSlider.maxValue;
            // ���� ������ ü�¹� ���� �̹��� ������
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


            // ü�� ���� ��
            isDamaged = false;
        }
    }

}