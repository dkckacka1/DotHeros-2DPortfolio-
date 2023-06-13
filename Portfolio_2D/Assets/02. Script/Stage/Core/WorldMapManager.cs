using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public MapNode CurrentUserChoiceNode { get => currentUserChoiceNode; set => currentUserChoiceNode = value; }
        // TODO : 현재 노드맵을 통한 UI 변경 작업중


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
            currentUserChoiceNode = worldNodeList[0];
        }
    }
}