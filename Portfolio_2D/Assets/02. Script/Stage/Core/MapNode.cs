using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Portfolio.WorldMap
{
    public class MapNode : MonoBehaviour
    {
        [SerializeField] int nodeMapID = 500;
        public MapNode prevNode;
        public List<MapNode> nextNodeList = new List<MapNode>();
        [Header("NodeUI")]
        [SerializeField] Button nodeBtn;
        [SerializeField] TextMeshProUGUI mapNameText;
        [SerializeField] RectTransform nodeLineParent;
        [SerializeField] GameObject NodeLinePrefab;

        private Map map;
        public Map Map { get => map; }
        public bool HasNextMap => nextNodeList.Count != 0;
        public bool HasPrevMap => prevNode != null;
        public MapNode GetNextMapNode => HasNextMap ? nextNodeList[0] : null;
        public MapNode GetPrevMapNode => HasPrevMap ? prevNode : null;

        private void Awake()
        {
            if (GameManager.Instance.TryGetMap(nodeMapID, out map))
            {
                mapNameText.text = map.MapData.mapName;
            }
        }

        private void Start()
        {
            foreach (var nextNode in nextNodeList)
            {
                var nodeLine = Instantiate(NodeLinePrefab, this.transform.position, Quaternion.identity, nodeLineParent);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(this.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - this.transform.position));
                nextNode.SetPrevNode(this);
            }
        }

        public void SetPrevNode(MapNode node)
        {
            this.prevNode = node;
        }

        public void BTN_ONCLICK_ShowMapInfoUI(MapInfoUI mapInfoUI)
        {
            mapInfoUI.gameObject.SetActive(true);
            mapInfoUI.ShowMapInfo(map);
        }

    }
}