using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class TimeChecker : MonoBehaviour
    {
        private void Awake()
        {
        }

        // 타이머 필요
        private void Update()
        {
            Debug.Log(Time.realtimeSinceStartup);
        }
    }
}