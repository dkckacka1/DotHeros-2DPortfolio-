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
            // ��� ���� �̾��ִ� �Լ� ��������� �Ǿ�����
            NodeSetting(worldNodeList[0]);
            // ó�� Ű�� ù��° �� ���� �̵��մϴ�.
            CurrentUserChoiceNode = worldNodeList[0];
        }

        // ORDER : Ʈ�� ������ ���� �� ��� Ʈ���� DFS ��͸� �̿��ؼ� ���� �ʳ�� ���� ��� ����
        // �� ��峢�� �̾� �ݴϴ�
        private void NodeSetting(MapNode currentMapNode)
        {
            // ���� �� ����� ���� �θ� ������Ʈ, ���� ������, �� ��� ȭ��ǥ �θ� ������Ʈ�� �����մϴ�.
            RectTransform nodeLinePrefab = UIManager.NodeLinePrefab;
            RectTransform nodeLineParent = UIManager.NodeLineParent;
            RectTransform nodeArrowPrefab = UIManager.NodeArrowPrefab;

            foreach (var nextNode in currentMapNode.nextNodeList)
            // ���� �ʳ���� ���� ������ ��ȸ�մϴ�.
            {
                // �ʳ�� ������ �����մϴ�.
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                // �� ��� ������ ���̴� ���� �� ���� ���� �� ����� ���� �����Դϴ�.
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                // �� ��� ������ �� �ʳ�带 �̾��ֵ��� ȸ�� ���� �ݴϴ�.
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - currentMapNode.transform.position));

                // �� ��� ȭ��ǥ�� �����մϴ�.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                // ȭ��ǥ�� ���� �� ��带 �ٶ󺸵��� ȸ�� ��ŵ�ϴ�.
                var arrowAngle = Mathf.Atan2(nextNode.transform.position.y - currentMapNode.transform.position.y, nextNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);

                // ȭ��ǥ�� ������ ������ ������ �ʳ�带 �����ϰ� ȭ�� ����� �̵���ŵ�ϴ�.
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = nextNode; UIManager.ShowMapInfo(nextNode.Map); });

                // �� ���� �� ���� ����� ���� ��尡 �˴ϴ�.
                nextNode.SetPrevNode(currentMapNode);
                // ���� ��嵵 ��� ������ �����մϴ�. DFS ���
                NodeSetting(nextNode);
            }

            if (currentMapNode.prevNode != null)
            // �� ��尡 ���� ��尡 �����Ѵٸ�
            {
                // �� ��� ȭ��ǥ�� �����մϴ�.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { CurrentUserChoiceNode = currentMapNode.prevNode; UIManager.ShowMapInfo(currentMapNode.prevNode.Map); });
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

        public void BTN_OnClick_ReturnToLobby()
        {
            SceneLoader.LoadLobbyScene();
        }
    }

}