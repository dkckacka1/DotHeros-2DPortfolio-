using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 로비에서 모든 패널의 기본 UI 추상 클래스
 */

namespace Portfolio.Lobby
{
    public abstract class PanelUI : MonoBehaviour, IUndoable
    {
        // 이 창이 켜지면 Undo 스택에 넣어줍니다.
        protected virtual void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        // 모든 패널의 Undo는 자신을 비활성화합니다.
        public void Undo()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        // Undo 합니다.
        public void BTN_OnClick_Undo()
        {
            LobbyManager.UIManager.Undo();
        }
    }

}