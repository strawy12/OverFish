using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionUI : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;

    [SerializeField]
    private Image _delayImage;

    private RectTransform _rectTransform;

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();

        _delayImage.fillAmount = 0f;

    }

    public void SetIconSprite(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetPos(Vector3 targetPos)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(targetPos);

        pos.x -= Constant.MAX_SCREEN_SIZE.x * 0.5f;
        pos.y -= Constant.MAX_SCREEN_SIZE.y * 0.5f;
        pos.z = 0f;

        _rectTransform.anchoredPosition = pos;

        gameObject.SetActive(true);
    }

    public void SetDelayFill(float fillAmount)
    {
        _delayImage.fillAmount = fillAmount;
    }
}
