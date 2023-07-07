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
        [SerializeField] PaymentType paymentType = PaymentType.Gold;    // ��ǰ�� �����ϱ� ���� ���� ���� ����
        [SerializeField] int paymentValue = 100;                        // ��ǰ�� �����ϱ� ���� �ݾ�

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
                    case PaymentType.Cash:
                        // ���� ����
                        return true;
                    case PaymentType.Diamond:
                        // ���̾Ʒ� ����
                        return GameManager.CurrentUser.Diamond >= paymentValue;
                    case PaymentType.Gold:
                        // ���� ����
                        return GameManager.CurrentUser.Gold >= paymentValue;
                }

                return false;
            }
        }

        protected virtual void Start()
        {
            // ���� ������ ���� ������ �ؽ�Ʈ �� �̹����� �����ش�.
            cashText.gameObject.SetActive(paymentType == PaymentType.Cash);
            resourceImage.gameObject.SetActive(paymentType != PaymentType.Cash);
            resourceText.gameObject.SetActive(paymentType != PaymentType.Cash);

            switch (paymentType)
            {
                case PaymentType.Cash:
                    // ���� ����
                    cashText.text = $"{paymentValue} \\";
                    break;
                case PaymentType.Diamond:
                    // ���̾Ʒ� ����
                    resourceImage.sprite = diaSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                case PaymentType.Gold:
                    // ���� ����
                    resourceImage.sprite = goldSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
            }
        }

        // ��ǰ�� �����մϴ�.
        public void BTN_OnClick_BuyProduct()
        {
            if (HasPaymentValue)
            // ����� ���� ������ �ִ� ���
            {
                if (paymentType == PaymentType.Cash || paymentType == PaymentType.Diamond)
                // ����ϴ� ��ȭ�� ���� �Ǵ� ���̾��� ���
                {
                    // Ȯ�� ���̾�α� â�� ǥ���մϴ�.
                    GameManager.UIManager.ShowConfirmation("��ȭ ����", "������ �����Ͻðڽ��ϱ�?", () =>
                    {
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
                case PaymentType.Cash:
                    break;
                case PaymentType.Diamond:
                    GameManager.CurrentUser.Diamond -= paymentValue;
                    break;
                case PaymentType.Gold:
                    GameManager.CurrentUser.Gold -= paymentValue;
                    break;
            }
        }

        // ��ǰ�� ����ϴ�.
        protected abstract void GetProduct();
    }
}