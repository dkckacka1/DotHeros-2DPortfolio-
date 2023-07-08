using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ORDER : 큐를 이용한 로그 시스템
/*
 * 전투 로그를 표시하는 UI 클래스
 */

namespace Portfolio.Battle
{
    public class BattleLogUI : MonoBehaviour
    {

        [SerializeField] int logCount = 5;                          // 로그가 표시될 최대 갯수
        [SerializeField] TextMeshProUGUI logText;                   // 로그 텍스트

        private Queue<string> logQueue = new Queue<string>();       // 로그 큐

        private void Start()
        {
            // 로그를 초기화 한다.
            ResetLog();
        }

        // 로그를 업데이트 한다.
        private void UpdateLog()
        { 
            string logtxt = string.Empty;

            // 로그 큐를 순회하며 로그를 쌓는다.
            foreach (string log in logQueue)
            {
                logtxt += ("\n" + log);
            }

            // 로그 출력
            logText.text = logtxt;
        }

        // 로그를 더해준다.
        public void AddLog(string logText)
        {
            // 로그를 로그 큐에 넣는다.
            logQueue.Enqueue(logText);
            
            // 만약 로그큐가 최대 카운트를 넘어가면
            if (logQueue.Count > logCount)
            {
                // 가장 오래된 로그를 없애준다.
                logQueue.Dequeue();
            }

            UpdateLog();
        }

        // 로그텍스트와 큐를 초기화한다.
        public void ResetLog()
        {
            logQueue.Clear();
            logText.text = "";
        }
    }
}