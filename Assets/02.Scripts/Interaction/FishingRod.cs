using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : InteractionObject
{
    [Header("FishingRod Data")]

    [SerializeField]
    private Sprite _fishIcon;

    [SerializeField]
    private Sprite _lureIcon;

    private bool _hasFish;

    protected override void BindInterationUI()
    {
        _hasFish = false;
        _interactionIcon = _lureIcon;
        base.BindInterationUI();
    }

    public override void TriggerInteraction()
    {
        if (_isDelay) return;

        _hasFish = !_hasFish;

        if (!_hasFish)
        {
            ChangeIcon(_lureIcon);
        }

        StartCoroutine(StartDelay());
    }

    protected override void EndDelay()
    {
        if (_hasFish)
        {
            ChangeIcon(_fishIcon);
        }
    }

    private void ChangeIcon(Sprite icon)
    {
        _interactionIcon = icon;
        _interactionUI.SetIconSprite(_interactionIcon);
    }
}
