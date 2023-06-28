using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class TimeChecker : MonoBehaviour
    {
        public int energyChargeCount = (int)Constant.energyChargeTime;

        public void CheckEnergy()
        {
            StartCoroutine(EnergyCheckCoroutine());
        }

        private IEnumerator EnergyCheckCoroutine()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(1f);
                energyChargeCount--;
                if (energyChargeCount <= 0)
                {
                    GameManager.CurrentUser.CurrentEnergy++;
                    energyChargeCount = (int)Constant.energyChargeTime;
                }
                GameManager.UIManager.ShowRemainTime(energyChargeCount);
            }
        }
    }
}