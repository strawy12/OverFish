using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    private Collider physicsCollider;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
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
            transform.SetParent(PlayerInput.Inst.BucketTransform);
            physicsCollider.enabled = false;
            rigid.useGravity = false;
            ValueChange(false);
            transform.position = transform.parent.position;
            transform.rotation = Quaternion.identity;
        }
        else if (state == STATE.GRAB)
        {
            StateChange(STATE.DROP);
            transform.SetParent(null);
            ValueChange(true);
        }
    }

    private void ValueChange(bool isDrop)
    {
        physicsCollider.enabled = isDrop;
        rigid.useGravity = isDrop;
        rigid.constraints = isDrop ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }

    public void StateChange(STATE setValue)
    {
        state = setValue;
    }
}
