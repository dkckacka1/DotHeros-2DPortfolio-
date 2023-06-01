using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.condition
{
    public class ConditionData : Data
    {
        public string conditionName;
        public string conditionDesc;
        public string conditionClassName;

        public ConditionType conditionType;

        public bool isBuff;
        public bool isOverlaping;
        public bool isResetCount;

    }
}