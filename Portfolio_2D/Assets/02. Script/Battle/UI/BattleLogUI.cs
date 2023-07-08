using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ORDER : ť�� �̿��� �α� �ý���
/*
 * ���� �α׸� ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleLogUI : MonoBehaviour
    {

        [SerializeField] int logCount = 5;                          // �αװ� ǥ�õ� �ִ� ����
        [SerializeField] TextMeshProUGUI logText;                   // �α� �ؽ�Ʈ

        private Queue<string> logQueue = new Queue<string>();       // �α� ť

        private void Start()
        {
            // �α׸� �ʱ�ȭ �Ѵ�.
            ResetLog();
        }

        // �α׸� ������Ʈ �Ѵ�.
        private void UpdateLog()
        { 
            string logtxt = string.Empty;

            // �α� ť�� ��ȸ�ϸ� �α׸� �״´�.
            foreach (string log in logQueue)
            {
                logtxt += ("\n" + log);
            }

            // �α� ���
            logText.text = logtxt;
        }

        // �α׸� �����ش�.
        public void AddLog(string logText)
        {
            // �α׸� �α� ť�� �ִ´�.
            logQueue.Enqueue(logText);
            
            // ���� �α�ť�� �ִ� ī��Ʈ�� �Ѿ��
            if (logQueue.Count > logCount)
            {
                // ���� ������ �α׸� �����ش�.
                logQueue.Dequeue();
            }

            UpdateLog();
        }

        // �α��ؽ�Ʈ�� ť�� �ʱ�ȭ�Ѵ�.
        public void ResetLog()
        {
            logQueue.Clear();
            logText.text = "";
        }
    }
}