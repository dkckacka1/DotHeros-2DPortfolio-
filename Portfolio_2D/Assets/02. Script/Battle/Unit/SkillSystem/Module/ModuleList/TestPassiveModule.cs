using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.skill
{
    public class TestPassiveModule : Module
    {
        public override void Action(BattleUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public override string ShowDesc(int skillLevel)
        {
            //Debug.Log("TestPassiveModule");
            return "";
        }
    }

}