using Portfolio.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �ռ� ���â�� ǥ�õ� ��� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero.Composition
{
    public class UnitGradeUpUI : MonoBehaviour
    {
        [SerializeField] Image activeStarImage;     // Ȱ��ȭ �� �̹���
        [SerializeField] Image unActiveStarImage;   // ��Ȱ��ȭ �� �̹���
        [SerializeField] Image glowImage;           // �۷ο� ȿ�� �̹���

        public bool IsActive
        { 
            set
            {
                // �� Ȱ��ȭ ����
                activeStarImage.gameObject.SetActive(value);
                unActiveStarImage.gameObject.SetActive(!value);
                glowImage.gameObject.SetActive(false);
            }
        }

        // ����Ʈ �ִϸ��̼��� �÷����Ѵ�.
        public void PlayEffect()
        {
            GetComponent<UIAnimationPlayer>().PlayAnim();
        }

        public void VibrateStar()
        {
            // SOUND : �� ���� ����
            //GameManager.AudioManager.PlaySound("");
        }

        public void SetStar()
        {
            // SOUND : �� �߰� ����
            //GameManager.AudioManager.PlaySound("");
        }
    } 
}