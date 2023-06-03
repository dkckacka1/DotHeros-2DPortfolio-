using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        private Stack<UndoAble> undoStack = new Stack<UndoAble>();

        [SerializeField] GameObject heroPanel;

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
            Debug.Log(undoInterface.GetType().Name + " : AddUndo");
            undoStack.Push(undoInterface);
        }

        public void Undo()
        {
            if (undoStack.Count < 1) return;

            Debug.Log(undoStack.Count + " : " + undoStack.Peek().GetType().Name + " Undo");
            undoStack.Pop().Undo();
        }
    }
}