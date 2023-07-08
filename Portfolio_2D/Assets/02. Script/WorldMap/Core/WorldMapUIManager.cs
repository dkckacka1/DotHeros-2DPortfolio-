using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/*
 * ����� UI�� �����ϴ� �Ŵ��� Ŭ����
 */

namespace Portfolio.WorldMap
{
    public class WorldMapUIManager : MonoBehaviour
    {
        [Header("WorldMap")]
        [SerializeField] ScrollRect worldMapScrollView;     // ����� ��ũ�� ��
        [SerializeField] RectTransform currentPosArrow;     // ���� ���� ȭ��ǥ
        [SerializeField] float scrollViewMoveTime = 5f;     // ����� ��ũ�� �䰡 �̵��ϴ� �ð�
        IEnumerator worldMapScrolling;                      // ��ũ�Ѻ� �̵� �ڷ�ƾ

        [Header("Node")]
        [SerializeField] RectTransform nodeLineParent;          // �� ��� ������ �θ� ������Ʈ
        [SerializeField] RectTransform nodeLinePrefab;          // �� ��� ���� ������
        [SerializeField] RectTransform nodeArrowPrefab;         // �� ��� ȭ��ǥ ������
        public RectTransform NodeLinePrefab => nodeLinePrefab;  
        public RectTransform NodeLineParent => nodeLineParent;   
        public RectTransform NodeArrowPrefab => nodeArrowPrefab; 

        public ScrollRect WorldMapScrollView { get => worldMapScrollView; }

        // ���� ������ �� ���
        public MapNode CurrentChoiceNode
        {
            get
            {
                return WorldMapManager.Instance.CurrentUserChoiceNode;
            }
        }

        // �ʳ�尡 ���õǸ� ���õ� �� ��尡 ȭ�� ����� �� �� �ֵ��� ��ũ�� �並 �̵���ŵ�ϴ�.
        public void MoveMapNode(MapNode choiceMapNode)
        {
            // �̹� ��ũ�� �䰡 �̵����̾��ٸ� �̵��� ��ҽ�ŵ�ϴ�.
            if (worldMapScrolling != null)
            {
                StopCoroutine(worldMapScrolling);
            }

            // ��ũ�Ѻ� �̵� �ڷ�ƾ�� �����մϴ�.
            worldMapScrolling = ScrollViewContentMoveCoroutine(scrollViewMoveTime, choiceMapNode);
            // ��ũ�Ѻ� �̵��� �����մϴ�.
            StartCoroutine(worldMapScrolling);

            //���� �� ȭ��ǥ�� �� ��� ���� ��ġ ��ŵ�ϴ�.
            currentPosArrow.position = new Vector2((choiceMapNode.transform as RectTransform).position.x, (choiceMapNode.transform as RectTransform).position.y + (choiceMapNode.transform as RectTransform).sizeDelta.y * 0.5f);
        }

        // ��ũ�� �並 �̵� ��ŵ�ϴ�.
        private IEnumerator ScrollViewContentMoveCoroutine(float time, MapNode nextMapNode)
        {
            // Ÿ�̸Ӹ� �����մϴ�.
            float timer = 0f;

            // ��ũ�� ���� ��ġ���� Ȯ���մϴ�.
            var contentAnchoredPosition = worldMapScrollView.content.anchoredPosition;
            // ��ũ�� �� ����Ʈ�� ��ġ�� �ʳ���� ��ǥ���� Ȯ���մϴ�.
            // �̵��� ��ǥ�� ��ü �� ������ȿ� ��ġ�ؾ� �ϱ⿡ ��ũ�� �� ��ü �������� �����̳��� �����մϴ�.
            var mapNodeAnchoredPosition = new Vector2(Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.x * -1, worldMapScrollView.viewport.rect.width / 2 * -1, worldMapScrollView.viewport.rect.width / 2),
                Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.y * -1, worldMapScrollView.viewport.rect.height / 2 * -1, worldMapScrollView.viewport.rect.height / 2));
            Vector2 contentMovePos = contentAnchoredPosition;

            while (timer <= 1f)
            {
                timer += Time.deltaTime / time;
                // �̵��� ���� �������� �ϵ� ó������ ������ ������ ���� ������ �̵��ϵ���
                // Sin ������ �̿��մϴ�.
                contentMovePos.x = Mathf.Lerp(contentAnchoredPosition.x, mapNodeAnchoredPosition.x, Mathf.Sin(timer * Mathf.PI * 0.5f));
                contentMovePos.y = Mathf.Lerp(contentAnchoredPosition.y, mapNodeAnchoredPosition.y, Mathf.Sin(timer * Mathf.PI * 0.5f));
                worldMapScrollView.content.anchoredPosition = contentMovePos;
                yield return null;
            }
            // ������ �ð��� ������ �̵�����
        }
    }
}