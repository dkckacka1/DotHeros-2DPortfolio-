using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ð��� ���� �ý��� Ŭ����
 */

namespace Portfolio
{
    public class TimeChecker : MonoBehaviour
    {
        public int energyChargeCount = (int)Constant.energyChargeTime; // ������ ȸ�� �ð�

        public void CheckEnergy()
        {
            // ������ ȸ�� üũ�� �����Ѵ�.
            StartCoroutine(EnergyCheckCoroutine());
        }

        // ������ ȸ�� �ڷ�ƾ
        private IEnumerator EnergyCheckCoroutine()
        {
            while (true)
            {
                // 1�ʴ� �ѹ��� üũ��.
                yield return new WaitForSecondsRealtime(1f);
                // ȸ�� �ð� 1�� ����
                energyChargeCount--;
                // ȸ�� �ð��� 0 ���ϰ� �Ǹ�
                if (energyChargeCount <= 0)
                {
                    // ������ �������� ȸ���Ѵ�.
                    GameManager.CurrentUser.CurrentEnergy++;
                    // ȸ�� �ð��� �ʱ�ȭ �Ѵ�.
                    energyChargeCount = (int)Constant.energyChargeTime;
                }
                // UI�� ������Ʈ �Ѵ�.
                GameManager.UIManager.ShowRemainTime(energyChargeCount);
            }
        }
    }
}