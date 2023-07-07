using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * UI 애니메이션을 관리해주는 클래스
 */

namespace Portfolio.UI
{
    public class UIAnimationPlayer : MonoBehaviour
    {
        new Animation animation; // 오브젝트에 부착된 애니메이션 컴포넌트

        int currentAnimClipNum; // 현재 플레이중인 애니메이션 클립 넘버
        List<string> clipNames; // 애니메이션 컴포넌트가 가지고 있는 레거시 애니메이션 클립의 이름 리스트

        private void Awake()
        {
            // 초기 세팅
            animation = GetComponent<Animation>();
            clipNames = new List<string>();
            foreach(AnimationState state in animation)
                // 애니메이션에 들어가 있는 클립들의 이름을 저장한다.
            {
                clipNames.Add(state.name);
            }
        }

        // 첫번째 애니메이션을 플레이한다.
        public void PlayAnim()
        {
            currentAnimClipNum = 0;
            animation.Play();
        }

        // 다음 애니메이션을 플레이한다.
        public void AnimationEvent_PlayNextAnimation()
        {
            currentAnimClipNum++;
            // 다음애니메이션이 없다면 리턴
            if(currentAnimClipNum >= animation.GetClipCount())
            {
                return;
            }

            // 수행중이던 애니메이션을 멈추고 새롭게 애니메이션을 출력한다.
            animation.Play(clipNames[currentAnimClipNum], PlayMode.StopAll);
        }
    }
}