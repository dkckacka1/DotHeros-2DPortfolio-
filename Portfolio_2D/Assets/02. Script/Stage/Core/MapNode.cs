using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] RectTransform nodeLineParent;
        [SerializeField] GameObject NodeLinePrefab;

        private Map nodeMap;

        private void Awake()
        {
            //if (!GameManager.Instance.TryGetMap(nodeMapID, out nodeMap))
            //{
            //    Debug.Log("Map ID is Unvaild");
            //}
        }

        private void Start()
        {
            foreach (var nextNode in nextNodeList)
            {
                var nodeLine = Instantiate(NodeLinePrefab,this.transform.position,Quaternion.identity,nodeLineParent);
                (nodeLine.transform as RectTransform).sizeDelta = new Vector2(Vector2.Distance(this.transform.position, nextNode.transform.position), (nodeLine.transform as RectTransform).sizeDelta.y);
                nodeLine.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, nextNode.transform.position - this.transform.position));
                nextNode.SetPrevNode(this);
            }
        }

        public void SetPrevNode(MapNode node)
        {
            this.prevNode = node;
        }

        public void LoadNodeMap()
        {
            List<Unit> units = GameManager.CurrentUser.userUnitList.OrderByDescending(unit => unit.UnitCurrentLevel).ThenByDescending(unit => unit.UnitGrade).Take(5).ToList();

            SceneLoader.LoadBattleScene(units, nodeMap);
        }

        public Vector2 GetContentNormalizePosition()
        {
            Vector2 normalizePos = new Vector2();

            //Debug.Log((this.transform.parent.transform as RectTransform).rect);
            Debug.Log(this.transform.position);
            Debug.Log(this.transform.localPosition);
            normalizePos.x = this.transform.localPosition.x / (this.transform.parent.transform as RectTransform).rect.width;
            normalizePos.y = this.transform.localPosition.y / (this.transform.parent.transform as RectTransform).rect.height;

            return normalizePos;
        }

    }
}