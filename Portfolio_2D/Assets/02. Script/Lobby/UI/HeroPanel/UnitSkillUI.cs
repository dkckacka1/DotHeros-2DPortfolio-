using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitSkillUI : MonoBehaviour
    {
        [SerializeField] Image skillImage;
        [SerializeField] TextMeshProUGUI skillTypeText;
        [SerializeField] TextMeshProUGUI skillLevelText;
        [SerializeField] TextMeshProUGUI skillNameText;
        [SerializeField] TextMeshProUGUI skillDescText;

        public void Init(Skill skill, int skillLevel)
        {
            if (skill == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            this.gameObject.SetActive(true);
            skillImage.sprite = skill.skillSprite;
            skillTypeText.text = (skill.GetData.skillType == SkillType.ActiveSkill) ? "��Ƽ�� ��ų" : "�нú� ��ų";
            skillLevelText.text = "���� " + skillLevel.ToString();
            skillNameText.text = skill.GetData.skillName;
            skillDescText.text = skill.GetData.skillDesc;
        }
    }
}