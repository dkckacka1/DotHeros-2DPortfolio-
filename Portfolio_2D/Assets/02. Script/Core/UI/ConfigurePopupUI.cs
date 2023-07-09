using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TODO : 환경설정을 표시하는 팝업 UI 클래스
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