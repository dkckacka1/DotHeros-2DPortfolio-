using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 월드맵의 맵노드를 표시하는 클래스
 */

namespace Portfolio.WorldMap
{
    public class MapNode : MonoBehaviour
    {
        [SerializeField] int nodeMapID = 500;                       // 맵노드와 연결된 맵 ID
        public MapNode prevNode;                                    // 이전 맵 노드
        public List<MapNode> nextNodeList = new List<MapNode>();    // 다음 맵노드들

        [Header("NodeUI")]
        [SerializeField] Button nodeBtn;                // 맵 노드 버튼
        [SerializeField] TextMeshProUGUI mapNameText;   // 맵이름 텍스트
        [SerializeField] Image RockImage;               // 잠금 이미지
        [SerializeField] GameObject nodeArrowParent;    // 맵 화살표의 부모 오브젝트
        [SerializeField] bool isDefaultMap = false;     // 첫번째 맵 노드를 표시

        private Map map;    // 맵 노드와 연결된 맵
        public Map Map { get => map; }
        // 다음 맵이 있는가
        public bool HasNextMap => nextNodeList.Count != 0;
        // 이전 맵이 있는가
        public bool HasPrevMap => prevNode != null;
        // 바로 다음 맵 노드 리턴
        public MapNode GetNextMapNode => HasNextMap ? nextNodeList[0] : null;
        // 이전 노드 리턴
        public MapNode GetPrevMapNode => HasPrevMap ? prevNode : null;

        public GameObject NodeArrowParent { get => nodeArrowParent;}

        // 맵 노드 버튼 상호작용 활성화 여부를 결정한다.
        public void SetNodeBtnInteractable(bool isActive) => nodeBtn.interactable = isActive;
        // 잠금 이미지를 표시할것인지 결정한다.
        public void ShowLockImage(bool isActive) => RockImage.gameObject.SetActive(isActive);
        // 맵 노드 화살표를 표시할것인지 결정한다.
        public void ShowNodeArrow(bool isActive) => nodeArrowParent.SetActive(isActive);

        private void Awake()
        {
            // 게임 매니저에서 맵 정보를 가져와 맵 노드와 연동합니다.
            if (GameManager.Instance.TryGetMap(nodeMapID, out map))
            {
                mapNameText.text = map.MapName;
            }
            else
            {
                Debug.LogError($"{nodeMapID}의 맵은 없습니다.");
            }
        }

        // 이전 노드를 설정합니다.
        public void SetPrevNode(MapNode node)
        {
            this.prevNode = node;
        }


        // 유저가 선택한 맵 노드를 매니저 클래스에게을 넘겨줍니다.
        public void BTN_OnClick_SelectMapNode()
        {
            WorldMapManager.Instance.CurrentUserChoiceNode = this;
        }

        // 맵 정보를 표시합니다.
        public void BTN_OnClick_ShowMapInfoUI(MapInfoUI mapInfoUI)
        {
            mapInfoUI.gameObject.SetActive(true);
            mapInfoUI.ShowMapInfo(map);
        }

    }
}