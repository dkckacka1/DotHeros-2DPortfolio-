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

        // Ÿ�̸� �ʿ�
        private void Update()
        {
            Debug.Log(Time.realtimeSinceStartup);
        }
    }
}