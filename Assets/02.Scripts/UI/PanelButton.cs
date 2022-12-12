using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Constant;

public enum EPanelType
{
    None = -1,
    Tool,
    Upgrade,
    Fix,
}

public class PanelButton : HighlightButton, 
    IPointerDownHandler, IPointerUpHandler
{
    public static PanelButton selectedBtn = null;

    [SerializeField]
    private EPanelType panelType = EPanelType.None;


    [SerializeField]
    private Color selectColor;

    [SerializeField]
    private bool defaultSelectedBtn;

    private RectTransform rectTransform;
    private Image currentImage;
    private Color originColor;

    public EPanelType PanelType => panelType;
    public Action OnSelected;

    public bool IsSelected => selectedBtn == this;

    private bool isSelecting = false;

    private void Awake()
    {
        currentImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originColor = currentImage.color;
    }

    private IEnumerator Start()
    {
        // LateStart

        EventManager.StartListening(BEGIN_DRAG_PANELBUTTONS, DeSelectButton);
        yield return new WaitForEndOfFrame();

        if (defaultSelectedBtn)
        {
            if (selectedBtn != null)
            {
                Debug.LogError($"{selectedBtn.gameObject.name}와 {gameObject.name}의 defaultSelectedBtn가 켜져있습니다.\n하나만 켜주시길 바랍니다.");
                yield break;
            }
            SelectButton();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (IsSelected) return;
        SelectingButton();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (isSelecting == false) return;
        if (IsSelected) return;
        SelectButton();
    }

    public void DeSelectButton(object[] empty = null)
    {
        if (!isSelecting) return;

        currentImage.color = originColor;
        isSelecting = false;
    }

    public void SelectingButton()
    {
        if (isSelecting) return;
        isSelecting = true;

        currentImage.color = selectColor;
    }

    public void SelectButton(PanelButton btn = null)
    {
        btn ??= this;

        SelectingButton();

        if(selectedBtn != null)
            selectedBtn.currentImage.color = originColor;

        isSelecting = false;
        selectedBtn = this;
        OnSelected?.Invoke();
    }

}
