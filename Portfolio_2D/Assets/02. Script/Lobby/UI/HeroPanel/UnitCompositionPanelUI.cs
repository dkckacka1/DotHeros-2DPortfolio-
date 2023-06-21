using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitCompositionPanelUI : MonoBehaviour
    {
        [SerializeField] CompositionUnitSlot mainUnitSlot;
        [SerializeField] List<CompositionUnitSlot> subUnitSlotList;
        [SerializeField] Button heroCompositionBtn;
        [SerializeField] TextMeshProUGUI explainText;

        private void OnDisable()
        {
            mainUnitSlot.Reset();
            foreach (var slot in subUnitSlotList)
            {
                slot.Reset();
            }
        }

        private void OnEnable()
        {
            foreach (var slot in subUnitSlotList)
            {
                slot.ShowLock();
            }

            mainUnitSlot.GetComponent<Toggle>().isOn = true;
        }
    }

}