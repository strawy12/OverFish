using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class FishingRod : InteractionObject
{
    [Header("FishingRod Data")]

    [SerializeField]
    private Sprite _fishIcon;

    [SerializeField]
    private Sprite _baitIcon;

    private float _fishDelay = 15f;
    private float _baitDelay = 5f;

    [SerializeField]
    private float _fishFreshDelay = 5f;

    private bool _hasFish;
    private bool _startFreshTimer = false;

    private void Init()
    {
        _startFreshTimer = false;
        _hasFish = false;
        ChangeIcon(_baitIcon);
    }

    protected override void BindInterationUI()
    {
        GameManager.Inst.GameStart += Init;
        base.BindInterationUI();
        _startFreshTimer = false;
        _hasFish = false;
        ChangeIcon(_baitIcon);
    }

    public override void TriggerInteraction()
    {
        if (_isDelay && !_startFreshTimer) return;

        float delayTime = 0;
        if (_hasFish)
        {
            Bucket bucket = Define.CurrentPlayer.currentBucket;

            if (bucket == null || bucket.contain != Bucket.CONTAIN.CLEANWATER)
            { return; }

            bucket.SetContain(Bucket.CONTAIN.FISH, _fishIcon);
            _hasFish = false;
            ChangeIcon(_baitIcon);
            delayTime = _baitDelay - DataManager.Inst.FindUpgradeData(EUpgradeDataType.BaitPower).level * 0.5f;
        }

        else if (!_hasFish)
        {
            if (DataManager.Inst.CurrentPlayer.BaitCount <= 0)
            {
                Debug.Log("¹Ì³¢ ºÎÁ·");
                return;
            }

            DataManager.Inst.CurrentPlayer.BaitCount--;

            _hasFish = true;
            delayTime = _fishDelay - DataManager.Inst.FindUpgradeData(EUpgradeDataType.FishRodPower).level * 0.5f;
        }

        _interactionUI.ChangeDelayImageColor(new Color(1f, 1f, 1f, 0.5f));
        StartDelay(delayTime);
    }

    protected override void EndDelay()
    {
        if (_hasFish)
        {
            ChangeIcon(_fishIcon);

            if (!_startFreshTimer)
            {
                _startFreshTimer = true;
                _interactionUI.ChangeDelayImageColor(new Color(1f, 1f, 0f, 0.5f));
                StartDelay(_fishFreshDelay);
            }

            else
            {
                _startFreshTimer = false;
                _hasFish = false;
                ChangeIcon(_baitIcon);
            }
        }
    }

    private void ChangeIcon(Sprite icon)
    {
        _interactionIcon = icon;
        _interactionUI.SetIconSprite(_interactionIcon);
    }

    private void OnEnable()
    {
        if (_interactionUI == null) return;
        _interactionUI?.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (_interactionUI == null) return;
        _interactionUI?.gameObject.SetActive(false);
    }
}
