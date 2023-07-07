using Portfolio.Lobby.Hero.Composition;
using Portfolio.skill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����â�� �����ִ� UI Ŭ����
 */

namespace Portfolio.Lobby.Hero
{
    public class HeroPanelUI : PanelUI
    {
        [Header("Toggle")]
        [SerializeField] ToggleGroup PanelToggleGroup;      // ��� �׷�
        [SerializeField] Toggle unitStatusToggle;           // ���� ����â�� �����ִ� ���
        [SerializeField] Toggle unitCompositionToggle;      // ���� �ռ�â�� �����ִ� ���

        [Header("Panel")]
        [SerializeField] UnitListUI unitListUI;                             // ���� ����Ʈ UI �г�
        [SerializeField] UnitStatusUI unitStatusUI;                         // ���� ����â UI �г�
        [SerializeField] UnitEquipmentPanelUI unitEquipmentUI;              // ���� ���â UI �г�
        [SerializeField] UnitSkillPanelUI unitSkillPanelUI;                 // ���� ��ųâ UI �г�
        [SerializeField] UnitCompositionPanelUI unitCompositionPanelUI;     // ���� �ռ�â UI �г�

        [Header("UnitEquipmentPopup")]
        [SerializeField] EquipmentPopupUI equipmentPopupUI;         // ���â �˾� UI
        [SerializeField] EquipmentReinforcePopupUI reinforcePopupUI;// ��� ��ȭâ �˾� UI
        [SerializeField] EquipmentListPopupUI equipmentListPopupUI; // ��� ����Ʈ �˾� UI
        [SerializeField] EquipmentTooltip equipmentTooltipUI;       // ��� ���� UI

        [Header("UnitSkillLevelUPPopup")]
        [SerializeField] SkillLevelUpPopupUI skillLevelUpPopupUI;   // ���� ��ų ������ �˾� UI

        // ���� ������ ������ ����
        private static Unit selectUnit;
        public static Unit SelectUnit
        {
            get
            {
                return selectUnit;
            }
            set
            {
                // ���� ������ �ٲ���ٸ� ���� ���� �̺�Ʈ�� ȣ���Ѵ�.
                selectUnit = value;
                LobbyManager.UIManager.OnUnitChanged();
            }
        }
        // ���� ������ ������ ��� ������
        private static EquipmentItemData selectEquipmentItem;
        public static EquipmentItemData SelectEquipmentItem
        {
            get
            {
                return selectEquipmentItem;
            }
            set
            {
                // ���� ��� �������� �ٲ���ٸ� ���� ���� �̺�Ʈ�� ȣ���Ѵ�.
                selectEquipmentItem = value;
                LobbyManager.UIManager.OnEquipmentItemChanged();
            }
        }
        
        // ���� ������ ������ ��� Ÿ��
        private static EquipmentItemType selectEquipmentItemType = EquipmentItemType.Weapon;
        public static EquipmentItemType SelectEquipmentItemType
        {
            get
            {
                return selectEquipmentItemType;
            }
            set
            {
                selectEquipmentItemType = value;
            }
        }
        
        // ��� ����Ʈ �˾�â���� ������ ������ ���
        private static EquipmentItemData choiceEquipmentItem;
        public static EquipmentItemData ChoiceEquipmentItem
        {
            get
            {
                return choiceEquipmentItem;
            }
            set
            {
                choiceEquipmentItem = value;
                LobbyManager.UIManager.OnEquipmentItemChanged();
            }
        }

        // ���� ������ ������ ��ų
        private static Skill selectSkill;
        public static Skill SelectSkill
        {
            get
            {
                return selectSkill;
            }
            set
            {
                selectSkill = value;
            }
        }

        // ���� ������ ������ ��ų Ÿ��
        private static UnitSkillType selectSkillType;
        public static UnitSkillType SelectSkillType
        {
            get
            {
                return selectSkillType;
            }
            set
            {
                selectSkillType = value;
            }
        }

        // ���� ������ ������ ������ ��ų ������ �ҷ��´�.
        public static int SelectSkillLevel
        {
            get
            {
                return SelectUnit.GetSkillLevel(SelectSkillType);
            }
        }
        private void Awake()
        {
            // ��� �г�, �˾� UI�� �ʱ�ȭ ���ش�.
            unitListUI.Init();
            unitStatusUI.Init();
            unitEquipmentUI.Init();
            unitSkillPanelUI.Init();
            equipmentPopupUI.Init();
            reinforcePopupUI.Init();
            equipmentListPopupUI.Init();
        }

        private void Start()
        {
            // ó�� â�� ų ��� ������ ù��°������ �����ֵ��� �����.
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void OnDisable()
        {
            // �ʱ� ���·� �ǵ�����
            equipmentPopupUI.gameObject.SetActive(false);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            equipmentTooltipUI.gameObject.SetActive(false);
            skillLevelUpPopupUI.gameObject.SetActive(false);
            unitCompositionToggle.onValueChanged.Invoke(false);
            unitStatusToggle.onValueChanged.Invoke(true);
            unitStatusToggle.isOn = true;
            GameManager.Instance.SaveUser();
            
        }

        // ��� ����â�� UI�� ������Ʈ ���ش�.
        public void ReShow()
        {
            unitListUI.ShowUnitList();
            LobbyManager.UIManager.OnUnitChanged();
            LobbyManager.UIManager.OnEquipmentItemChanged();
        }

        //===========================================================
        // TogglePlugin
        //===========================================================
        public void ResetPanel()
        {
            
            if (!unitEquipmentUI.gameObject.activeInHierarchy || !unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }
            }
        }

        //===========================================================
        // ShowUI
        //===========================================================
        // TODO : �ּ� ���� �ް� ��ư �޼��� �̸� �������־����
        // �������ͽ� �г� â�� �����ش�.
        public void ShowStatus()
        {
            unitListUI.SetStatus();
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        // ���� �ռ�â�� �����ش�.
        public void ShowComposition()
        {
            unitListUI.SetComposition();
        }


        // ���� ���â�� �����ش�.
        public void ShowEquipmentUI()
        {
            // ��ųâ�� �����ִ� ���¶�� ��θ� �����Ѵ�.
            if (!unitEquipmentUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }

                unitEquipmentUI.gameObject.SetActive(true);
                LobbyManager.UIManager.AddUndo(unitEquipmentUI);
            }
        }

        // ���� ��ųâ�� �����ش�.
        public void ShowSkillUI()
        {
            // ���â�� �����ִ� ���¶�� ��θ� �����Ѵ�.
            if (!unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }

                unitSkillPanelUI.gameObject.SetActive(true);
                LobbyManager.UIManager.AddUndo(unitSkillPanelUI);
            }
        }

        // ��� �˾�â�� �����ش�.
        public void ShowEquipmentPopupUI()
        {
            equipmentPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
            equipmentListPopupUI.gameObject.SetActive(false);
            LobbyManager.UIManager.OnEquipmentItemChanged();
        }

        // ��� ��ȭ �˾�â�� �����ش�.
        public void ShowReinforcePopup()
        {
            reinforcePopupUI.gameObject.SetActive(true);
            equipmentListPopupUI.gameObject.SetActive(false);
        }

        // ��� ����Ʈ �˾�â�� �����ش�.
        public void ShowEquipmentListPopup()
        {
            equipmentListPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
        }

        // ��ų ������ �˾�â�� �����ش�.
        public void ShowSkillLevelUPPopup()
        {
            skillLevelUpPopupUI.gameObject.SetActive(true);
            skillLevelUpPopupUI.Show();
        }

        //===========================================================
        // BtnPlugin
        //===========================================================
        public void Reinforce()
        {
            GameManager.CurrentUser.Gold-= Constant.reinforceConsumeGoldValues[selectEquipmentItem.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.reinforceProbabilitys[selectEquipmentItem.reinforceCount])
            {
                GameManager.ItemCreator.ReinforceEquipment(selectEquipmentItem);
            }

            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void ReleaseEquipment()
        {
            if (GameManager.CurrentUser.IsMaxEquipmentCount)
                // ��� �κ��丮�� ���� ������ ���ٸ�
            {
                GameManager.UIManager.ShowAlert("��� ���� ������ �����ϴ�.\n��� �κ��丮���� ��� �����ּ���.");
            }
            else
                // ��� �κ��丮�� ���� ������ �ִٸ�
            {
                var releaseItem = selectUnit.ReleaseEquipment(selectEquipmentItemType);
                GameManager.CurrentUser.AddEquipmentItem(releaseItem);
                selectEquipmentItem = null;
                choiceEquipmentItem = null;

                equipmentListPopupUI.ChoiceItemReset();
                ReShow();
                GameManager.Instance.SaveUser();
            }
        }

        public void ChangeEquipment()
        {
            GameManager.CurrentUser.GetInventoryEquipmentItem.Remove(choiceEquipmentItem);
            var releaseItem = selectUnit.ChangeEquipment(selectEquipmentItemType, choiceEquipmentItem);
            if (releaseItem != null)
            {
                GameManager.CurrentUser.AddEquipmentItem(selectEquipmentItem);
            }

            selectEquipmentItem = choiceEquipmentItem;
            choiceEquipmentItem = null;
            equipmentListPopupUI.ChoiceItemReset();
            ReShow();
            GameManager.Instance.SaveUser();
        }

        public void GetExperience(float getValue)
        {
            selectUnit.CurrentExperience += getValue;
            ReShow();
        }

        public void SkillLevelUp()
        {
            skillLevelUpPopupUI.SkillLevelUP();
            ReShow();
        }

        public void SizeUPUintList()
        {
            Debug.Log("������� ��ư ����");
            if (GameManager.CurrentUser.MaxUnitListCount >= Constant.unitListMaxSizeCount)
            {
                GameManager.UIManager.ShowAlert("�̹� �ִ� ����� �����߽��ϴ�!");
            }
            else
            {
                int consumeDia = Constant.unitListSizeUPDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("�ִ� ���� ���� ����", $"�ִ� ���� ������ ���� ��Ű�ڽ��ϱ�?\n{consumeDia} ���̾ư� �Һ�Ǹ�\n{Constant.unitListSizeUpCount}ĭ�� �þ�ϴ�.\n(�ִ� 100ĭ)", SizeUp);
                Debug.Log("������� ��ư Ȯ�� ����");
            }
        }

        private void SizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.unitListSizeUPDiaConsumeValue))
            {
                GameManager.CurrentUser.Diamond -= Constant.unitListSizeUPDiaConsumeValue;
                GameManager.CurrentUser.MaxUnitListCount += Constant.unitListSizeUpCount;
                unitListUI.ShowUnitListCountText();
                Debug.Log("������");
            }
            else
            {
                GameManager.UIManager.ShowAlert("���̾Ƹ�尡 �����մϴ�!");
            }
        }
    }
}
