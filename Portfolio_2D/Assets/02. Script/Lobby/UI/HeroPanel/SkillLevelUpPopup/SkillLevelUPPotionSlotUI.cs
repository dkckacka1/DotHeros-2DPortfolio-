using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 스킬 레벨업할때 사용될 포션 슬롯UI 클래스
 */

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUPPotionSlotUI : MonoBehaviour
    {
        [SerializeField] Image unSelectedImage; // 비선택 이미지
        // 포션을 더해준다. 비선택 이미지 해제
        public void Add() => unSelectedImage.gameObject.SetActive(false);
        // 포션을 더해준다. 비선택 이미지 출력
        public void Minus() => unSelectedImage.gameObject.SetActive(true);
    }
}