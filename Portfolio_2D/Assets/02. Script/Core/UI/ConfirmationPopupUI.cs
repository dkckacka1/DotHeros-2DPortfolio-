using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * Ȯ��â�� ǥ���ϴ� �˾� UI Ŭ����
 */

public class ConfirmationPopupUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertTitleText;    // ��� Ÿ��Ʋ �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI alertText;         // ��� �ؽ�Ʈ
    [SerializeField] Button confirmaitionBtn;           // Ȯ�� ��ư
    [SerializeField] VerticalLayoutGroup layout;        // �ؽ�Ʈ�� ǥ�õ� ���̾ƿ� �׷�

    public void Show(string alertTitleText, string alertText, UnityAction confirmEvent)
    {
        // Ÿ��Ʋ �ؽ�Ʈ �Է�
        this.alertTitleText.text = alertTitleText;
        // ��� �ؽ�Ʈ �Է�
        this.alertText.text = alertText;
        // Ȯ�� ��ư�� �ִ� ��� ������ ���� �Ѵ�.
        confirmaitionBtn.onClick.RemoveAllListeners();
        // Ȯ�ι�ư�� ���� �̺�Ʈ�� ���â ����� �����Ѵ�.
        confirmaitionBtn.onClick.AddListener(() => { confirmEvent.Invoke(); ReleasePopup(); });

        transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);

        // ������ �ؽ�Ʈ ũ�⿡ ���缭 �˾�â ũ�⸦ �����Ѵ�.
        LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
        LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
    }

    public void ReleasePopup()
    {
        gameObject.SetActive(false);
    }
}
