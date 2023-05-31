using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class PlayerEquipmentItemData
    {
        public EquipmentItemData data;
        public int reinforceLevel = 0;

        public EquipmentOptionStat optionStat_1_Type = EquipmentOptionStat.NONE;
        public float optionStat_1_value = 0;
        public EquipmentOptionStat optionStat_21_Type = EquipmentOptionStat.NONE;
        public float optionStat_2_value = 0;
        public EquipmentOptionStat optionStat_3_Type = EquipmentOptionStat.NONE;
        public float optionStat_3_value = 0;
        public EquipmentOptionStat optionStat_4_Type = EquipmentOptionStat.NONE;
        public float optionStat_4_value = 0;
    }
}