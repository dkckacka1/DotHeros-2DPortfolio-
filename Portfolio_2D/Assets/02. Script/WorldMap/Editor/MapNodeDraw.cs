using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
// TODO : �ּ�
/*
 * �����ͻ󿡼� ����ʾ��� �� ��带 �̾��ִ� ���ΰ� ȭ��ǥ�� �ڵ������� ������ִ� ������ Ŭ����
 */

namespace Portfolio.WorldMap
{
    // ����� �Ŵ��� ��ũ��Ʈ �ν�����â�� �����ϱ� ���� Ŭ����
    [CustomEditor(typeof(WorldMapManager))]
    public class MapNodeDraw : UnityEditor.Editor
    {
        WorldMapManager worldMapManager;    // ����� �Ŵ���
        WorldMapUIManager UIManager;        // ����ʾ� UI �Ŵ���
        RectTransform nodeLinePrefab;       // ��� ���� ������
        RectTransform nodeLineParent;       // ��� ������ �θ� ������Ʈ
        RectTransform nodeArrowPrefab;      // ��� ȭ��ǥ ������

        // ORDER : Ʈ�� ������ ���� �� ��� Ʈ���� DFS ��͸� �̿��ؼ� ���� �ʳ�� ���� ��� ���� (������ ��忡�� ��ư�ϳ��� �����ΰ� ��� �ַο츦 �׷��ְ� ��ư ������ ���� ������ִ� �ڵ�ȭ)
        // ���Ӿ� resolution�� 1920 * 1080 �̿����� ����� �����Ѵ�.
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // ������ ������Ʈ�� WorldMapManager ����
            worldMapManager = (WorldMapManager)target;
            UIManager = worldMapManager.GetComponentInChildren<WorldMapUIManager>();
            // ���� �� ����� ���� �θ� ������Ʈ, ���� ������, �� ��� ȭ��ǥ �θ� ������Ʈ�� �����մϴ�.
            nodeLinePrefab = UIManager.NodeLinePrefab;
            nodeLineParent = UIManager.NodeLineParent;
            nodeArrowPrefab = UIManager.NodeArrowPrefab;

            GUILayout.Space(25f);
            if (GUILayout.Button("�� �׸���"))
            {
                ClearLineAndArrow(worldMapManager.rootNode);
                DrawLineAndArrow(worldMapManager.rootNode);
            }

            GUILayout.Space(10f);
            if (GUILayout.Button("�� �ʱ�ȭ �ϱ�"))
            {
                ClearLineAndArrow(worldMapManager.rootNode);
            }
            // �� ��峢�� �̾� �ݴϴ�
        }

        // �� ������ ��ȸ�ϸ鼭 ��� ���ΰ� ��� �ַο츦 �׷��ݴϴ�.
        // ��� �ַο쿡�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        private void DrawLineAndArrow(MapNode currentMapNode)
        {
            foreach (var nextNode in currentMapNode.nextNodeList)
            // ���� �ʳ���� ���� ������ ��ȸ�մϴ�.
            {
                // �ʳ�� ������ �����մϴ�.
                RectTransform nodeLine = Instantiate(nodeLinePrefab, currentMapNode.transform.position, Quaternion.identity, nodeLineParent);
                // �� ��� ������ ���̴� ���� �� ���� ���� �� ����� ���� �����Դϴ�.
                float lineDistance = Vector2.Distance(currentMapNode.transform.position, nextNode.transform.position);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(lineDistance, (nodeLine.transform as RectTransform).sizeDelta.y);
                // �� ��� ������ �� �ʳ�带 �̾��ֵ��� ȸ�� ���� �ݴϴ�.
                Vector2 direction = nextNode.transform.position - currentMapNode.transform.position;
                var angle = Vector2.SignedAngle(Vector2.right, direction);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, angle);

                // �� ��� ȭ��ǥ�� �����մϴ�.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                // ȭ��ǥ�� ���� �� ��带 �ٶ󺸵��� ȸ�� ��ŵ�ϴ�.
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                // ȭ��ǥ�� ������ ������ ������ �ʳ�带 �����ϰ� ȭ�� ����� �̵���Ű�� �Լ������ʸ�
                // ��ư ��Ŭ�� �̺�Ʈ�� ����մϴ�.
                // ������ ��ũ��Ʈ���� �̺�Ʈ�� ����Ϸ��� AddPersistentListener �Լ� ���� �̿��ؾ��Ѵ�.
                UnityEditor.Events.UnityEventTools.AddObjectPersistentListener<MapNode>(
                    nodeArrow.GetComponentInChildren<Button>().onClick,
                    worldMapManager.BTN_OnClick_ChoiceNode, 
                    nextNode
                    );

                // �� ���� �� ���� ����� ���� ��尡 �˴ϴ�.
                nextNode.SetPrevNode(currentMapNode);
                // ���� ��嵵 ��� ������ �����մϴ�. DFS ���
                DrawLineAndArrow(nextNode);
            }

            if (currentMapNode.prevNode != null)
            // �� ��尡 ���� ��尡 �����Ѵٸ�
            {
                // �� ��� ȭ��ǥ�� �����մϴ�.
                RectTransform nodeArrow = Instantiate(nodeArrowPrefab, currentMapNode.transform.position, Quaternion.identity, currentMapNode.NodeArrowParent.transform);
                var arrowAngle = Mathf.Atan2(currentMapNode.prevNode.transform.position.y - currentMapNode.transform.position.y, currentMapNode.prevNode.transform.position.x - currentMapNode.transform.position.x) * Mathf.Rad2Deg;
                nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
                // ȭ��ǥ�� ������ ������ ������ �ʳ�带 �����ϰ� ȭ�� ����� �̵���Ű�� �Լ������ʸ�
                // ��ư ��Ŭ�� �̺�Ʈ�� ����մϴ�.
                UnityEditor.Events.UnityEventTools.AddObjectPersistentListener<MapNode>(
                    nodeArrow.GetComponentInChildren<Button>().onClick,
                    worldMapManager.BTN_OnClick_ChoiceNode,
                    currentMapNode.prevNode
                    );

            }
        }

        // �� ����� ������ ���� �����ϸ鼭 �� ����� ȭ��ǥ�� �����մϴ�.
        // ������ ��忡�� ���ӿ�����Ʈ�� �����Ϸ��� Destory ��� DestroyImmediate�� ����ؾ��Ѵ�.
        private void ClearLineAndArrow(MapNode currentMapNode)
        {
            foreach(var nodeLine in nodeLineParent)
            {
                DestroyImmediate((nodeLine as Transform).gameObject);
            }

            ClearArrow(currentMapNode);
        }

        // �� ������ ��ȸ�ϸ鼭 �� ����� ȭ��ǥ�� �����մϴ�.
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