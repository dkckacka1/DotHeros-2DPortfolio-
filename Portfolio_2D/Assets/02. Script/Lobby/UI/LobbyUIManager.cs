using Portfolio.Lobby.Hero;
using Portfolio.Lobby.Inventory;
using Portfolio.Lobby.Summon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Portfolio.Lobby.Shop;
using System;

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {
        private Stack<UndoAble> undoStack;

        [SerializeField] HeroPanelUI heroPanel;
        [SerializeField] SummonPanel summonPanel;
        [SerializeField] InventoryPanel inventoryPanel;
        [SerializeField] ShopPanel shopPanel;
        [SerializeField] UserInfoUI userInfoUI;

        [SerializeField] LobbyHeroView[] mainHeros;

        private void Awake()
        {
            undoStack = new Stack<UndoAble>();
        }

        private void Start()
        {
            while (undoStack.Count >= 1)
            {
                Undo();
            }

            ShowMainUnits();
        }

        public void ShowMainUnits()
        {
            var mainUnits = GameManager.CurrentUser.userUnitList.OrderByDescending(unit => unit.UnitCurrentLevel).ThenByDescending(unit => unit.UnitGrade).Take(5).ToList();
            for (int i = 0; i < mainHeros.Length; i++)
            {
                if (mainUnits.Count
                    <= i)
                {
                    mainHeros[i].gameObject.SetActive(false);
                }
                mainHeros[i].gameObject.SetActive(true);
                mainHeros[i].ShowUnit(mainUnits[i]);
            }
        }

        public void ReShowPanel()
        {
            heroPanel.ReShow();
        }

        public void ShowCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        public void LoadWorldMapScene()
        {
            SceneLoader.LoadWorldMapScene();
        }

        public void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }
        //===========================================================
        // DataChangedEvent
        //===========================================================
        public event EventHandler unitChangedEvent;
        public event EventHandler equipmentItemDataChangeEvent;

        public void OnUnitChanged() => unitChangedEvent?.Invoke(this, EventArgs.Empty);
        public void OnEquipmentItemChanged() => equipmentItemDataChangeEvent?.Invoke(this, EventArgs.Empty);

        //===========================================================
        // UndoSystem
        //===========================================================
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