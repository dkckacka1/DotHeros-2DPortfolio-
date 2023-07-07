using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 상점의 구매 물품을 표시하는 UI 클래스
 */

namespace Portfolio.Lobby.Shop
{
    public abstract class ShopNodeUI : MonoBehaviour
    {
        [SerializeField] PaymentType paymentType = PaymentType.Gold;    // 상품을 구매하기 위한 결제 수단 종류
        [SerializeField] int paymentValue = 100;                        // 상품을 구매하기 위한 금액

        [Header("Cash")]
        [SerializeField] TextMeshProUGUI cashText;      // 현금 소비 텍스트

        [Header("Resource")]
        [SerializeField] Image resourceImage;           // 재화 소비 종류 이미지
        [SerializeField] TextMeshProUGUI resourceText;  // 재화 소비양 텍스트
        [SerializeField] Sprite diaSprite;              // 다이아 스프라이트
        [SerializeField] Sprite goldSprite;             // 골드 스프라이트

        // 구매하기에 충분한 결제 수단이 있는가?
        public bool HasPaymentValue
        {
            get
            {
                switch (paymentType)
                {
                    case PaymentType.Cash:
                        // 현금 결제
                        return true;
                    case PaymentType.Diamond:
                        // 다이아로 구매
                        return GameManager.CurrentUser.Diamond >= paymentValue;
                    case PaymentType.Gold:
                        // 골드로 구매
                        return GameManager.CurrentUser.Gold >= paymentValue;
                }

                return false;
            }
        }

        protected virtual void Start()
        {
            // 결제 종류에 따라 보여줄 텍스트 및 이미지를 정해준다.
            cashText.gameObject.SetActive(paymentType == PaymentType.Cash);
            resourceImage.gameObject.SetActive(paymentType != PaymentType.Cash);
            resourceText.gameObject.SetActive(paymentType != PaymentType.Cash);

            switch (paymentType)
            {
                case PaymentType.Cash:
                    // 현금 결제
                    cashText.text = $"{paymentValue} \\";
                    break;
                case PaymentType.Diamond:
                    // 다이아로 구매
                    resourceImage.sprite = diaSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                case PaymentType.Gold:
                    // 골드로 구매
                    resourceImage.sprite = goldSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
            }
        }

        // 상품을 구매합니다.
        public void BTN_OnClick_BuyProduct()
        {
            if (HasPaymentValue)
            // 충분한 결제 수단이 있는 경우
            {
                if (paymentType == PaymentType.Cash || paymentType == PaymentType.Diamond)
                // 사용하는 재화가 현금 또는 다이아일 경우
                {
                    // 확인 다이얼로그 창을 표시합니다.
                    GameManager.UIManager.ShowConfirmation("재화 구매", "정말로 구매하시겠습니까?", () =>
                    {
                        // 재화을 지불합니다.
                        PayProduct();
                        // 해당 상품을 얻습니다.
                        GetProduct();
                        // 유저 정보를 저장합니다.
                        GameManager.Instance.SaveUser();
                    });
                }
                else
                // 사용하는 재화가 골드일 경우
                {
                    // 재화을 지불합니다.
                    PayProduct();
                    // 해당 상품을 얻습니다.
                    GetProduct();
                    // 유저 정보를 저장합니다.
                    GameManager.Instance.SaveUser();
                }
            }
            else
            // 없다면 경고창을 표시한다.
            {
                GameManager.UIManager.ShowAlert("재화가 부족합니다.");
            }
        }

        // 재화를 지불합니다.
        private void PayProduct()
        {
            // 재화에 맞게 수치를 조절합니다.
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

        // 상품을 얻습니다.
        protected abstract void GetProduct();
    }
}