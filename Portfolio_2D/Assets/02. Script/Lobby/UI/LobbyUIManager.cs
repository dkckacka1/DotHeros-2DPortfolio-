using Portfolio.Lobby.Hero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        private Stack<UndoAble> undoStack = new Stack<UndoAble>();

        [SerializeField] HeroPanelUI heroPanel;
        [SerializeField] Canvas summonCanvas;

        [SerializeField] UserInfoUI userInfoUI;

        private void Start()
        {
            ShowUserResource();
            while (undoStack.Count >= 1)
            {
                Undo();
            }
        }

        public void ReShowPanel()
        {
            heroPanel.ReShow();
        }

        public void ShowUserResource()
        {
            userInfoUI.Init(GameManager.CurrentUser);
        }

        public void ShowCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        public void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        public int UndoCount()
        {
            return undoStack.Count;
        }

        public void AddUndo(UndoAble undoInterface)
        {
            //Debug.Log(undoInterface.GetType().Name + " : AddUndo");
            undoStack.Push(undoInterface);
        }

        public void Undo()
        {
            if (undoStack.Count < 1)
            {
                Debug.LogWarning("undoStack.Count < 1");
                return;
            }
            //Debug.Log(undoStack.Count + " : " + undoStack.Peek().GetType().Name + " Undo");
            undoStack.Pop().Undo();
        }
    }
}