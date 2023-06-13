using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace Portfolio.WorldMap
{
    public class WorldMapUIManager : MonoBehaviour
    {
        [SerializeField] ScrollRect worldMapScrollView;
        [SerializeField] MapNode currentNode;
        [SerializeField] RectTransform currentPosArrow;

        [SerializeField] float scrollViewMoveTime = 5f;

        public ScrollRect WorldMapScrollView { get => worldMapScrollView; }
        public MapNode CurrentChoiceNode
        {
            get
            {
                return WorldMapManager.Instance.CurrentUserChoiceNode;
            }
        }

        private void Start()
        {
            worldMapScrollView.verticalNormalizedPosition = 0;
            worldMapScrollView.horizontalNormalizedPosition = 0;
        }

        private IEnumerator ScrollViewContentMoveCoroutine(float time, float horizontalPoint, float verticalPoint)
        {
            float timer = 0f;
            float currentVertical = worldMapScrollView.verticalNormalizedPosition;
            float currentHorizontal = worldMapScrollView.horizontalNormalizedPosition;
            Debug.Log(currentHorizontal + " : " + currentVertical);
            Debug.Log(horizontalPoint + " : " + verticalPoint);
            while (timer <= 1f)
            {
                timer += Time.deltaTime / time;
                worldMapScrollView.horizontalNormalizedPosition = Mathf.Lerp(currentHorizontal, horizontalPoint, timer);
                worldMapScrollView.verticalNormalizedPosition = Mathf.Lerp(currentVertical, verticalPoint, timer);
                yield return null;
            }
        }
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 100), "다음 노드 위치로 이동"))
            {
                currentNode = currentNode.nextNodeList.First();
                if (currentNode != null)
                {
                    var point = currentNode.GetContentNormalizePosition();
                    StartCoroutine(ScrollViewContentMoveCoroutine(scrollViewMoveTime, point.x, point.y));
                }
            }

            if (GUI.Button(new Rect(110, 10, 100, 100), "이전 노드 위치로 이동"))
            {
                currentNode = currentNode.prevNode;
                if (currentNode != null)
                {
                    var point = currentNode.GetContentNormalizePosition();
                    StartCoroutine(ScrollViewContentMoveCoroutine(scrollViewMoveTime, point.x, point.y));
                }
            }

            if (GUI.Button(new Rect(10, 110, 100, 100), "현재 좌표 확인"))
            {
                float currentVertical = worldMapScrollView.verticalNormalizedPosition;
                float currentHorizontal = worldMapScrollView.horizontalNormalizedPosition;
                Debug.Log(currentHorizontal + " : " + currentVertical);
            }
        }
    }
}