using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class SkillStack
    {
        private int skillID;
        private int stackCount;

        private event EventHandler OnSkillAction;
        private event EventHandler OnStackEndAction;
        public int StackCount { get => stackCount; set => stackCount = value; }
        public int SkillID { get => skillID; }

        public SkillStack(int skillID, int stackCount, EventHandler OnSkillAction, EventHandler OnStackEndAction)
        {
            this.skillID = skillID;
            this.stackCount = stackCount;
            this.OnSkillAction = OnSkillAction;
            this.OnStackEndAction = OnStackEndAction;
        }

        public void ProcessStack()
        {
            if (stackCount <= 0) return;

            stackCount--;
            OnSkillAction.Invoke(this, EventArgs.Empty);

            if (stackCount <= 0)
            {
                OnStackEndAction.Invoke(this, EventArgs.Empty);
            }
        }
    }
}