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
         
        private List<UnitBase> unitList;

        private BattleSTate battleState = BattleSTate.SETTING;

        [SerializeField] private Unit currentTurnUnit = null;

        [SerializeField] private float turnCount = 100f;
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
            unitList = new List<UnitBase>();
        }

        private void Start()
        {
            battleState = BattleSTate.BATTLE;
        }

        private void Update()
        {
            switch (battleState)
            {
                case BattleSTate.BATTLE:
                    {
                        foreach (UnitBase unitBase in unitList)
                        {
                            if (unitBase.GetCurrentTurnCount() >= turnCount)
                            {
                                currentTurnUnit = unitBase.unit;

                                if (currentTurnUnit is Unit_Enemy)
                                {
                                    Debug.Log("적 턴");
                                    battleState = BattleSTate.ENEMYTURN;
                                }
                                else if(currentTurnUnit is Unit_Playable)
                                {
                                    Debug.Log("아군 턴");
                                    battleState = BattleSTate.PLAYERTURN;
                                }

                            }

                            unitBase.AddUnitTurnCount(unitBase.unit.Speed * Time.deltaTime);
                            float normalizedXPos = unitBase.GetCurrentTurnCount() / turnCount;
                            BattleUI.SequenceUI.SetSequenceUnitUIXPosition(unitBase.unitSequenceUI, normalizedXPos);
                        }
                    }
                    break;
            }
        }

        public void AddUnitinUnitList(UnitBase unit) => unitList.Add(unit);
        public void RemoveUnitinUnitList(Unit unit)
        {
            currentTurnUnit = null;
            unitList.Remove(unitList.Find((findunit) => findunit.unit == unit));
        }
        public void ClearUnitinUnitList()
        {
            currentTurnUnit = null;
            unitList.Clear();
        }
        public UnitBase FindUnitinUnitList(Unit unit) => unitList.Find((findunit) => findunit.unit == unit);
        public List<UnitBase> GetUnitList() => unitList;

        public Unit GetCurrentTurnUnit() => currentTurnUnit;

        public void TurnEnd()
        {
            UnitBase unitBase = FindUnitinUnitList(currentTurnUnit);
            unitBase.ResetUnitTurnCount();
            currentTurnUnit = null;
            battleState = BattleSTate.BATTLE;
        }
    }

}