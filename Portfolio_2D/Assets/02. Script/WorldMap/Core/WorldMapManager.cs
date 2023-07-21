using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����� ���� �����ϴ� �Ŵ��� Ŭ���� 
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

        List<MapNode> worldNodeList = new List<MapNode>();  // �� ��� ����Ʈ
        MapNode currentUserChoiceNode;                      // ���� ������ ������ ��
        float resolutionRate = 0f;                          // ���� ��ũ�� ������� ĵ���� �ػ� ������ ����
        public MapNode rootNode;                            // ��� �ʳ���� ��Ʈ ���

        public MapNode CurrentUserChoiceNode
        {
            get
            {
                return currentUserChoiceNode;
            }
            set
            // ������ �� ��带 �����մϴ�.
            {
                // ������ �ʳ�尡 ȭ�� �߾����� ������ �մϴ�.
                worldMapUIManager.MoveMapNode(value);
                currentUserChoiceNode = value;
            }
        }

        public WorldMapUIManager GetComponentinchildren { get; set; }

        // �̱��� ����
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

            // �ʱ�ȭ �մϴ�.
            foreach (var node in worldMapUIManager.WorldMapScrollView.content.GetComponentsInChildren<MapNode>())
            {
                worldNodeList.Add(node);
            }
        }

        private void Start()
        {
            // ���� ��ũ�� ������ �����ɴϴ�.
            resolutionRate = GameManager.UIManager.GetScreenCanvasRate;

            // ��� ���� �̾��ִ� �Լ� ��������� �Ǿ�����
            NodeSetting(worldNodeList[0]);
            // ó�� Ű�� ù��° �� ���� �̵��մϴ�.
            CurrentUserChoiceNode = worldNodeList[0];
        }

        // ORDER : Ʈ�� ������ ���� �� ��� Ʈ���� DFS ��͸� �̿��ؼ� ���� �ʳ�� ���� ��� ����
        // �� ��峢�� �̾� �ݴϴ�
        private void NodeSetting(MapNode currentMapNode)
        {
            foreach (var nextNode in currentMapNode.nextNodeList)
            // ���� �ʳ���� ���� ������ ��ȸ�մϴ�.
            {
                // ���� ��嵵 ��� ������ �����մϴ�. DFS ���
                NodeSetting(nextNode);
            }

            if (GameManager.CurrentUser.IsClearMap(currentMapNode.Map.MapID))
            // �̹� Ŭ������ ���̶��
            {
                // �� ��� ��ư�� ȭ��ǥ�� Ȱ��ȭ �ϰ� ����̹����� �����ݴϴ�.
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);
                currentMapNode.ShowNodeArrow(true);

                // ���� ������ ��ư�� Ȱ��ȭ�ϰ� ����̹����� �����ݴϴ�.
                foreach (var nextNode in currentMapNode.nextNodeList)
                {
                    nextNode.SetNodeBtnInteractable(true);
                    nextNode.ShowLockImage(false);
                }
            }
            else if (currentMapNode.Map.MapID == 500)
            // ���� �ʹ� ���̶��
            {
                currentMapNode.SetNodeBtnInteractable(true);
                currentMapNode.ShowLockImage(false);
            }

            // ���� Ŭ������ �ʳ��� Ŭ���� ǥ�ø� �մϴ�.
            currentMapNode.ShowClearObject(GameManager.CurrentUser.IsClearMap(currentMapNode.Map.MapID));
        }

        // �κ�� ���ư��ϴ�.
        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }

        // �� ����� ȭ��ǥ�� �־��� ��ư Ŭ�� ������
        // ȭ��ǥ�� ������ �ش� �ʳ��� �̵��մϴ�.
        public void BTN_OnClick_ChoiceNode(MapNode choiceNode)
        {
            CurrentUserChoiceNode = choiceNode;
            UIManager.ShowMapInfo(choiceNode.Map);
        }
    }

}