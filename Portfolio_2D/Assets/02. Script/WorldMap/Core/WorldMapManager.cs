using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 월드맵 씬을 관리하는 매니저 클래스 
 */

namespace Portfolio.WorldMap
{
    public class WorldMapManager : MonoBehaviour
    {
        //===========================================================
        // Singleton
        //===========================================================
        private static WorldMapManager instance;
        public static WorldMapManager Instance { get => instance; }
        private static WorldMapUIManager worldMapUIManager;
        public static WorldMapUIManager UIManager { get => worldMapUIManager; }

        List<MapNode> worldNodeList = new List<MapNode>();  // 맵 노드 리스트
        MapNode currentUserChoiceNode;                      // 현재 유저가 선택한 맵

        public MapNode CurrentUserChoiceNode
        {
            get
            {
                return currentUserChoiceNode;
            }
            set
            // 유저가 맵 노드를 선택합니다.
            {
                // 선택한 맵노드가 화면 중앙으로 오도록 합니다.
                worldMapUIManager.MoveMapNode(value);
                currentUserChoiceNode = value;
            }
        }


        // 싱글턴 생성
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            worldMapUIManager = GetComponentInChildren<WorldMapUIManager>();

            // 초기화 합니다.
            foreach (var node in worldMapUIManager.WorldMapScrollView.content.GetComponentsInChildren<MapNode>())
            {
                worldNodeList.Add(node);
            }
        }

        private void Start()
        {
            // 노드 끼리 이어주는 함수 재귀형으로 되어있음
            NodeSetting(worldNodeList[0]);
            // 처음 키면 첫번째 맵 노드로 이동합니다.
            CurrentUserChoiceNode = worldNodeList[0];
        }

        // ORDER : 트리 구조로 만든 맵 노드 트리를 DFS 재귀를 이용해서 만든 맵노드 끼리 노드 연결
        // 맵 노드끼리 이어 줍니다
        private void NodeSetting(MapNode currentMapNode)
        {
            // 현재 맵 노드의 라인 부모 오브젝트, 라인 프리팹, 맵 노드 화살표 부모 오브젝트를 참조합니다.
            RectTransform nodeLinePrefab = UIManager.NodeLinePrefab;
            RectTransform nodeLineParent = UIManager.NodeLineParent;
            RectTransform nodeArrowPrefab = UIManager.NodeArrowPrefab;

            foreach (var nextNode in currentMapNode.nextNodeList)
            // 현재 맵노드의 다음 노드들을 순회합니다.
            {
                // 맵노드 라인을 생성합니다.
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                // 맵 노드 라인의 길이는 현재 맵 노드와 다음 맵 노드의 사이 길이입니다.
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                // 맵 노드 라인이 각 맵노드를 이어주도록 회전 시켜 줍니다.
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - currentMapNode.transform.position));

                // 맵 노드 화살표를 생성합니다.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                // 화살표를 다음 맵 노드를 바라보도록 회전 시킵니다.
                var arrowAngle = Mathf.Atan2(nextNode.transform.position.y - currentMapNode.transform.position.y, nextNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);

                // 화살표를 누르면 유저가 선택한 맵노드를 변경하고 화면 가운데로 이동시킵니다.
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = nextNode; UIManager.ShowMapInfo(nextNode.Map); });

                // 이 노드는 이 다음 노드의 이전 노드가 됩니다.
                nextNode.SetPrevNode(currentMapNode);
                // 다음 노드도 노드 세팅을 수행합니다. DFS 재귀
                NodeSetting(nextNode);
            }

            if (currentMapNode.prevNode != null)
            // 이 노드가 이전 노드가 존재한다면
            {
                // 맵 노드 화살표를 생성합니다.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = currentMapNode.prevNode; UIManager.ShowMapInfo(currentMapNode.prevNode.Map); });
            }


            if (GameManager.CurrentUser.IsClearMap(currentMapNode.Map.MapID))
            // 이미 클리어한 맵이라면
            {
                // 맵 노드 버튼과 화살표를 활성화 하고 잠금이미지를 숨겨줍니다.
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);
                currentMapNode.ShowNodeArrow(true);

                // 다음 노드들의 버튼을 활성화하고 잠금이미지를 숨겨줍니다.
                foreach (var nextNode in currentMapNode.nextNodeList)
                {
                    nextNode.SetNodeBtnInteractable(true);
                    nextNode.ShowLockImage(false);
                }
            }
            else if (currentMapNode.Map.MapID == 500)
            // 맵이 초반 맵이라면
            {
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);

            }

            // 이중 클리어한 맵노드는 클리어 표시를 합니다.
            currentMapNode.ShowClearObject(GameManager.CurrentUser.IsClearMap(currentMapNode.Map.MapID));
        }

        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }

}