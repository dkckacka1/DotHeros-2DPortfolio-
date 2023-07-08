using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/*
 * 월드맵 UI를 관리하는 매니저 클래스
 */

namespace Portfolio.WorldMap
{
    public class WorldMapUIManager : MonoBehaviour
    {
        [Header("WorldMap")]
        [SerializeField] ScrollRect worldMapScrollView;     // 월드맵 스크롤 뷰
        [SerializeField] RectTransform currentPosArrow;     // 현재 선택 화살표
        [SerializeField] float scrollViewMoveTime = 5f;     // 월드맵 스크롤 뷰가 이동하는 시간
        IEnumerator worldMapScrolling;                      // 스크롤뷰 이동 코루틴

        [Header("Node")]
        [SerializeField] RectTransform nodeLineParent;          // 맵 노드 라인의 부모 오브젝트
        [SerializeField] RectTransform nodeLinePrefab;          // 맵 노드 라인 프리팹
        [SerializeField] RectTransform nodeArrowPrefab;         // 맵 노드 화살표 프리팹
        public RectTransform NodeLinePrefab => nodeLinePrefab;  
        public RectTransform NodeLineParent => nodeLineParent;   
        public RectTransform NodeArrowPrefab => nodeArrowPrefab; 

        public ScrollRect WorldMapScrollView { get => worldMapScrollView; }

        // 현재 선택한 맵 노드
        public MapNode CurrentChoiceNode
        {
            get
            {
                return WorldMapManager.Instance.CurrentUserChoiceNode;
            }
        }

        // 맵노드가 선택되면 선택된 맵 노드가 화면 가운데로 올 수 있도록 스크롤 뷰를 이동시킵니다.
        public void MoveMapNode(MapNode choiceMapNode)
        {
            // 이미 스크롤 뷰가 이동중이었다면 이동을 취소시킵니다.
            if (worldMapScrolling != null)
            {
                StopCoroutine(worldMapScrolling);
            }

            // 스크롤뷰 이동 코루틴을 정의합니다.
            worldMapScrolling = ScrollViewContentMoveCoroutine(scrollViewMoveTime, choiceMapNode);
            // 스크롤뷰 이동을 시작합니다.
            StartCoroutine(worldMapScrolling);

            //현재 맵 화살표를 맵 노드 위에 위치 시킵니다.
            currentPosArrow.position = new Vector2((choiceMapNode.transform as RectTransform).position.x, (choiceMapNode.transform as RectTransform).position.y + (choiceMapNode.transform as RectTransform).sizeDelta.y * 0.5f);
        }

        // 스크롤 뷰를 이동 시킵니다.
        private IEnumerator ScrollViewContentMoveCoroutine(float time, MapNode nextMapNode)
        {
            // 타이머를 가동합니다.
            float timer = 0f;

            // 스크롤 뷰의 위치값을 확인합니다.
            var contentAnchoredPosition = worldMapScrollView.content.anchoredPosition;
            // 스크롤 뷰 컨텐트가 위치할 맵노드의 좌표값을 확인합니다.
            // 이동할 좌표가 전체 맵 사이즈안에 위치해야 하기에 스크롤 뷰 전체 사이즈의 반절이내로 제한합니다.
            var mapNodeAnchoredPosition = new Vector2(Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.x * -1, worldMapScrollView.viewport.rect.width / 2 * -1, worldMapScrollView.viewport.rect.width / 2),
                Mathf.Clamp((nextMapNode.transform as RectTransform).anchoredPosition.y * -1, worldMapScrollView.viewport.rect.height / 2 * -1, worldMapScrollView.viewport.rect.height / 2));
            Vector2 contentMovePos = contentAnchoredPosition;

            while (timer <= 1f)
            {
                timer += Time.deltaTime / time;
                // 이동을 보간 형식으로 하되 처음에는 빠르게 하지만 점차 느리게 이동하도록
                // Sin 보간을 이용합니다.
                contentMovePos.x = Mathf.Lerp(contentAnchoredPosition.x, mapNodeAnchoredPosition.x, Mathf.Sin(timer * Mathf.PI * 0.5f));
                contentMovePos.y = Mathf.Lerp(contentAnchoredPosition.y, mapNodeAnchoredPosition.y, Mathf.Sin(timer * Mathf.PI * 0.5f));
                worldMapScrollView.content.anchoredPosition = contentMovePos;
                yield return null;
            }
            // 설정한 시간이 끝나면 이동종료
        }
    }
}