using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 유닛 합성 결과창에 표시될 등급 UI 클래스
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class UnitGradeUpUI : MonoBehaviour
    {
        [SerializeField] Image activeStarImage;     // 활성화 별 이미지
        [SerializeField] Image unActiveStarImage;   // 비활성화 별 이미지
        [SerializeField] Image glowImage;           // 글로우 효과 이미지

        public bool IsActive
        { 
            set
            {
                // 별 활성화 지정
                activeStarImage.gameObject.SetActive(value);
                unActiveStarImage.gameObject.SetActive(!value);
                glowImage.gameObject.SetActive(false);
            }
        }

        // 합성창이 꺼졌을 때 초기상태로 원복 시킵니다.
        private void OnDisable()
        {
            this.transform.Rotate(Vector3.zero);
            unActiveStarImage.rectTransform.anchoredPosition = Vector2.zero;
            glowImage.transform.Rotate(Vector3.zero);
            activeStarImage.rectTransform.localScale = Vector3.one;
        }

        // 이펙트 애니메이션을 플레이한다.
        public void PlayEffect()
        {
            GetComponent<UIAnimationPlayer>().PlayAnim();
        }

        public void VibrateStar()
        {
            // SOUND : 별 진동 사운드
            //GameManager.AudioManager.PlaySound("");
        }

        public void SetStar()
        {
            GameManager.AudioManager.PlaySound("Sound_SetStar");
        }
    } 
}