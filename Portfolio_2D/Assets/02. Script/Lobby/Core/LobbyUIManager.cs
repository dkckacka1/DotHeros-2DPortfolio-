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
 * 로비 UI를 관리하는 UI 매니저 클래스
 */

namespace Portfolio.Lobby
{
    public class LobbyUIManager : MonoBehaviour
    {

        [SerializeField] HeroPanelUI heroPanel;         // 영웅창 UI
        [SerializeField] SummonPanel summonPanel;       // 소환창 UI
        [SerializeField] InventoryPanel inventoryPanel; // 가방창 UI
        [SerializeField] ShopPanel shopPanel;           // 상점창 UI

        [SerializeField] LobbyHeroView[] mainHeros;     // 유저 메인 영웅 

        private void Awake()
        {
            undoStack = new Stack<IUndoable>();
        }

        private void Start()
        {
            // Undo를 초기화합니다.
            while (undoStack.Count >= 1)
            {
                Undo();
            }

            // 유저의 메인 유닛을 보여줍니다.
            ShowMainUnits();

            // 유닛이나 장비의 데이터가 변경된다면 로비의 메인 유닛도 변경되도록 이벤트 구독
            unitChangedEvent += (sender, e) => { ShowMainUnits(); };
            equipmentItemDataChangeEvent += (sender, e) => { ShowMainUnits(); };
        }

        // 유저의 메인 유닛을 보여줍니다.
        public void ShowMainUnits()
        {
            // 유저의 유닛 리스트에서 전투력 내림차 순으로 정렬 후 5명까지 가져와서 보여줍니다.
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
        public event EventHandler unitChangedEvent;             // 유닛의 데이터가 변경될 때의 이벤트
        public event EventHandler equipmentItemDataChangeEvent; // 장비아이템 데이터가 변경될 때의 이벤트

        // 유닛이 변경될 때의 이벤트 호출
        public void OnUnitChanged() => unitChangedEvent?.Invoke(this, EventArgs.Empty);
        // 장비아이템 데이터가 변경될 때의 이벤트 호출
        public void OnEquipmentItemChanged() => equipmentItemDataChangeEvent?.Invoke(this, EventArgs.Empty);


        // 보여줄 창의 캔버스를 활성화합니다.
        public void BTN_OnClick_ShowCanvas(Canvas canvas)
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_Lobby_OpenPanel");
            canvas.gameObject.SetActive(true);
        }

        // 캔버스를 비활성화 합니다.
        public void BTN_OnClick_HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        // 월드맵씬으로 이동합니다.
        public void BTN_OnClick_LoadWorldMapScene()
        {
            SceneLoader.LoadWorldMapScene();
        }

        //===========================================================
        // UndoSystem
        //===========================================================
        // ORDER : #14) 커맨드 패턴을 이용한 Undo 시스템
        // 활성화된 Undo의 카운트
        private Stack<IUndoable> undoStack;             // Undo 시스템을 위한 Undo 인터페이스 스택
        public int UndoCount()
        {
            return undoStack.Count;
        }

        // Undo를 추가합니다
        public void AddUndo(IUndoable undoInterface)
        {
            // 스택에 추가합니다.
            undoStack.Push(undoInterface);
        }

        // Undo를 수행합니다.
        public void Undo()
        {
            if (undoStack.Count < 1)
            {
                Debug.LogWarning("undoStack.Count < 1");
                return;
            }

            // 스택에서 요소를 빼고 Undo를 수행합니다.
            undoStack.Pop().Undo();
        }
    }
}