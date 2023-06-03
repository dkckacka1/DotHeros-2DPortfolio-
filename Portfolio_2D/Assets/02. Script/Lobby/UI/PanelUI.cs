using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public abstract class PanelUI : MonoBehaviour, UndoAble
    {
        protected virtual void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        public void Undo()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        public void UndoBtn()
        {
            LobbyManager.UIManager.Undo();
        }
    }

}