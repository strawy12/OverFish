using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float highlightScale;
    [SerializeField]
    private float highlightDuration;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(highlightScale, highlightDuration);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.localScale = Vector3.one;
    }

}
