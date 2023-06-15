using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class SkillLevelUPPotionSlotUI : MonoBehaviour
    {
        [SerializeField] Image unSelectedImage;

        public void Add() => unSelectedImage.gameObject.SetActive(false);
        public void Minus() => unSelectedImage.gameObject.SetActive(true);
    }
}