using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelContent : MonoBehaviour
{
    public EPanelType panelType = EPanelType.None;
    private CanvasGroup _canvasGroup;
    public CanvasGroup canvasGroup
    {
        get
        {
            if(_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
            return _canvasGroup;
        }
    }

    private RectTransform _rectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = transform as RectTransform;
            }
            return _rectTransform;
        }
    }
}
