using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Portfolio.Lobby.Summon
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
                var data = dataList[Random.Range(0, dataList.Count)];
                UserUnitData unitData = new UserUnitData(data);
                Unit summonUnit = new Unit(data, unitData);
                return summonUnit;
            }
        }

        public void SummonUnitBtn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.summon_1_unitConsumeDiaValue))
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
            else if (GameManager.CurrentUser.userUnitList.Count + 1 > GameManager.CurrentUser.MaxUnitListCount)
            {
                GameManager.UIManager.ShowAlert("최대 유닛 개수를 초과힙니다!");
            }
            else
            {
                GameManager.CurrentUser.Diamond -= Constant.summon_1_unitConsumeDiaValue;
                summonResultUI.ClearSummonResult();

                Unit newUnit = SummonUnit();
                GameManager.CurrentUser.AddNewUnit(newUnit);

                summonResultUI.ShowSummonResult(new List<Unit>() { newUnit });
            }
        }

        public void SummonUnit_10_Btn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.summon_10_unitConsumeDiaValue))
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
            else if (GameManager.CurrentUser.userUnitList.Count + 10 > GameManager.CurrentUser.MaxUnitListCount)
            {
                GameManager.UIManager.ShowAlert("최대 유닛 개수를 초과힙니다!");
            }
            else
            {
                GameManager.CurrentUser.Diamond -= Constant.summon_10_unitConsumeDiaValue;
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
}