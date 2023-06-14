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
        [SerializeField] RectTransform currentPosArrow;
        [SerializeField] float scrollViewMoveTime = 5f;

        [Header("Node")]
        [SerializeField] RectTransform nodeLineParent;
        public RectTransform NodeLineParent => nodeLineParent;
        [SerializeField] RectTransform nodeLinePrefab;
        public RectTransform NodeLinePrefab => nodeLinePrefab;

        public ScrollRect WorldMapScrollView { get => worldMapScrollView; }
        public MapNode CurrentChoiceNode
        {
            get
            {
                return WorldMapManager.Instance.CurrentUserChoiceNode;
            }
        }

        public void MoveMapNode(MapNode choiceMapNode)
        {
            StartCoroutine(ScrollViewContentMoveCoroutine(scrollViewMoveTime, choiceMapNode));
            currentPosArrow.position = new Vector2((choiceMapNode.transform as RectTransform).position.x, (choiceMapNode.transform as RectTransform).position.y + (choiceMapNode.transform as RectTransform).sizeDelta.y * 0.5f);
        }

        private IEnumerator ScrollViewContentMoveCoroutine(float time, MapNode nextMapNode)
        {
            float timer = 0f;
            var contentAnchoredPosition = worldMapScrollView.content.anchoredPosition;
            var mapNodeAnchoredPosition = new Vector2(Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.x * -1, worldMapScrollView.viewport.rect.width / 2 * -1, worldMapScrollView.viewport.rect.width / 2),
                Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.y * -1, worldMapScrollView.viewport.rect.height / 2 * -1, worldMapScrollView.viewport.rect.height / 2));
            Vector2 contentMovePos = contentAnchoredPosition;

            while (timer <= 1f)
            {
                timer += Time.deltaTime / time;
                contentMovePos.x = Mathf.Lerp(contentAnchoredPosition.x, mapNodeAnchoredPosition.x, Mathf.Sin(timer * Mathf.PI * 0.5f));
                contentMovePos.y = Mathf.Lerp(contentAnchoredPosition.y, mapNodeAnchoredPosition.y, Mathf.Sin(timer * Mathf.PI * 0.5f));
                worldMapScrollView.content.anchoredPosition = contentMovePos;
                yield return null;
            }
        }
    }
}