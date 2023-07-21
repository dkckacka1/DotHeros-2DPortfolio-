using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
// TODO : 주석
/*
 * 에디터상에서 월드맵씬의 맵 노드를 이어주는 라인과 화살표를 자동적으로 만들어주는 에디터 클래스
 */

namespace Portfolio.WorldMap
{
    // 월드맵 매니저 스크립트 인스펙터창을 수정하기 위한 클래스
    [CustomEditor(typeof(WorldMapManager))]
    public class MapNodeDraw : UnityEditor.Editor
    {
        WorldMapManager worldMapManager;    // 월드맵 매니저
        WorldMapUIManager UIManager;        // 월드맵씬 UI 매니저
        RectTransform nodeLinePrefab;       // 노드 라인 프리팹
        RectTransform nodeLineParent;       // 노드 라인의 부모 오브젝트
        RectTransform nodeArrowPrefab;      // 노드 화살표 프리팹

        // ORDER : 트리 구조로 만든 맵 노드 트리를 DFS 재귀를 이용해서 만든 맵노드 끼리 노드 연결 (에디터 모드에서 버튼하나로 노드라인과 노드 애로우를 그려주고 버튼 리스너 까지 등록해주는 자동화)
        // 게임씬 resolution이 1920 * 1080 이여야지 제대로 동작한다.
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 부착된 오브젝트의 WorldMapManager 참조
            worldMapManager = (WorldMapManager)target;
            UIManager = worldMapManager.GetComponentInChildren<WorldMapUIManager>();
            // 현재 맵 노드의 라인 부모 오브젝트, 라인 프리팹, 맵 노드 화살표 부모 오브젝트를 참조합니다.
            nodeLinePrefab = UIManager.NodeLinePrefab;
            nodeLineParent = UIManager.NodeLineParent;
            nodeArrowPrefab = UIManager.NodeArrowPrefab;

            GUILayout.Space(25f);
            if (GUILayout.Button("맵 그리기"))
            {
                ClearLineAndArrow(worldMapManager.rootNode);
                DrawLineAndArrow(worldMapManager.rootNode);
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("맵 초기화 하기"))
            {
                ClearLineAndArrow(worldMapManager.rootNode);
            }
            // 맵 노드끼리 이어 줍니다
        }

        // 맵 노드들을 순회하면서 노드 라인과 노드 애로우를 그려줍니다.
        // 노드 애로우에는 클릭 이벤트를 추가합니다.
        private void DrawLineAndArrow(MapNode currentMapNode)
        {
            foreach (var nextNode in currentMapNode.nextNodeList)
            // 현재 맵노드의 다음 노드들을 순회합니다.
            {
                // 맵노드 라인을 생성합니다.
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                // 맵 노드 라인의 길이는 현재 맵 노드와 다음 맵 노드의 사이 길이입니다.
                float lineDistance = Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(lineDistance, (nodeLine.transform as RectTransform).sizeDelta.y);
                // 맵 노드 라인이 각 맵노드를 이어주도록 회전 시켜 줍니다.
                Vector2 direction = nextNode.transform.position - currentMapNode.transform.position;
                var angle = Vector2.SignedAngle(Vector2.right, direction);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, angle);

                // 맵 노드 화살표를 생성합니다.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                // 화살표를 다음 맵 노드를 바라보도록 회전 시킵니다.
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                // 화살표를 누르면 유저가 선택한 맵노드를 변경하고 화면 가운데로 이동시키는 함수리스너를
                // 버튼 온클릭 이벤트에 등록합니다.
                // 에디터 스크립트에서 이벤트를 등록하려면 AddPersistentListener 함수 등을 이용해야한다.
                UnityEditor.Events.UnityEventTools.AddObjectPersistentListener<MapNode>(
                    nodeArrow.GetComponentInChildren<Button>().onClick,
                    worldMapManager.BTN_OnClick_ChoiceNode, 
                    nextNode
                    );

                // 이 노드는 이 다음 노드의 이전 노드가 됩니다.
                nextNode.SetPrevNode(currentMapNode);
                // 다음 노드도 노드 세팅을 수행합니다. DFS 재귀
                DrawLineAndArrow(nextNode);
            }

            if (currentMapNode.prevNode != null)
            // 이 노드가 이전 노드가 존재한다면
            {
                // 맵 노드 화살표를 생성합니다.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                // 화살표를 누르면 유저가 선택한 맵노드를 변경하고 화면 가운데로 이동시키는 함수리스너를
                // 버튼 온클릭 이벤트에 등록합니다.
                UnityEditor.Events.UnityEventTools.AddObjectPersistentListener<MapNode>(
                    nodeArrow.GetComponentInChildren<Button>().onClick,
                    worldMapManager.BTN_OnClick_ChoiceNode,
                    currentMapNode.prevNode
                    );

            }
        }

        // 맵 노드의 라인을 전부 제거하면서 맵 노드의 화살표를 제거합니다.
        // 에디터 모드에서 게임오브젝트를 제거하려면 Destory 대신 DestroyImmediate를 사용해야한다.
        private void ClearLineAndArrow(MapNode currentMapNode)
        {
            foreach(var nodeLine in nodeLineParent)
            {
                DestroyImmediate((nodeLine as Transform).gameObject);
            }

            ClearArrow(currentMapNode);
        }

        // 맵 노드들을 순회하면서 맵 노드의 화살표를 제거합니다.
        private void ClearArrow(MapNode currentMapNode)
        {
            foreach(var arrow in currentMapNode.NodeArrowParent.transform)
            {
                DestroyImmediate((arrow as Transform).gameObject);
            }

            foreach(var nextNode in currentMapNode.nextNodeList)
            {
                ClearArrow(nextNode);
            }
        }
    }
}