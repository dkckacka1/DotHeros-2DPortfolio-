using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Lobby
{
    public class SummonPanel : PanelUI
    {
        [SerializeField] SummonResultUI summonResultUI;

        protected override void OnEnable()
        {
            base.OnEnable();
            summonResultUI.ClearSummonResult();
        }

        private Unit SummonUnit()
        {
            float randomValue = Random.Range(0, 1f);

            int unitGrade = 0;

            if (randomValue < Constant.uniqueSummonPercent)
            {
                Debug.Log(randomValue);
                unitGrade = 3;
            }
            else if (randomValue < Constant.rareSummonPercent)
            {
                unitGrade = 2;
            }
            else
            {
                unitGrade = 1;
            }

            var dataList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.defaultGrade == unitGrade).ToList();
            if (dataList.Count == 0)
            {
                return null;
            }
            else
            {
                Unit summonUnit = new Unit(dataList[Random.Range(0, dataList.Count)]);
                return summonUnit;
            }
        }

        public void SummonUnitBtn()
        {
            summonResultUI.ClearSummonResult();

            Unit newUnit = SummonUnit();
            GameManager.CurrentUser.AddNewUnit(newUnit);

            summonResultUI.ShowSummonResult(new List<Unit>() { newUnit });
        }

        public void SummonUnit_10_Btn()
        {
            summonResultUI.ClearSummonResult();

            List<Unit> summonList = new List<Unit>();
            for (int i = 0; i < 10; i++)
            {
                summonList.Add(SummonUnit());
            }
            GameManager.CurrentUser.AddNewUnit(summonList);

            summonResultUI.ShowSummonResult(summonList);
        }
    }
}