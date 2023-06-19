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
        [SerializeField] bool isDefaultMap = false;

        private Map map;
        public Map Map { get => map; }
        public bool HasNextMap => nextNodeList.Count != 0;
        public bool HasPrevMap => prevNode != null;
        public MapNode GetNextMapNode => HasNextMap ? nextNodeList[0] : null;
        public MapNode GetPrevMapNode => HasPrevMap ? prevNode : null;

        public GameObject NodeArrowParent { get => nodeArrowParent;}

        public void SetNodeBtnInteractable(bool isActive) => nodeBtn.interactable = isActive || isDefaultMap;
        public void ShowLockImage(bool isActive) => RockImage.gameObject.SetActive(isActive && !isDefaultMap);
        public void ShowNodeArrow(bool isActive) => nodeArrowParent.SetActive(isActive && !isDefaultMap);

        private void Awake()
        {
            if (GameManager.Instance.TryGetMap(nodeMapID, out map))
            {
                mapNameText.text = map.MapName;
            }
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