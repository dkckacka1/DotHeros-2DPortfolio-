using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class EquipmentItemData : ItemData
    {
        public ItemGrade equipmentGrade = ItemGrade.Normal;
        public ElementalType equipmentElementType;
        public EquipmentItemType equipmentType;
        public SetType setType;

        public int reinforceCount = 0;
        public EquipmentOptionStat optionStat_1_Type = EquipmentOptionStat.NONE;
        public float optionStat_1_value = 0;
        public EquipmentOptionStat optionStat_2_Type = EquipmentOptionStat.NONE;
        public float optionStat_2_value = 0;
        public EquipmentOptionStat optionStat_3_Type = EquipmentOptionStat.NONE;
        public float optionStat_3_value = 0;
        public EquipmentOptionStat optionStat_4_Type = EquipmentOptionStat.NONE;
        public float optionStat_4_value = 0;
    }
}