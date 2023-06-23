using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Portfolio.Battle
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("DamageTextPool")]
        [SerializeField] BattleTextUI battleTextPrefab;
        [SerializeField] GameObject battleTextParents;
        [SerializeField] int battleTextDefualtCreateNum;

        Queue<BattleTextUI> battleTextPool = new Queue<BattleTextUI>();

        private void Awake()
        {
            for(int i =0; i < battleTextDefualtCreateNum; i++)
            {
                CreateBattleText();
            }
        }

        private BattleTextUI CreateBattleText()
        {
            var newBattleText = Instantiate(battleTextPrefab, battleTextParents.transform);
            ReleaseBattleText(newBattleText);
            return newBattleText;
        }

        public BattleTextUI SpawnBattleText(bool isActive = true)
        {
            if (battleTextPool.Count == 0)
            {
                CreateBattleText();
            }

            var battleText = battleTextPool.Dequeue();
            battleText.gameObject.SetActive(isActive);

            return battleText;
        }

        public void ReleaseBattleText(BattleTextUI releaseBattleText)
        {
            releaseBattleText.gameObject.SetActive(false);
            releaseBattleText.transform.position = Vector3.zero;
            releaseBattleText.transform.rotation = Quaternion.identity;
            battleTextPool.Enqueue(releaseBattleText);
        }
    } 
}