using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���â�� ǥ���ϴ� �˾� UI Ŭ����
 */

namespace Portfolio
{
    public class AlertPoupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI alertText;     // ��� ǥ���� �ؽ�Ʈ
        [SerializeField] VerticalLayoutGroup layout;    // �ؽ�Ʈ�� ǥ�õ� ���̾ƿ� �׷�

        // ���â�� ǥ���Ѵ�.
        public void Show(string alertText)
        {
            // ǥ���� ��� �ؽ�Ʈ�� �Է��Ѵ�.
            this.alertText.text = alertText;

            // ĵ������ Ȱ��ȭ �Ѵ�.
            transform.parent.gameObject.SetActive(true);
            // �ڽ��� Ȱ��ȭ �Ѵ�.
            gameObject.SetActive(true);

            // ������ �ؽ�Ʈ ũ�⿡ ���缭 �˾�â ũ�⸦ �����Ѵ�.
            LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
            LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
        }
    }
}