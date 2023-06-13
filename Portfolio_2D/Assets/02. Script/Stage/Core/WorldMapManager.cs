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
            CurrentUserChoiceNode = worldNodeList[0];
        }

        private void OnGUI()
        {
            //if (GUI.Button(new Rect(110, 10, 100, 100), "현재 맵모드"))
            //{
            //    if (currentUserChoiceNode.HasNextMap)
            //    {
            //        CurrentUserChoiceNode = currentUserChoiceNode;
            //    }
            //}

            if (GUI.Button(new Rect(110, 10, 100, 100), "다음 맵모드"))
            {
                if (currentUserChoiceNode.HasNextMap)
                {
                    CurrentUserChoiceNode = currentUserChoiceNode.GetNextMapNode;
                }
            }

            if (GUI.Button(new Rect(110, 110, 100, 100), "이전 맵모드"))
            {
                if (currentUserChoiceNode.HasPrevMap)
                {
                    CurrentUserChoiceNode = currentUserChoiceNode.GetPrevMapNode;
                }
            }
        }
    }
}