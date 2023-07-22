using Portfolio.Lobby.Hero;
using Portfolio.Lobby.Inventory;
using Portfolio.Lobby.Summon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Portfolio.Lobby.Shop;
using System;

/*
 * �κ� UI�� �����ϴ� UI �Ŵ��� Ŭ����
 */

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {

        [SerializeField] HeroPanelUI heroPanel;         // ����â UI
        [SerializeField] SummonPanel summonPanel;       // ��ȯâ UI
        [SerializeField] InventoryPanel inventoryPanel; // ����â UI
        [SerializeField] ShopPanel shopPanel;           // ����â UI

        [SerializeField] LobbyHeroView[] mainHeros;     // ���� ���� ���� 

        private void Awake()
        {
            undoStack = new Stack<IUndoable>();
        }

        private void Start()
        {
            // Undo�� �ʱ�ȭ�մϴ�.
            while (undoStack.Count >= 1)
            {
                Undo();
            }

            // ������ ���� ������ �����ݴϴ�.
            ShowMainUnits();

            // �����̳� ����� �����Ͱ� ����ȴٸ� �κ��� ���� ���ֵ� ����ǵ��� �̺�Ʈ ����
            unitChangedEvent += (sender, e) => { ShowMainUnits(); };
            equipmentItemDataChangeEvent += (sender, e) => { ShowMainUnits(); };
        }

        // ������ ���� ������ �����ݴϴ�.
        public void ShowMainUnits()
        {
            // ������ ���� ����Ʈ���� ������ ������ ������ ���� �� 5����� �����ͼ� �����ݴϴ�.
            var mainUnits = GameManager.CurrentUser.UserUnitList.OrderByDescending(GameLib.UnitBattlePowerSort).Take(5).ToList();
            for (int i = 0; i < mainHeros.Length; i++)
            {
                if (mainUnits.Count <= i)
                {
                    mainHeros[i].gameObject.SetActive(false);
                    continue;
                }
                mainHeros[i].gameObject.SetActive(true);
                mainHeros[i].ShowUnit(mainUnits[i]);
            }
        }

        //===========================================================
        // DataChangedEvent
        //===========================================================
        public event EventHandler unitChangedEvent;             // ������ �����Ͱ� ����� ���� �̺�Ʈ
        public event EventHandler equipmentItemDataChangeEvent; // �������� �����Ͱ� ����� ���� �̺�Ʈ

        // ������ ����� ���� �̺�Ʈ ȣ��
        public void OnUnitChanged() => unitChangedEvent?.Invoke(this, EventArgs.Empty);
        // �������� �����Ͱ� ����� ���� �̺�Ʈ ȣ��
        public void OnEquipmentItemChanged() => equipmentItemDataChangeEvent?.Invoke(this, EventArgs.Empty);


        // ������ â�� ĵ������ Ȱ��ȭ�մϴ�.
        public void BTN_OnClick_ShowCanvas(Canvas canvas)
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_Lobby_OpenPanel");
            canvas.gameObject.SetActive(true);
        }

        // ĵ������ ��Ȱ��ȭ �մϴ�.
        public void BTN_OnClick_HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        // ����ʾ����� �̵��մϴ�.
        public void BTN_OnClick_LoadWorldMapScene()
        {
            SceneLoader.LoadWorldMapScene();
        }

        //===========================================================
        // UndoSystem
        //===========================================================
        // ORDER : #14) Ŀ�ǵ� ������ �̿��� Undo �ý���
        // Ȱ��ȭ�� Undo�� ī��Ʈ
        private Stack<IUndoable> undoStack;             // Undo �ý����� ���� Undo �������̽� ����
        public int UndoCount()
        {
            return undoStack.Count;
        }

        // Undo�� �߰��մϴ�
        public void AddUndo(IUndoable undoInterface)
        {
            // ���ÿ� �߰��մϴ�.
            undoStack.Push(undoInterface);
        }

        // Undo�� �����մϴ�.
        public void Undo()
        {
            if (undoStack.Count < 1)
            {
                Debug.LogWarning("undoStack.Count < 1");
                return;
            }

            // ���ÿ��� ��Ҹ� ���� Undo�� �����մϴ�.
            undoStack.Pop().Undo();
        }
    }
}