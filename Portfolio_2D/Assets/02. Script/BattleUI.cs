using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private BattleSequenceUI sequenceUI;

        public BattleSequenceUI SequenceUI { get => sequenceUI;}
    }
}