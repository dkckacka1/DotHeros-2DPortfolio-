using Portfolio.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ����â�� �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField] Image unitPortraitImage;                   // ���� ��Ʈ����Ʈ �̹���
        [SerializeField] TextMeshProUGUI unitBattlePowerText;       // ���� ������ �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitNameText;              // ���� �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitGradeText;             // ������ ��� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitCurrentLevelText;      // ������ ���� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitMaxLevelText;          // ������ �ִ� ���� �ؽ�Ʈ
        [SerializeField] Slider unitExperienceSlider;               // ������ ����ġ �����̴���
        [SerializeField] TextMeshProUGUI unitExperienceText;        // ������ ����ġ�� �ؽ�Ʈ
        [SerializeField] RectTransform potionSlot;                  // ���� ���� ���̾ƿ� ������Ʈ

        [Header("Unit Status")]
        [SerializeField] TextMeshProUGUI unitAttackPointText;       // ������ ���ݷ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitHealthPointText;       // ������ ����� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitDefencePointText;      // ������ ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitSpeedText;             // ������ �ӵ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitCriticalPercentText;   // ������ ġ��Ÿ Ȯ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitCriticalDamageText;    // ������ ġ��Ÿ ���ݷ� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitEffectHitText;         // ������ ȿ�� ���߷� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI unitEffectResText;         // ������ ȿ�� ���׷� �ؽ�Ʈ
        
        [Header("ExperiencePotion")]
        [SerializeField] Toggle potionPanelToggle;                  // ���� ���� ���
        [SerializeField] ItemSlotUI[] experiencePotionSlots;        // ���� ������ ���Ե�

        private void OnEnable()
        {
            // ������ ������ ������ ����Ǹ� UI�� ������Ʈ �Ѵ�.
            LobbyManager.UIManager.unitChangedEvent += ShowStat;
        }


        private void OnDisable()
        {
            // â�� ������ ���� ������ �����ִٸ� ���ݴϴ�.
            potionPanelToggle.isOn = false;
            potionPanelToggle.onValueChanged?.Invoke(false);
            LobbyManager.UIManager.unitChangedEvent -= ShowStat;
        }

        // ������ ������ ������ ������ ǥ���Ѵ�.
        public void ShowStat(object sender, EventArgs eventArgs)
        {
            Unit unit = HeroPanelUI.SelectUnit;
            if (unit == null) return;

            unitBattlePowerText.text = unit.battlePower.ToString("###,###,###");
            unitPortraitImage.sprite = unit.portraitSprite;
            unitNameText.text = unit.UnitName;
            unitGradeText.text = unit.UnitGrade.ToString() + " ��";
            unitCurrentLevelText.text = unit.UnitCurrentLevel.ToString();
            unitMaxLevelText.text = "/ " + unit.UnitMaxLevel.ToString();
            unitExperienceSlider.value = unit.CurrentExperience / unit.MaxExperience;
            unitExperienceText.text = (unit.CurrentExperience / unit.MaxExperience * 100f).ToString("F1") + " %";

            unitAttackPointText.text = unit.AttackPoint.ToString("N0");
            unitHealthPointText.text = unit.HealthPoint.ToString("N0");
            unitDefencePointText.text = unit.DefencePoint.ToString("N0");
            unitSpeedText.text = unit.Speed.ToString("N0");
            // �Ҽ��� ù��° �ڸ����� ǥ���Ѵ�.
            unitCriticalPercentText.text = (unit.CriticalPercent * 100f).ToString("F1") + " %";
            unitCriticalDamageText.text = (unit.CriticalDamage * 100f).ToString("F1") + " %";
            unitEffectHitText.text = (unit.EffectHit * 100f).ToString("F1") + " %";
            unitEffectResText.text = (unit.EffectResistance * 100f).ToString("F1") + " %";

            for (int i = 0; i < experiencePotionSlots.Length; i++)
            {
                // ����ġ ������ ������Ʈ �մϴ�.
                experiencePotionSlots[i].GetComponent<ItemSlotSelector_ItemConsum>().ShowSlot();
            }
        }

        // ���� ������ ������Ʈ �մϴ�.
        public void TOGGLE_OnValueChanged_SetPotionSlot()
        {
            for (int i = 0; i < experiencePotionSlots.Length; i++)
            {
                // ����ġ ������ ������Ʈ �մϴ�.
                experiencePotionSlots[i].GetComponent<ItemSlotSelector_ItemConsum>().ShowSlot();
            }
        }
    }
}