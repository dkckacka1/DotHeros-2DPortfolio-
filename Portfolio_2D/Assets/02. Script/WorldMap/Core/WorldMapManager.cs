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
        float resolutionRate = 0f;                          // 현재 스크린 사이즈와 캔버스 해상도 사이즈 비율
        public MapNode rootNode;                            // 모든 맵노드의 루트 노드

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

        public WorldMapUIManager GetComponentinchildren { get; set; }

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
            // 현재 스크린 비율을 가져옵니다.
            resolutionRate = GameManager.UIManager.GetScreenCanvasRate;

            // 노드 끼리 이어주는 함수 재귀형으로 되어있음
            NodeSetting(worldNodeList[0]);
            // 처음 키면 첫번째 맵 노드로 이동합니다.
            CurrentUserChoiceNode = worldNodeList[0];
        }

        // ORDER : 트리 구조로 만든 맵 노드 트리를 DFS 재귀를 이용해서 만든 맵노드 끼리 노드 연결
        // 맵 노드끼리 이어 줍니다
        private void NodeSetting(MapNode currentMapNode)
        {
            foreach (var nextNode in currentMapNode.nextNodeList)
            // 현재 맵노드의 다음 노드들을 순회합니다.
            {
                // 다음 노드도 노드 세팅을 수행합니다. DFS 재귀
                NodeSetting(nextNode);
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

        // 로비로 돌아갑니다.
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }

        // 맵 노드의 화살표에 넣어줄 버튼 클릭 리스너
        // 화살표를 누르면 해당 맵노드로 이동합니다.
        public void BTN_OnClick_ChoiceNode(MapNode choiceNode)
        {
            CurrentUserChoiceNode = choiceNode;
            UIManager.ShowMapInfo(choiceNode.Map);
        }
    }

}