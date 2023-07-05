using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * ���� ������ �˷��ִ� UI Ŭ����
 */

namespace Portfolio.Battle
{
    public class BattleSequenceUI : MonoBehaviour
    {
        [SerializeField] Image sequenceUI;                      // �� ���� ��
        [SerializeField] BattleUnitSequenceUI sequenceUIObject; // ���� UI ������Ʈ

        private float sequenceUIHeight;                         // �� ������� ����
        private float unitSequenceUIHeight;                     // ���� �� ���� UI�� ����
        private float sequenceDefaultYPos;                      // �� ���� �ٿ��� ���� �� ���� UI�� ���������� �ʵ��� ����

        private void Awake()
        {
            // �� ���� �� �̹����� ����
            sequenceUIHeight = sequenceUI.rectTransform.rect.height;
            // ���� �� ���� UI�� ����
            unitSequenceUIHeight = (sequenceUIObject.transform as RectTransform).rect.height;
            // �� ���� �ٿ��� ���� �� ���� UI�� ���������� �ʵ��� ����
            sequenceDefaultYPos = sequenceUIHeight - unitSequenceUIHeight;
        }

        // ���� �� ���� UI�� �� ��������� ��ġ�ϵ��� ǥ�����ش�.
        public void SetSequenceUnitUIYPosition(BattleUnitSequenceUI targetSequenceUI, float normalizedYPos)
        {
            // ���� �� ���� UI�� �� ������� ��� ��ġ�ؾ��ϴ��� ����ȭ�� y��ǥ���� �ް�
            // �� ����� �̹����� �˸´� ��ġ�� ��ġ�ϰ� �Ѵ�.
            float yPos = ((1 - normalizedYPos) * sequenceDefaultYPos) * -1;
            (targetSequenceUI.transform as RectTransform).anchoredPosition = new Vector2(0, yPos);
        }
    }

}