using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Portfolio
{
    public class BattleLogUI : MonoBehaviour
    {

        [SerializeField] int logCount = 5;
        [SerializeField] TextMeshProUGUI logText;
        
        private Queue<string> logQueue = new Queue<string>();

        private void Start()
        {
            ResetLog();
        }

        private void UpdateLog()
        {
            string logtxt = string.Empty;

            foreach (string log in logQueue)
            {
                //Debug.Log($"{logtxt} + {log}");
                logtxt += ("\n" + log);
            }
            logText.text = logtxt;
        }

        public void AddLog(string str)
        {
            logQueue.Enqueue(str);
            //Debug.Log(logQueue.Count);
            if (logQueue.Count > logCount)
            {
                logQueue.Dequeue();
            }

            UpdateLog();
        }

        public void ResetLog()
        {
            logQueue.Clear();
            logText.text = "";
        }
    }
}