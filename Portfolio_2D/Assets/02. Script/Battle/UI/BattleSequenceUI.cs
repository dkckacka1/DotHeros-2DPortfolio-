using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio
{
    public class BattleSequenceUI : MonoBehaviour
    {
        [SerializeField] Image sequenceUI;
        [SerializeField] UnitSequenceUI sequenceUIObject;

        private float sequenceUIHeight;
        private float unitSequenceUIHeight;
        private float sequenceDefaultYPos;

        private void Awake()
        {
            sequenceUIHeight = sequenceUI.rectTransform.rect.height;
            unitSequenceUIHeight = (sequenceUIObject.transform as RectTransform).rect.height;
            sequenceDefaultYPos = sequenceUIHeight - unitSequenceUIHeight;
        }

        public void SetSequenceUnitUIYPosition(UnitSequenceUI targetSequenceUI, float normalizedYPos)
        {
            float yPos = ((1 - normalizedYPos) * sequenceDefaultYPos) * -1;
            (targetSequenceUI.transform as RectTransform).anchoredPosition = new Vector2(0, yPos);
            ////targetSequenceUI.transform.position = new Vector3(xPos, yPos);
            ////Debug.Log(targetSequenceUI.transform.position);
            //targetSequenceUI.transform.localPosition = new Vector3(0, yPos);
            //Debug.Log(targetSequenceUI.transform.position);
        }
    }

}