using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 경고창을 표시하는 팝업 UI 클래스
 */

namespace Portfolio
{
    public class AlertPoupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI alertText;     // 경고 표시할 텍스트
        [SerializeField] VerticalLayoutGroup layout;    // 텍스트가 표시될 레이아웃 그룹

        // 경고창을 표시한다.
        public void Show(string alertText)
        {
            // 표시할 경고 텍스트를 입력한다.
            this.alertText.text = alertText;

            // 캔버스를 활성화 한다.
            transform.parent.gameObject.SetActive(true);
            // 자신을 활성화 한다.
            gameObject.SetActive(true);

            // 보여줄 텍스트 크기에 맞춰서 팝업창 크기를 조절한다.
            LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
            LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
        }
    }
}