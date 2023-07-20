using Portfolio.Battle;
using Portfolio.condition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ORDER : #2) 상속을 이용하여 만든 스킬 시스템 구현
/*
 * 스킬의 기본을 만들 추상 클래스
 */

namespace Portfolio.skill
{
    public abstract class Skill
    {
        protected SkillData skillData;                                  // 스킬 데이터

        public SkillData GetData => skillData;                          // 스킬 데이터를 리턴한다.
        public Sprite skillSprite;                                      // 스킬의 이미지를 표기할 스프라이트
        public List<Condition> conditionList = new List<Condition>() { null, null, null };  // 스킬이 가지고있는 상태이상

        public Skill(SkillData skillData)
        {
            // 데이터를 참조한다.
            this.skillData = skillData;
            // 스프라이트 이미지를 가져온다.
            this.skillSprite = GameManager.Instance.GetSprite(skillData.skillIconSpriteName);


            if (skillData.conditinID_1 != -1)
                // 스킬 데이터에서 상태이상이 존재하면
            {
                if (GameManager.Instance.TryGetCondition(skillData.conditinID_1, out Condition condition_1))
                    // 상태이상을 가져와 리스트에 넣어준다.
                {
                    conditionList[0] = condition_1;
                }
                else
                    // 상태이상이 존재 하지 않으면
                {
                    // null 값 넣기
                    conditionList[0] = null;
                }
            }
            else
            // 스킬 데이터에서 상태이상이 존재하지 않으면
            {
                // null 값 넣기
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

        // ORDER : string.Format을 활용한 스킬 설명 구현
        // 스킬 설명을 보여준다.
        public virtual string GetDesc(int skillLevel)
        {
            string desc = string.Empty;
            object[] values = GetLevelValue(); // 스킬 레벨에 의해 변경될 수치들

            if (values.Length == 0)
                // 변경될 수치가 없다면
            {
                // 스킬 설명을 그대로 리턴
                desc = GetData.skillDesc;
            }
            else
                // 변경될 수치가 있다면
            {
                float[] SetSkillLevelValues = values.Select(value => (float)value * skillLevel).ToArray();
                // 스킬 설명을 string.format 방식으로 가져온다.
                // Cast<object>().ToArray()를 하지않으면 컴파일러가 SetSkillLevelValues을 배열이 아닌 하나의 오브젝트로 판단하여
                // public static String Format(String format, object arg0); 함수를 호출해버린다.
                // Cast<object>().ToArray()를 사용해야
                // public static String Format(String format, params object[] args); 함수를 호출한다.
                desc = string.Format(GetData.skillDesc, SetSkillLevelValues.Cast<object>().ToArray());
            }

            return desc;
        }

        // 스킬을 사용할때 호출되는 가상 함수
        public virtual void Action(object sender, SkillActionEventArgs e)
        {
            // 스킬을 사용하면 로그에 등록된다.
            BattleManager.UIManager.AddLog(GetLogString(e));
        }

        // 로그에 등록될 로그 정보를 만드는 순수 가상 함수
        protected abstract string GetLogString(SkillActionEventArgs e);

        // 스킬 데이터에서 수치를 가져온다.
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
