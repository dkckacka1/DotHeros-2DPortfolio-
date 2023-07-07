using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * UI �ִϸ��̼��� �������ִ� Ŭ����
 */

namespace Portfolio.UI
{
    public class UIAnimationPlayer : MonoBehaviour
    {
        new Animation animation; // ������Ʈ�� ������ �ִϸ��̼� ������Ʈ

        int currentAnimClipNum; // ���� �÷������� �ִϸ��̼� Ŭ�� �ѹ�
        List<string> clipNames; // �ִϸ��̼� ������Ʈ�� ������ �ִ� ���Ž� �ִϸ��̼� Ŭ���� �̸� ����Ʈ

        private void Awake()
        {
            // �ʱ� ����
            animation = GetComponent<Animation>();
            clipNames = new List<string>();
            foreach(AnimationState state in animation)
                // �ִϸ��̼ǿ� �� �ִ� Ŭ������ �̸��� �����Ѵ�.
            {
                clipNames.Add(state.name);
            }
        }

        // ù��° �ִϸ��̼��� �÷����Ѵ�.
        public void PlayAnim()
        {
            currentAnimClipNum = 0;
            animation.Play();
        }

        // ���� �ִϸ��̼��� �÷����Ѵ�.
        public void AnimationEvent_PlayNextAnimation()
        {
            currentAnimClipNum++;
            // �����ִϸ��̼��� ���ٸ� ����
            if(currentAnimClipNum >= animation.GetClipCount())
            {
                return;
            }

            // �������̴� �ִϸ��̼��� ���߰� ���Ӱ� �ִϸ��̼��� ����Ѵ�.
            animation.Play(clipNames[currentAnimClipNum], PlayMode.StopAll);
        }
    }
}