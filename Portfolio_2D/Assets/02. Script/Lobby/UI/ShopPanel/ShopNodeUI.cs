using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ������ ���� ��ǰ�� ǥ���ϴ� UI Ŭ����
 */

namespace Portfolio.Lobby.Shop
{
    public abstract class ShopNodeUI : MonoBehaviour
    {
        [SerializeField] private ePaymentType paymentType = ePaymentType.Gold;    // ��ǰ�� �����ϱ� ���� ���� ���� ����
        [SerializeField] protected int paymentValue = 100;                        // ��ǰ�� �����ϱ� ���� �ݾ�

        [Header("Cash")]
        [SerializeField] TextMeshProUGUI cashText;      // ���� �Һ� �ؽ�Ʈ

        [Header("Resource")]
        [SerializeField] Image resourceImage;           // ��ȭ �Һ� ���� �̹���
        [SerializeField] TextMeshProUGUI resourceText;  // ��ȭ �Һ�� �ؽ�Ʈ
        [SerializeField] Sprite diaSprite;              // ���̾� ��������Ʈ
        [SerializeField] Sprite goldSprite;             // ��� ��������Ʈ

        // �����ϱ⿡ ����� ���� ������ �ִ°�?
        public bool HasPaymentValue
        {
            get
            {
                switch (paymentType)
                {
                    case ePaymentType.Cash:
                        // ���� ����
                        return true;
                    case ePaymentType.Diamond:
                        // ���̾Ʒ� ����
                        return GameManager.CurrentUser.Diamond >= paymentValue;
                    case ePaymentType.Gold:
                        // ���� ����
                        return GameManager.CurrentUser.Gold >= paymentValue;
                    default:
                        Debug.LogWarning("unknownType");
                        break;
                }

                return false;
            }
        }

        public ePaymentType PaymentType 
        {
            get => paymentType; 
            protected set => paymentType = value;
        }

        protected virtual void Start()
        {
            ShowPayment();
        }

        // ���� ������ ���� ������ �ؽ�Ʈ �� �̹����� �����ش�.
        protected void ShowPayment()
        {
            cashText.gameObject.SetActive(paymentType == ePaymentType.Cash);
            resourceImage.gameObject.SetActive(paymentType != ePaymentType.Cash);
            resourceText.gameObject.SetActive(paymentType != ePaymentType.Cash);

            switch (paymentType)
            {
                case ePaymentType.Cash:
                    // ���� ����
                    cashText.text = $"{paymentValue} \\";
                    break;
                case ePaymentType.Diamond:
                    // ���̾Ʒ� ����
                    resourceImage.sprite = diaSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                case ePaymentType.Gold:
                    // ���� ����
                    resourceImage.sprite = goldSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }

        // ��ǰ�� �����մϴ�.
        public void BTN_OnClick_BuyProduct()
        {
            if (HasPaymentValue)
            // ����� ���� ������ �ִ� ���
            {
                if (paymentType == ePaymentType.Cash || paymentType == ePaymentType.Diamond)
                // ����ϴ� ��ȭ�� ���� �Ǵ� ���̾��� ���
                {
                    // Ȯ�� ���̾�α� â�� ǥ���մϴ�.
                    GameManager.UIManager.ShowConfirmation("��ȭ ����", "������ �����Ͻðڽ��ϱ�?", () =>
                    {
                        // SOUND : ���� ���� ���
                        // ��ȭ�� �����մϴ�.
                        PayProduct();
                        // �ش� ��ǰ�� ����ϴ�.
                        GetProduct();
                        // ���� ������ �����մϴ�.
                        GameManager.Instance.SaveUser();
                    });
                }
                else
                // ����ϴ� ��ȭ�� ����� ���
                {
                    // SOUND : ���� ���� ���
                    // ��ȭ�� �����մϴ�.
                    PayProduct();
                    // �ش� ��ǰ�� ����ϴ�.
                    GetProduct();
                    // ���� ������ �����մϴ�.
                    GameManager.Instance.SaveUser();
                }
            }
            else
            // ���ٸ� ���â�� ǥ���Ѵ�.
            {
                GameManager.UIManager.ShowAlert("��ȭ�� �����մϴ�.");
            }
        }

        // ��ȭ�� �����մϴ�.
        private void PayProduct()
        {
            // ��ȭ�� �°� ��ġ�� �����մϴ�.
            switch (paymentType)
            {
                case ePaymentType.Cash:
                    break;
                case ePaymentType.Diamond:
                    GameManager.CurrentUser.Diamond -= paymentValue;
                    break;
                case ePaymentType.Gold:
                    GameManager.CurrentUser.Gold -= paymentValue;
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }

        // ��ǰ�� ����ϴ�.
        protected abstract void GetProduct();
    }
}