using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConfirmationPopupUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertTitleText;
    [SerializeField] TextMeshProUGUI alertText;
    [SerializeField] Button confirmaitionBtn;
    [SerializeField] VerticalLayoutGroup layout;

    public void Show(string titleText, string alertText, UnityAction confirmEvent)
    {
        alertTitleText.text = titleText;
        this.alertText.text = alertText;
        confirmaitionBtn.onClick.RemoveAllListeners();
        confirmaitionBtn.onClick.AddListener(() => { confirmEvent.Invoke(); ReleasePopup(); });
        Debug.Log(confirmaitionBtn == null);
        Debug.Log(confirmEvent == null);


        transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);

        LayoutRebuilder.ForceRebuildLayoutImmediate((this.alertText.transform as RectTransform));
        LayoutRebuilder.ForceRebuildLayoutImmediate((layout.transform as RectTransform));
    }

    public void ReleasePopup()
    {
        gameObject.SetActive(false);
    }
}
