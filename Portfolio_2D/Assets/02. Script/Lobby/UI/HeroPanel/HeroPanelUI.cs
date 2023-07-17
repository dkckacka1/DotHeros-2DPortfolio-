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
        private static eEquipmentItemType selectEquipmentItemType = eEquipmentItemType.Weapon;
        public static eEquipmentItemType SelectEquipmentItemType
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
        private static eUnitSkillType selectSkillType;
        public static eUnitSkillType SelectSkillType
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
            unitCompositionToggle.isOn = false;
            unitCompositionToggle.onValueChanged?.Invoke(false);
            unitStatusToggle.isOn = true;
            unitStatusToggle.onValueChanged?.Invoke(true);
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
        public void TOGGLE_OnClick_ResetPanel()
        {

            if (!unitEquipmentUI.gameObject.activeInHierarchy || !unitSkillPanelUI.gameObject.activeInHierarchy)
            {
                if (LobbyManager.UIManager.UndoCount() >= 2)
                {
                    LobbyManager.UIManager.Undo();
                }
            }
        }

        // �������ͽ� �г� â�� �����ش�.
        public void BTN_OnClick_ShowStatus()
        {
            unitListUI.SetStatus();
            SelectUnit = GameManager.CurrentUser.UserUnitList[0];
        }

        // ���� �ռ�â�� �����ش�.
        public void BTN_OnClick_ShowComposition()
        {
            unitListUI.SetComposition();
        }

        // ���� ���â�� �����ش�.
        public void BTN_OnClick_ShowEquipmentUI()
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
        public void BTN_OnClick_ShowSkillUI()
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
        public void BTN_OnClick_ShowReinforcePopup()
        {
            reinforcePopupUI.gameObject.SetActive(true);
            equipmentListPopupUI.gameObject.SetActive(false);
        }

        // ��� ����Ʈ �˾�â�� �����ش�.
        public void BTN_OnClick_ShowEquipmentListPopup()
        {
            equipmentListPopupUI.gameObject.SetActive(true);
            reinforcePopupUI.gameObject.SetActive(false);
        }

        // ��ų ������ �˾�â�� �����ش�.
        public void BTN_OnClick_ShowSkillLevelUPPopup()
        {
            skillLevelUpPopupUI.gameObject.SetActive(true);
            skillLevelUpPopupUI.Show();
        }


        // ������ ��� ��ȭ�Ѵ�.
        public void BTN_OnClick_Reinforce()
        {
            // ������ ��带 ��ȭ ��븸ŭ ���ҽ�Ų��.
            GameManager.CurrentUser.Gold -= Constant.ReinforceConsumeGoldValues[selectEquipmentItem.reinforceCount];

            if (Random.Range(0f, 1f) <= Constant.ReinforceProbabilitys[selectEquipmentItem.reinforceCount])
            // ��ȭ ���� ���θ� Ȯ���ؼ� ������ 
            {
                // ������ ��� ��ȭ�Ѵ�.
                GameManager.AudioManager.PlaySoundOneShot("Sound_ReinforceSuccess");
                GameManager.ItemCreator.ReinforceEquipment(selectEquipmentItem);
            }
            else
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_ReinforceFailed");
            }

            // UI�� ������Ʈ�ϰ� ���� ������ �����մϴ�.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // ��� �����Ѵ�.
        public void BTN_OnClick_ReleaseEquipment()
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
                // UI�� ������Ʈ�ϰ� ���� ������ �����մϴ�.
                ReShow();
                GameManager.Instance.SaveUser();
            }
        }

        // ��� ��ü�մϴ�.
        public void BTN_OnClick_ChangeEquipment()
        {
            // ���� ��� �κ��丮���� ������ ��� �����մϴ�.
            GameManager.CurrentUser.GetInventoryEquipmentItem.Remove(choiceEquipmentItem);
            // ������ ������ �ٲٰ��� �ϴ� ��� Ÿ���� �������� �κ��丮���� ������ ���������� ��ü�մϴ�.
            var releaseItem = selectUnit.ChangeEquipment(selectEquipmentItemType, choiceEquipmentItem);
            if (releaseItem != null)
            // ������ �������� �������� �־��ٸ�
            {
                // �κ��丮�� �־��ݴϴ�.
                GameManager.CurrentUser.AddEquipmentItem(selectEquipmentItem);
            }

            // ��ü�� �������� ���� ������ ������ ���������� �������ݴϴ�.
            selectEquipmentItem = choiceEquipmentItem;
            // ������ �������� �ʱ�ȭ
            choiceEquipmentItem = null;
            equipmentListPopupUI.ChoiceItemReset();
            // UI�� ������Ʈ�ϰ� ���� ������ �����մϴ�.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // ����ġ�� ȹ���մϴ�.
        public void BTN_OnClick_GetExperience(float getValue)
        {
            if(!selectUnit.IsMaxLevel && selectUnit.CurrentExperience + getValue >= selectUnit.MaxExperience)
                // ������ �ִ� ������ �ƴϰ�, ȹ�� ����ġ�� ���� ����ġ�� ���� �ִ� ����ġ�� �Ѿ ���
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_HeroPanelUnitLevelup");
            }
            else 
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_GetExperience");
            }

            // ������ ������ ����ġ�� ���� ��ŵ�ϴ�.
            selectUnit.CurrentExperience += getValue;
            // UI�� ������Ʈ�ϰ� ���� ������ �����մϴ�.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // ��ų�� �������մϴ�.
        public void BTN_OnClick_SkillLevelUp()
        {
            GameManager.AudioManager.PlaySoundOneShot("Sound_SkillLevelUp");
            skillLevelUpPopupUI.SkillLevelUP();
            // UI�� ������Ʈ�ϰ� ���� ������ �����մϴ�.
            ReShow();
            GameManager.Instance.SaveUser();
        }

        // ������ ���� ������ �ִ밹���� �ø��ϴ�.
        public void BTN_OnClick_SizeUPUintList()
        {
            if (GameManager.CurrentUser.MaxUnitListCount >= Constant.UnitListMaxSizeCount)
            // �̹� �ִ� ����� ������ ���¶�� ���â�� ǥ���մϴ�.
            {
                GameManager.UIManager.ShowAlert("�̹� �ִ� ����� �����߽��ϴ�!");
            }
            else
            // �ִ� ����� �ƴ϶�� Ȯ�� ���̾�α� â�� ǥ���մϴ�.
            {
                int consumeDia = Constant.UnitListSizeUPDiaConsumeValue;
                GameManager.UIManager.ShowConfirmation("�ִ� ���� ���� ����", $"�ִ� ���� ������ ���� ��Ű�ڽ��ϱ�?\n{consumeDia} ���̾ư� �Һ�Ǹ�\n{Constant.UnitListSizeUpCount}ĭ�� �þ�ϴ�.\n(�ִ� 100ĭ)", SizeUp);
            }
        }

        // ������ ���� ������ �ִ� ������ �ø��ϴ�.
        private void SizeUp()
        {
            if (GameManager.CurrentUser.CanDIamondConsume(Constant.UnitListSizeUPDiaConsumeValue))
            // ���� ���� ������ �ø� ���̾� ���� ����ϴٸ� 
            {
                GameManager.AudioManager.PlaySoundOneShot("Sound_ListSizeUP");
                //���̾ƾ��� �Һ��ϰ� �ִ� ������ ���� ��ŵ�ϴ�.
                GameManager.CurrentUser.Diamond -= Constant.UnitListSizeUPDiaConsumeValue;
                GameManager.CurrentUser.MaxUnitListCount += Constant.UnitListSizeUpCount;
                // ���ֽ��� �ؽ�Ʈ�� ������Ʈ �մϴ�.
                unitListUI.ShowUnitListCountText();
            }
            else
            // ���̾ƾ��� ������ϴٸ�
            {
                // ���â ǥ��
                GameManager.UIManager.ShowAlert("���̾Ƹ�尡 �����մϴ�!");
            }
        }
    }
}
