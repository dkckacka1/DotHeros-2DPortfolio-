using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class AlertPoupUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI alertText;
        [SerializeField] VerticalLayoutGroup layout;

        public void Show(string alertText)
        {
            this.alertText.text = alertText;

            transform.parent.gameObject.SetActive(true);
            gameObject.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
            LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
        }
    }
}