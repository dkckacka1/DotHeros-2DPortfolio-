using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public static WorldMapUIManager WorldMapUIManager { get => worldMapUIManager; }

        List<MapNode> worldNodeList = new List<MapNode>();
        MapNode currentUserChoiceNode;
        public MapNode CurrentUserChoiceNode 
        {
            get
            {
                return currentUserChoiceNode;
            }
            set
            {
                worldMapUIManager.MoveMapNode(value);
                currentUserChoiceNode = value;
            }
        }


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
            foreach (var node in worldMapUIManager.WorldMapScrollView.content.GetComponentsInChildren<MapNode>())
            {
                worldNodeList.Add(node);
            }
        }

        private void Start()
        {
            NodeSetting(worldNodeList[0]); // 노드 끼리 이어주는 함수 재귀형으로 되어있음
            CurrentUserChoiceNode = worldNodeList[0];
        }

        private void NodeSetting(MapNode currentMapNode)
        {
            foreach (var nextNode in currentMapNode.nextNodeList)
            {
                // NodeLine 생성
                RectTransform nodeLinePrefab = WorldMapManager.WorldMapUIManager.NodeLinePrefab;
                RectTransform nodeLineParent = WorldMapManager.WorldMapUIManager.NodeLineParent;
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - currentMapNode.transform.position));

                // NodeArrow 생성
                RectTransform nodeArrowPrefab = WorldMapManager.WorldMapUIManager.NodeArrowPrefab;
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(nextNode.transform.position.y - currentMapNode.transform.position.y, nextNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = nextNode; });

                nextNode.SetPrevNode(currentMapNode);
                NodeSetting(nextNode);
            }

            if (currentMapNode.prevNode != null)
            {
                RectTransform nodeArrowPrefab = WorldMapManager.WorldMapUIManager.NodeArrowPrefab;
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = currentMapNode.prevNode; });
            }
        }

        //private void OnGUI()
        //{
        //    if (GUI.Button(new Rect(110, 10, 100, 100), "다음 맵모드"))
        //    {
        //        if (currentUserChoiceNode.HasNextMap)
        //        {
        //            CurrentUserChoiceNode = currentUserChoiceNode.GetNextMapNode;
        //        }
        //    }

        //    if (GUI.Button(new Rect(110, 110, 100, 100), "이전 맵모드"))
        //    {
        //        if (currentUserChoiceNode.HasPrevMap)
        //        {
        //            CurrentUserChoiceNode = currentUserChoiceNode.GetPrevMapNode;
        //        }
        //    }
        //}

        public void ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}