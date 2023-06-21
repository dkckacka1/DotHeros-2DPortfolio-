using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Portfolio.UI;

namespace Portfolio.Lobby.Hero
{
    public class UnitSlotHeroCompositionSelector : MonoBehaviour
    {
        [SerializeField] UnitSlotUI unitSlotUI;
        [SerializeField] Button button;
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI;
        [SerializeField] RectTransform mainSelectImage;
        [SerializeField] RectTransform subSelectImage;
        [SerializeField] Image cantSelectImage;

        private bool canSelect = true;
        private bool isMainSelect;
        private bool isSubSelect;

        public bool IsSelected
        {
            get => isMainSelect || isSubSelect;
        }

        public Unit CurrentUnit
        {
            get => unitSlotUI.CurrentUnit;
        }
        public bool IsMainSelect
        {
            get => isMainSelect;
            set 
            { 
                isMainSelect = value; 
                if (IsMainSelect == true)
                {
                    IsSubSelect = false;
                }
                mainSelectImage.gameObject.SetActive(isMainSelect);
            }
        }
        public bool IsSubSelect 
        { 
            get => isSubSelect;
            set
            {
                isSubSelect = value;
                if (isSubSelect == true)
                {
                    IsMainSelect = false;
                }
                subSelectImage.gameObject.SetActive(isSubSelect);
            }
        }

        public bool CanSelect
        {
            get => canSelect;
            set
            {
                canSelect = value;
                //Debug.Log(canSelect);
                cantSelectImage.gameObject.SetActive(!canSelect);
            }
        }

        private void OnEnable()
        {
            button.onClick.AddListener(SelectUnit);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(SelectUnit);
            ResetSelect();
        }

        public void SelectUnit()
        {
            if (canSelect)
            {
                unitCompositionPanelUI.InsertUnit(this);
            }
        }

        public void ResetSelect()
        {
            IsMainSelect = false;
            IsSubSelect = false;
        }
    }

}