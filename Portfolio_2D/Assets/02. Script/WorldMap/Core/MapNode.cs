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
        [SerializeField] Image RockImage;
        [SerializeField] GameObject nodeArrowParent;

        private Map map;
        public Map Map { get => map; }
        public bool HasNextMap => nextNodeList.Count != 0;
        public bool HasPrevMap => prevNode != null;
        public MapNode GetNextMapNode => HasNextMap ? nextNodeList[0] : null;
        public MapNode GetPrevMapNode => HasPrevMap ? prevNode : null;

        public GameObject NodeArrowParent { get => nodeArrowParent;}

        public void SetNodeBtnInteractable(bool isActive) => nodeBtn.interactable = isActive;
        public void ShowLockImage(bool isActive) => RockImage.gameObject.SetActive(isActive);
        public void ShowNodeArrow(bool isActive) => nodeArrowParent.SetActive(isActive);

        private void Awake()
        {
            if (GameManager.Instance.TryGetMap(nodeMapID, out map))
            {
                mapNameText.text = map.MapData.mapName;
            }
        }

        private void Start()
        {
            //foreach (var nextNode in nextNodeList)
            //{
            //    // NodeLine 持失
            //    RectTransform nodeLinePrefab = WorldMapManager.WorldMapUIManager.NodeLinePrefab;
            //    RectTransform nodeLineParent = WorldMapManager.WorldMapUIManager.NodeLineParent;
            //    RectTransform nodeLine = Instantiate(nodeLinePrefab, this.transform.position, Quaternion.identity, nodeLineParent);
            //    (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(this.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
            //    nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - this.transform.position));

            //    // NodeArrow 持失
            //    RectTransform nodeArrowPrefab = WorldMapManager.WorldMapUIManager.NodeArrowPrefab;
            //    RectTransform nodeArrow = Instantiate(nodeArrowPrefab, this.transform.position, Quaternion.identity, nodeArrowParent.transform);
            //    var arrowAngle = Mathf.Atan2(nextNode.transform.position.y - transform.position.y, nextNode.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            //    nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
            //    nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { WorldMapManager.Instance.CurrentUserChoiceNode = nextNode; });

            //    nextNode.SetPrevNode(this);
            //}

            //if (prevNode != null)
            //{
            //    RectTransform nodeArrowPrefab = WorldMapManager.WorldMapUIManager.NodeArrowPrefab;
            //    RectTransform nodeArrow = Instantiate(nodeArrowPrefab, this.transform.position, Quaternion.identity, nodeArrowParent.transform);
            //    var arrowAngle = Mathf.Atan2(prevNode.transform.position.y - transform.position.y, prevNode.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            //    nodeArrow.transform.rotation = Quaternion.Euler(0, 0, arrowAngle - 90);
            //    nodeArrow.GetComponentInChildren<Button>().onClick.AddListener(() => { WorldMapManager.Instance.CurrentUserChoiceNode = prevNode; });
            //}
        }

        public void SetPrevNode(MapNode node)
        {
            this.prevNode = node;
        }


        public void BTN_ONCLICK_SetMapNode()
        {
            WorldMapManager.Instance.CurrentUserChoiceNode = this;
        }

        public void BTN_ONCLICK_ShowMapInfoUI(MapInfoUI mapInfoUI)
        {
            mapInfoUI.gameObject.SetActive(true);
            mapInfoUI.ShowMapInfo(map);
        }

    }
}