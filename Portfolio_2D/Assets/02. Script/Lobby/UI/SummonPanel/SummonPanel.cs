using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * ��ȯâ�� �����ִ� �г� UI Ŭ����
 */

namespace Portfolio.Lobby.Summon
{
    public class SummonPanel : PanelUI
    {
        [SerializeField] SummonResultUI summonResultUI;                     // ��ȯ ���â UI
        [SerializeField] CheckProbabilityPopupUI checkProbabilityPopupUI;   // ��ȯ Ȯ�� �˾� UI


        public void Awake()
        {
            // �˾� UI �ʱ�ȭ
            checkProbabilityPopupUI.Init();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // ��ȯ ���â�� ���� �մϴ�.
            summonResultUI.ClearSummonResult();
        }

        // â�� ���� �� �ʱ�ȭ�մϴ�.
        private void OnDisable()
        {
            checkProbabilityPopupUI.gameObject.SetActive(false);
            summonResultUI.ClearSummonResult();
        }


        // ������ �Ѹ� ��ȯ�մϴ�.
        private Unit SummonUnit()
        {
            // ������ ������ ���� �̽��ϴ�.
            float randomValue = Random.Range(0, 1f);

            int unitGrade = 0;

            // ������ ���� ���� ���� ������ ����� �����մϴ�.
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


            // ���� ����Ʈ���� ������ ���� �� �ִ� �����̸�, ���� ����� ����Ʈ�� �����մϴ�.
            var dataList = GameManager.Instance.GetDatas<UnitData>().Where(data => data.isUserUnit && data.defaultGrade == unitGrade).ToList();
            if (dataList.Count == 0)
            {
                return null;
            }
            else
            {
                // ����Ʈ���� ������ ������ �ϳ��� �̾Ƽ� ���� ���� �����͸� ����ϴ�.
                var data = dataList[Random.Range(0, dataList.Count)];
                UserUnitData unitData = new UserUnitData(data);
                Unit summonUnit = new Unit(data, unitData);
                return summonUnit;
            }
        }

        // 1ȸ ��ȯ�� �����մϴ�.
        public void BTN_OnClick_SummonUnitBtn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.Summon_1_unitConsumeDiaValue))
                // ���� ���̾ƾ��� �����ϴٸ� ���â �˾�
            {
                GameManager.UIManager.ShowAlert("���̾Ƹ�尡 �����մϴ�!");
            }
            else if (GameManager.CurrentUser.UserUnitList.Count + 1 > GameManager.CurrentUser.MaxUnitListCount)
                // ���� ���� ����Ʈ�� ���� ������ ���ٸ� ���â �˾�
            {
                GameManager.UIManager.ShowAlert("�ִ� ���� ������ �ʰ����ϴ�!");
            }
            else
            // ��� ������ ���ٸ�
            {
                // ���� ���̾� ���� ���ҽ�ŵ�ϴ�.
                GameManager.CurrentUser.Diamond -= Constant.Summon_1_unitConsumeDiaValue;
                // ���� ���â�� �����ݴϴ�.
                summonResultUI.ClearSummonResult();

                // ���� �ϳ��� ��ȯ�մϴ�.
                Unit newUnit = SummonUnit();
                // �ش� ������ �������� �ݴϴ�.
                GameManager.CurrentUser.AddNewUnit(newUnit);

                // ��ȯ ���â�� ���� ������ �����ݴϴ�.
                summonResultUI.ShowSummonResult(new List<Unit>() { newUnit });

                // ���� ������ ������Ʈ �մϴ�.
                LobbyManager.UIManager.ShowMainUnits();
            }
        }

        // 10 ȸ ��ȯ�� �����մϴ�.
        public void BTN_OnClick_SummonUnit_10_Btn()
        {
            if (!GameManager.CurrentUser.CanDIamondConsume(Constant.Summon_10_unitConsumeDiaValue))
            {
                GameManager.UIManager.ShowAlert("���̾Ƹ�尡 �����մϴ�!");
            }
            else if (GameManager.CurrentUser.UserUnitList.Count + 10 > GameManager.CurrentUser.MaxUnitListCount)
            {
                GameManager.UIManager.ShowAlert("�ִ� ���� ������ �ʰ����ϴ�!");
            }
            else
            {
                GameManager.CurrentUser.Diamond -= Constant.Summon_10_unitConsumeDiaValue;
                summonResultUI.ClearSummonResult();

                List<Unit> summonList = new List<Unit>();
                // ��ȯ�� 10ȸ �����մϴ�.
                for (int i = 0; i < 10; i++)
                {
                    summonList.Add(SummonUnit());
                }
                GameManager.CurrentUser.AddNewUnit(summonList);

                summonResultUI.ShowSummonResult(summonList);

                // ���� ������ ������Ʈ �մϴ�.
                LobbyManager.UIManager.ShowMainUnits();
            }
        }

        // Ȯ�� �˾�â�� �����ݴϴ�.
        public void BTN_OnClick_ShowCheckProbabilityPopup()
        {
            checkProbabilityPopupUI.Show();
        }
    }
}