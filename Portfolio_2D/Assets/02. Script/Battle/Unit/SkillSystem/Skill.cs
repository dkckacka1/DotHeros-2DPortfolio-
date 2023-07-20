using Portfolio.Battle;
using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ORDER : #2) ����� �̿��Ͽ� ���� ��ų �ý��� ����
/*
 * ��ų�� �⺻�� ���� �߻� Ŭ����
 */

namespace Portfolio.skill
{
    public abstract class Skill
    {
        protected SkillData skillData;                                  // ��ų ������

        public SkillData GetData => skillData;                          // ��ų �����͸� �����Ѵ�.
        public Sprite skillSprite;                                      // ��ų�� �̹����� ǥ���� ��������Ʈ
        public List<Condition> conditionList = new List<Condition>() { null, null, null };  // ��ų�� �������ִ� �����̻�

        public Skill(SkillData skillData)
        {
            // �����͸� �����Ѵ�.
            this.skillData = skillData;
            // ��������Ʈ �̹����� �����´�.
            this.skillSprite = GameManager.Instance.GetSprite(skillData.skillIconSpriteName);


            if (skillData.conditinID_1 != -1)
                // ��ų �����Ϳ��� �����̻��� �����ϸ�
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_1, out Condition condition_1))
                    // �����̻��� ������ ����Ʈ�� �־��ش�.
                {
                    conditionList[0] = condition_1;
                }
                else
                    // �����̻��� ���� ���� ������
                {
                    // null �� �ֱ�
                    conditionList[0] = null;
                }
            }
            else
            // ��ų �����Ϳ��� �����̻��� �������� ������
            {
                // null �� �ֱ�
                conditionList[0] = null;
            }

            if (skillData.conditinID_2 != -1)
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_2, out Condition condition_2))
                {
                    conditionList[1] = condition_2;
                }
                else
                {
                    conditionList[1] = null;
                }
            }
            else
            {
                conditionList[1] = null;
            }

            if (skillData.conditinID_3 != -1)
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_3, out Condition condition_3))
                {
                    conditionList[2] = condition_3;
                }
                else
                {
                    conditionList[2] = null;
                }
            }
            else
            {
                conditionList[2] = null;
            }
        }

        // ORDER : string.Format�� Ȱ���� ��ų ���� ����
        // ��ų ������ �����ش�.
        public virtual string GetDesc(int skillLevel)
        {
            string desc = string.Empty;
            object[] values = GetLevelValue(); // ��ų ������ ���� ����� ��ġ��

            if (values.Length == 0)
                // ����� ��ġ�� ���ٸ�
            {
                // ��ų ������ �״�� ����
                desc = GetData.skillDesc;
            }
            else
                // ����� ��ġ�� �ִٸ�
            {
                float[] SetSkillLevelValues = values.Select(value => (float)value * skillLevel).ToArray();
                // ��ų ������ string.format ������� �����´�.
                // Cast<object>().ToArray()�� ���������� �����Ϸ��� SetSkillLevelValues�� �迭�� �ƴ� �ϳ��� ������Ʈ�� �Ǵ��Ͽ�
                // public static String Format(String format, object arg0); �Լ��� ȣ���ع�����.
                // Cast<object>().ToArray()�� ����ؾ�
                // public static String Format(String format, params object[] args); �Լ��� ȣ���Ѵ�.
                desc = string.Format(GetData.skillDesc, SetSkillLevelValues.Cast<object>().ToArray());
            }

            return desc;
        }

        // ��ų�� ����Ҷ� ȣ��Ǵ� ���� �Լ�
        public virtual void Action(object sender, SkillActionEventArgs e)
        {
            // ��ų�� ����ϸ� �α׿� ��ϵȴ�.
            BattleManager.UIManager.AddLog(GetLogString(e));
        }

        // �α׿� ��ϵ� �α� ������ ����� ���� ���� �Լ�
        protected abstract string GetLogString(SkillActionEventArgs e);

        // ��ų �����Ϳ��� ��ġ�� �����´�.
        private object[] GetLevelValue()
        {
            List<object> levelValues = new List<object>();
            if (GetData.skillLevelValue_1 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_1);
            }

            if (GetData.skillLevelValue_2 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_2);
            }

            if (GetData.skillLevelValue_3 != 0)
            {
                levelValues.Add(GetData.skillLevelValue_3);
            }

            return levelValues.ToArray();
        }
    } 
}
