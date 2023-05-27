using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio
{
    public class AISystem : MonoBehaviour
    {
        public bool isAI;

        private float timer = 0f;
        private float turnEndTime = 1f;

        private BattleUnit battleUnit;

        private void Awake()
        {
            battleUnit = GetComponent<BattleUnit>();
        }

        private void Update()
        {
            if (battleUnit.IsTurn && isAI)
            {
                timer += Time.deltaTime;
                if (timer >= turnEndTime)
                {
                    BattleManager.TurnBaseSystem.TurnEnd();
                    timer = 0;
                }
            }
        }
    }
}