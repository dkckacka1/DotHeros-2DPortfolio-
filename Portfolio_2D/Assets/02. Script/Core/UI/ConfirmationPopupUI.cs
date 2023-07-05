using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * 확인창을 표시하는 팝업 UI 클래스
 */

public class ConfirmationPopupUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertTitleText;    // 경고 타이틀 텍스트
    [SerializeField] TextMeshProUGUI alertText;         // 경고 텍스트
    [SerializeField] Button confirmaitionBtn;           // 확인 버튼
    [SerializeField] VerticalLayoutGroup layout;        // 텍스트가 표시될 레이아웃 그룹

    public void Show(string alertTitleText, string alertText, UnityAction confirmEvent)
    {
        // 타이틀 텍스트 입력
        this.alertTitleText.text = alertTitleText;
        // 경고 텍스트 입력
        this.alertText.text = alertText;
        // 확인 버튼에 있는 모든 구독을 해제 한다.
        confirmaitionBtn.onClick.RemoveAllListeners();
        // 확인버튼에 들어온 이벤트와 경고창 끄기롤 구독한다.
        confirmaitionBtn.onClick.AddListener(() => { confirmEvent.Invoke(); ReleasePopup(); });

        transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);

        // 보여줄 텍스트 크기에 맞춰서 팝업창 크기를 조절한다.
        LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
        LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
    }

    public void ReleasePopup()
    {
        gameObject.SetActive(false);
    }
}
