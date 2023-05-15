using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class BattleSequenceUI : MonoBehaviour
    {
        [SerializeField] Image sequenceUI;

        private float sequenceUIwidth;

        private void Awake()
        {
            sequenceUIwidth = sequenceUI.rectTransform.rect.width - 50f;
        }

        public void SetSequenceUnitUIXPosition(UnitSequenceUI targetSequenceUI, float normalizedXPos)
        {
            float xPos = normalizedXPos* sequenceUIwidth;
            float yPos = targetSequenceUI.transform.position.y;
            targetSequenceUI.transform.position = new Vector3(xPos, yPos);
        }
    }

}