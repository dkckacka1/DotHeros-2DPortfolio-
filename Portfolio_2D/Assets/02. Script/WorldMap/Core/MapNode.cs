using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������� �ʳ�带 ǥ���ϴ� Ŭ����
 */

namespace Portfolio.WorldMap
{
    public class MapNode : MonoBehaviour
    {
        [SerializeField] int nodeMapID = 500;                       // �ʳ��� ����� �� ID
        public MapNode prevNode;                                    // ���� �� ���
        public List<MapNode> nextNodeList = new List<MapNode>();    // ���� �ʳ���

        [Header("NodeUI")]
        [SerializeField] Button nodeBtn;                // �� ��� ��ư
        [SerializeField] TextMeshProUGUI mapNameText;   // ���̸� �ؽ�Ʈ
        [SerializeField] Image RockImage;               // ��� �̹���
        [SerializeField] GameObject nodeArrowParent;    // �� ȭ��ǥ�� �θ� ������Ʈ
        [SerializeField] bool isDefaultMap = false;     // ù��° �� ��带 ǥ��

        private Map map;    // �� ���� ����� ��
        public Map Map { get => map; }
        // ���� ���� �ִ°�
        public bool HasNextMap => nextNodeList.Count != 0;
        // ���� ���� �ִ°�
        public bool HasPrevMap => prevNode != null;
        // �ٷ� ���� �� ��� ����
        public MapNode GetNextMapNode => HasNextMap ? nextNodeList[0] : null;
        // ���� ��� ����
        public MapNode GetPrevMapNode => HasPrevMap ? prevNode : null;

        public GameObject NodeArrowParent { get => nodeArrowParent;}

        // �� ��� ��ư ��ȣ�ۿ� Ȱ��ȭ ���θ� �����Ѵ�.
        public void SetNodeBtnInteractable(bool isActive) => nodeBtn.interactable = isActive;
        // ��� �̹����� ǥ���Ұ����� �����Ѵ�.
        public void ShowLockImage(bool isActive) => RockImage.gameObject.SetActive(isActive);
        // �� ��� ȭ��ǥ�� ǥ���Ұ����� �����Ѵ�.
        public void ShowNodeArrow(bool isActive) => nodeArrowParent.SetActive(isActive);

        private void Awake()
        {
            // ���� �Ŵ������� �� ������ ������ �� ���� �����մϴ�.
            if (GameManager.Instance.TryGetMap(nodeMapID, out map))
            {
                mapNameText.text = map.MapName;
            }
            else
            {
                Debug.LogError($"{nodeMapID}�� ���� �����ϴ�.");
            }
        }

        // ���� ��带 �����մϴ�.
        public void SetPrevNode(MapNode node)
        {
            this.prevNode = node;
        }


        // ������ ������ �� ��带 �Ŵ��� Ŭ���������� �Ѱ��ݴϴ�.
        public void BTN_OnClick_SelectMapNode()
        {
            WorldMapManager.Instance.CurrentUserChoiceNode = this;
        }

        // �� ������ ǥ���մϴ�.
        public void BTN_OnClick_ShowMapInfoUI(MapInfoUI mapInfoUI)
        {
            mapInfoUI.gameObject.SetActive(true);
            mapInfoUI.ShowMapInfo(map);
        }

    }
}