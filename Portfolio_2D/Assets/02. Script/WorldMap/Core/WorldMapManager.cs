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
            RectTransform nodeLinePrefab = WorldMapUIManager.NodeLinePrefab;
            RectTransform nodeLineParent = WorldMapUIManager.NodeLineParent;
            RectTransform nodeArrowPrefab = WorldMapUIManager.NodeArrowPrefab;

            foreach (var nextNode in currentMapNode.nextNodeList)
            {
                // Create NodeLine
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - currentMapNode.transform.position));

                // Create NodeArrow
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(nextNode.transform.position.y - currentMapNode.transform.position.y, nextNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = nextNode; });

                nextNode.SetPrevNode(currentMapNode);
                NodeSetting(nextNode);
            }

            if (currentMapNode.prevNode != null)
            {
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = currentMapNode.prevNode; });
            }

            bool isClear = GameManager.CurrentUser.IsClearMap(currentMapNode.Map.MapID);
            bool isExternalMap = currentMapNode.Map.IsExternMap;

            if(GameManager.CurrentUser.ClearHighestMapID == currentMapNode.Map.MapID)
            {
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);
                currentMapNode.ShowNodeArrow(false);
            }
            else if (isClear)
            {
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);
                currentMapNode.ShowNodeArrow(true);
            }
            else
            {
                currentMapNode.SetNodeBtnInteractable(false);
                currentMapNode.ShowLockImage(true);
                currentMapNode.ShowNodeArrow(false);
            }
        }


        public void ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }
}