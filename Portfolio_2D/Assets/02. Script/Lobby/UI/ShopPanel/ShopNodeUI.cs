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
        [SerializeField] private ePaymentType paymentType = ePaymentType.Gold;    // 상품을 구매하기 위한 결제 수단 종류
        [SerializeField] protected int paymentValue = 100;                        // 상품을 구매하기 위한 금액

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
                    case ePaymentType.Cash:
                        // 현금 결제
                        return true;
                    case ePaymentType.Diamond:
                        // 다이아로 구매
                        return GameManager.CurrentUser.Diamond >= paymentValue;
                    case ePaymentType.Gold:
                        // 골드로 구매
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

        // 결제 종류에 따라 보여줄 텍스트 및 이미지를 정해준다.
        protected void ShowPayment()
        {
            cashText.gameObject.SetActive(paymentType == ePaymentType.Cash);
            resourceImage.gameObject.SetActive(paymentType != ePaymentType.Cash);
            resourceText.gameObject.SetActive(paymentType != ePaymentType.Cash);

            switch (paymentType)
            {
                case ePaymentType.Cash:
                    // 현금 결제
                    cashText.text = $"{paymentValue} \\";
                    break;
                case ePaymentType.Diamond:
                    // 다이아로 구매
                    resourceImage.sprite = diaSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                case ePaymentType.Gold:
                    // 골드로 구매
                    resourceImage.sprite = goldSprite;
                    resourceText.text = paymentValue.ToString();
                    break;
                default:
                    Debug.LogWarning("unknownType");
                    break;
            }
        }

        // 상품을 구매합니다.
        public void BTN_OnClick_BuyProduct()
        {
            if (HasPaymentValue)
            // 충분한 결제 수단이 있는 경우
            {
                if (paymentType == ePaymentType.Cash || paymentType == ePaymentType.Diamond)
                // 사용하는 재화가 현금 또는 다이아일 경우
                {
                    // 확인 다이얼로그 창을 표시합니다.
                    GameManager.UIManager.ShowConfirmation("재화 구매", "정말로 구매하시겠습니까?", () =>
                    {
                        // SOUND : 구매 사운드 재생
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
                    // SOUND : 구매 사운드 재생
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

        // 상품을 얻습니다.
        protected abstract void GetProduct();
    }
}