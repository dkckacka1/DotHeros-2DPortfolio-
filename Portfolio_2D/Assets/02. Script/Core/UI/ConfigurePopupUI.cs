using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 환경설정을 표시하는 팝업 UI 클래스
 * TODO
 */

namespace Portfolio
{
    public class ConfigurePopupUI : MonoBehaviour
    {
        public void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}