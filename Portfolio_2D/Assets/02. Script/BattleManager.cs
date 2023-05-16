using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Portfolio
{
    public class BattleManager : MonoBehaviour
    {

        [SerializeField] private BattleUI battleUI;

        [SerializeField] private BattleFactory battleFactory;
         
        private List<UnitTurnBase> unitList;

        private BattleState battleState = BattleState.SETTING;



        private Dictionary<BattleState, UnityEvent> StateEventHandlerDic = new Dictionary<BattleState, UnityEvent>();

        public static BattleManager Instance { get; private set; }
        public BattleUI BattleUI { get => battleUI; }
        public BattleFactory BattleFactory { get => battleFactory; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("BattleManager is already created");
                return;
            }

            Instance = this;
            unitList = new List<UnitTurnBase>();
        }

        private void Start()
        {
            battleState = BattleState.BATTLE;
        }

        private void Update()
        {
            switch (battleState)
            {
                case BattleState.BATTLE:
                    {
                    }
                    break;
            }
        }

        public void AddUnitinUnitList(UnitTurnBase unit) => unitList.Add(unit);
        public void RemoveUnitinUnitList(Unit unit)
        {
            unitList.Remove(unitList.Find((findunit) => findunit.unit == unit));
        }
        public void ClearUnitinUnitList()
        {
            unitList.Clear();
        }
        public UnitTurnBase FindUnitinUnitList(Unit unit) => unitList.Find((findunit) => findunit.unit == unit);
        public List<UnitTurnBase> GetUnitList() => unitList;


    }

}