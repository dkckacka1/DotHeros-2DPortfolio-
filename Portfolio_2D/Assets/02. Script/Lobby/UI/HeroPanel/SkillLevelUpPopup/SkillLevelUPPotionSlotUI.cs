using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��ų �������Ҷ� ���� ���� ����UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUPPotionSlotUI : MonoBehaviour
    {
        [SerializeField] Image unSelectedImage; // ���� �̹���
        // ������ �����ش�. ���� �̹��� ����
        public void Add() => unSelectedImage.gameObject.SetActive(false);
        // ������ �����ش�. ���� �̹��� ���
        public void Minus() => unSelectedImage.gameObject.SetActive(true);
    }
}