using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * 소환창을 보여주는 패널 UI 클래스
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonPanel : PanelUI
    {
        [SerializeField] SummonResultUI summonResultUI;                     // 소환 결과창 UI
        [SerializeField] CheckProbabilityPopupUI checkProbabilityPopupUI;   // 소환 확률 팝업 UI


        public void Awake()
        {
            // 팝업 UI 초기화
            checkProbabilityPopupUI.Init();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // 소환 결과창을 리셋 합니다.
            summonResultUI.ClearSummonResult();
        }

        // 창이 꺼질 때 초기화합니다.
        private void OnDisable()
        {
            checkProbabilityPopupUI.gameObject.SetActive(false);
            summonResultUI.ClearSummonResult();
        }


        // 유닛을 한명 소환합니다.
        private Unit SummonUnit()
        {
            // 유저가 랜덤한 값을 뽑습니다.
            float randomValue = Random.Range(0, 1f);

            int unitGrade = 0;

            // 랜덤한 값에 따라 나올 유닛의 등급을 결정합니다.
            if (randomValue < Constant.UniqueUnitSummonPercent)
            {
                unitGrade = 3;
            }
            else if (randomValue < Constant.RareUnitSummonPercent)
            {
                unitGrade = 2;
            }
            else
            {
                unitGrade = 1;
            }


            // 유닛 리스트에서 유저가 뽑을 수 있는 유닛이며, 같은 등급의 리스트를 추출합니다.
            var dataList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == unitGrade).ToList();
            if (dataList.Count == 0)
            {
                return null;
            }
            else
            {
                // 리스트에서 랜덤한 데이터 하나를 뽑아서 유저 유닛 데이터를 만듭니다.
                var data = dataList[Random.Range(0, dataList.Count)];
                UserUnitData unitData = new UserUnitData(data);
                Unit summonUnit = new Unit(data, unitData);
                return summonUnit;
            }
        }

        // 1회 소환을 진행합니다.
        public void BTN_OnClick_SummonUnitBtn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.Summon_1_unitConsumeDiaValue))
                // 소지 다이아양이 부족하다면 경고창 팝업
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
            else if (GameManager.CurrentUser.UserUnitList.Count + 1 > GameManager.CurrentUser.MaxUnitListCount)
                // 유저 유닛 리스트에 넣을 공간이 없다면 경고창 팝업
            {
                GameManager.UIManager.ShowAlert("최대 유닛 개수를 초과힙니다!");
            }
            else
            // 결격 사유가 없다면
            {
                // 소지 다이아 양을 감소시킵니다.
                GameManager.CurrentUser.Diamond -= Constant.Summon_1_unitConsumeDiaValue;
                // 기존 결과창을 지워줍니다.
                summonResultUI.ClearSummonResult();

                // 유닛 하나를 소환합니다.
                Unit newUnit = SummonUnit();
                // 해당 유닛을 유저에게 줍니다.
                GameManager.CurrentUser.AddNewUnit(newUnit);

                // 소환 결과창에 뽑은 유닛을 보여줍니다.
                summonResultUI.ShowSummonResult(new List<Unit>() { newUnit });

                // 메인 유닛을 업데이트 합니다.
                LobbyManager.UIManager.ShowMainUnits();
            }
        }

        // 10 회 소환을 진행합니다.
        public void BTN_OnClick_SummonUnit_10_Btn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.Summon_10_unitConsumeDiaValue))
            {
                GameManager.UIManager.ShowAlert("다이아몬드가 부족합니다!");
            }
            else if (GameManager.CurrentUser.UserUnitList.Count + 10 > GameManager.CurrentUser.MaxUnitListCount)
            {
                GameManager.UIManager.ShowAlert("최대 유닛 개수를 초과힙니다!");
            }
            else
            {
                GameManager.CurrentUser.Diamond -= Constant.Summon_10_unitConsumeDiaValue;
                summonResultUI.ClearSummonResult();

                List<Unit> summonList = new List<Unit>();
                // 소환을 10회 진행합니다.
                for (int i = 0; i < 10; i++)
                {
                    summonList.Add(SummonUnit());
                }
                GameManager.CurrentUser.AddNewUnit(summonList);

                summonResultUI.ShowSummonResult(summonList);

                // 메인 유닛을 업데이트 합니다.
                LobbyManager.UIManager.ShowMainUnits();
            }
        }

        // 확률 팝업창을 보여줍니다.
        public void BTN_OnClick_ShowCheckProbabilityPopup()
        {
            checkProbabilityPopupUI.Show();
        }
    }
}