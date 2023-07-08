using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �κ񿡼� ��� �г��� �⺻ UI �߻� Ŭ����
 */

namespace Portfolio.Lobby
{
    public abstract class PanelUI : MonoBehaviour, IUndoable
    {
        // �� â�� ������ Undo ���ÿ� �־��ݴϴ�.
        protected virtual void OnEnable()
        {
            LobbyManager.UIManager.AddUndo(this);
        }

        // ��� �г��� Undo�� �ڽ��� ��Ȱ��ȭ�մϴ�.
        public void Undo()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        // Undo �մϴ�.
        public void BTN_OnClick_Undo()
        {
            LobbyManager.UIManager.Undo();
        }
    }

}