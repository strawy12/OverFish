using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : InteractionObject
{
    public enum STATE
    {
        DROP,
        GRAB,
        INTERACTING
    }
    public enum CONTAIN
    {
        NONE,
        CLEANWATER,
        DIRTYWATER,
        FISH
    }

    public STATE state = STATE.DROP;
    public CONTAIN contain = CONTAIN.NONE;

    [SerializeField]
    private List<Color> containWaterColor;

    [SerializeField]
    private float fishDelay = 3f;


    [SerializeField]
    private Collider physicsCollider;
    private Rigidbody rigid;
    [SerializeField]
    private MeshRenderer waterMeshRenderer;

    protected override void Awake()
    {
        base.Awake();

        rigid = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        base.Start();

        SetContain(contain, null);
    }

    public override void TriggerInteraction()
    {
        if (state == STATE.INTERACTING)
        {
            return;
        }
        else if (state == STATE.DROP)
        {
            StateChange(STATE.GRAB);
            transform.SetParent(Define.CurrentPlayer.bucketTrm);
            Define.CurrentPlayer.currentBucket = this;

            physicsCollider.enabled = false;
            rigid.useGravity = false;
            ValueChange(false);
            transform.SetPositionAndRotation(transform.parent.position, Quaternion.identity);
        }
        else if (state == STATE.GRAB)
        {
            StateChange(STATE.DROP);
            transform.SetParent(null);

            if (Define.CurrentPlayer.currentBucket == this)
            {
                Define.CurrentPlayer.currentBucket = null;
            }

            ValueChange(true);
        }
    }

    private void ValueChange(bool isDrop)
    {
        physicsCollider.enabled = isDrop;
        rigid.useGravity = isDrop;
        rigid.constraints = isDrop ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }

    public void SetContain(CONTAIN contain, Sprite icon)
    {
        switch (contain)
        {
            case CONTAIN.NONE:
                if (this.contain == CONTAIN.FISH)
                {
                    StopAllCoroutines();
                    ResetFish();
                }
                break;
            case CONTAIN.CLEANWATER:
                break;
            case CONTAIN.DIRTYWATER:
                break;
            case CONTAIN.FISH:
                FishTrigger();
                break;
        }

        this.contain = contain;
        waterMeshRenderer.material.color = containWaterColor[(int)contain];
        ChangeIcon(icon);
    }

    public void StateChange(STATE setValue)
    {
        state = setValue;
    }

    private void ChangeIcon(Sprite icon)
    {
        _interactionIcon = icon;
        if (contain == CONTAIN.FISH)
        {
            _interactionUI.ChangeIconImageColor(Color.white);
        }
        else if(contain == CONTAIN.NONE)
        {
            _interactionUI.ChangeIconImageColor(new Color(0,0,0,0));
        }
        else
        {
            Color color = containWaterColor[(int)contain];
            color.a = 1f;
            _interactionUI.ChangeIconImageColor(color);
        }
        _interactionUI.SetIconSprite(_interactionIcon);
    }

    private void FishTrigger()
    {
        Color color = Color.yellow;
        color.a = 0.5f;
        _interactionUI.ChangeDelayImageColor(color);
        StartDelay(fishDelay);
    }

    protected override void EndDelay()
    {
        if (contain == CONTAIN.FISH)
        {
            SetContain(CONTAIN.NONE, null);
            ResetFish();
        }
    }

    private void ResetFish()
    {
        _interactionUI.SetDelayFill(0f);

        Color color = Color.black;
        color.a = 0.5f;
        _interactionUI.ChangeDelayImageColor(color);
    }
}
