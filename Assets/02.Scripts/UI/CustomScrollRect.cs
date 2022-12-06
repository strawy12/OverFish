using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static Constant;

public class CustomScrollRect : ScrollRect
{
    private Image scrollBarHandleImage;

    protected override void Awake()
    {
        base.Awake();
        scrollBarHandleImage = verticalScrollbar.handleRect.GetComponent<Image>();
        scrollBarHandleImage.color = new Color(1, 1, 1, 0);
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        EventManager.TriggerEvent(BEGIN_DRAG_PANELBUTTONS);
        base.OnBeginDrag(eventData);
        scrollBarHandleImage.DOKill(true);
        scrollBarHandleImage.DOFade(1f, 0.5f).SetEase(Ease.Linear);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        scrollBarHandleImage.DOKill(true);
        scrollBarHandleImage.DOFade(0f, 2f).SetEase(Ease.Linear);
    }
}
